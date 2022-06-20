using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CLContext : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        public CLContext() : this(CL_DEVICE_TYPE.GPU)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device_type"></param>
        public CLContext(CL_DEVICE_TYPE device_type)
        {
            CreatePlatforms(device_type);
            CreateContext();
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return Id != UIntPtr.Zero &&
                       Platform != null &&
                       Platform.IsValid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumDevices
        {
            get
            {
                if (!IsValid)
                    return 0;
                else
                    return Platform.NumDevices;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumPlatforms => Platforms.Count;

        /// <summary>
        /// 
        /// </summary>
        private CLPlatform Platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CLPlatform> Platforms { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var platform = Platform != null ? Platform.Id : UIntPtr.Zero;

            return String.Format("[CLContext: Id={0}, PlatformID={1}, NumPlatforms={2}, NumDevices={3}, Error={4}]",
                Id, platform, NumPlatforms, NumDevices, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public cl_device_id[] GetDeviceIds()
        {
            return Platform.GetDeviceIds();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public cl_device_id GetDeviceID(int index)
        {
            return Platform.GetDeviceID(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid)
                return;

            builder.AppendLine();
            var values = Enum.GetValues<CL_CONTEXT_INFO>();

            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }

            builder.AppendLine();
            builder.AppendLine("Prefered Platform.");
            builder.AppendLine();
            Platform.Print(builder);

            if (Platforms.Count <= 1)
                return;

            builder.AppendLine();
            builder.AppendLine("Other Platforms.");
            builder.AppendLine();

            foreach (var platform in Platforms)
            {
                if (platform == Platform)
                    continue;

                platform.Print(builder);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_CONTEXT_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_CONTEXT_INFO name)
        {
            CL.GetContextInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetContextInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_CONTEXT_INFO name)
        {
            CL.GetContextInfoSize(Id, name, out uint size);

            int size_of = sizeof(cl_object);
            var info = new cl_object[size / size_of];
            CL.GetContextInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device_type"></param>
        private void CreatePlatforms(CL_DEVICE_TYPE device_type)
        {
            Platforms = new List<CLPlatform>();

            var platform_ids = new List<cl_platform_id>();
            var error = CL.GetPlatformIDs(platform_ids);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            if (platform_ids.Count == 0)
            {
                Error = ERROR_NO_PLATFORMS_FOUND;
                return;
            }

            Platform = null;

            foreach (var id in platform_ids)
            {
                var platform = new CLPlatform(id);

                if (Platform == null && platform.HasDevice(device_type))
                    Platform = platform;

                Platforms.Add(platform);
            }

            if (Platform == null && Platforms.Count > 0)
                Platform = Platforms[0];

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateContext()
        {
            if (Platform == null) return;

            Id = CL.CreateContext(
                Platform.Id,
                (uint)Platform.NumDevices,
                Platform.GetDeviceIds());
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Release()
        {
            CL.ReleaseContext(Id);
        }

    }
}
