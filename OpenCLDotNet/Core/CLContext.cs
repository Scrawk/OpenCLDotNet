using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public class CLContext : CLObject
    {
        public CLContext() : this(CL_DEVICE_TYPE.GPU)
        {

        }

        public CLContext(CL_DEVICE_TYPE device_type)
        {
            CreatePlatforms(device_type);
            CreateContext();
        }

        public override bool IsValid
        {
            get 
            {  
                return Id != UIntPtr.Zero &&
                       Platform != null && 
                       Platform.IsValid; 
            }
        }

        public int NumDevices
        {
            get
            {
                if(!IsValid)
                    return 0;
                else
                    return Platform.NumDevices;
            }
        }

        public int NumPlatforms => Platforms.Count;

        private CLPlatform Platform { get; set; }

        private List<CLPlatform> Platforms { get; set; }

        public override string ToString()
        {
            var platform = Platform != null ? Platform.Id : UIntPtr.Zero;
            
            return String.Format("[CLContext: Id={0}, PlatformID={1}, NumPlatforms={2}, NumDevices={3}, Error={4}]", 
                Id, platform, NumPlatforms, NumDevices, Error);
        }

        public cl_device_id[] GetDeviceIds()
        {
            if (!IsValid)
                return new cl_device_id[0];
            else
                return Platform.GetDeviceIds();
        }

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

        public string GetInfo(CL_CONTEXT_INFO info)
        {
            if (!IsValid)
                return "UNKNOWN";

            var type = CL.GetReturnType(info);

            string str = "";

            if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);
            else
                str = "UNKNOWN";

            return str;
        }

        private UInt64 GetInfoUInt64(CL_CONTEXT_INFO name)
        {
            CL.GetContextInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetContextInfo(Id, name, size, out info);
            return info;
        }

        private unsafe string GetInfoObjectArray(CL_CONTEXT_INFO name)
        {
            CL.GetContextInfoSize(Id, name, out uint size);

            int size_of = sizeof(cl_object);
            var info = new cl_object[size / size_of];
            CL.GetContextInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

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
                Error = "NO_PLATFORMS_FOUND";
                return;
            }

            Platform = null;

            foreach (var id in platform_ids)
            {
                var platform = new CLPlatform(id);

                if(Platform == null && platform.HasDevice(device_type))
                    Platform = platform;

                Platforms.Add(platform);
            }

            if(Platform == null && Platforms.Count > 0)
                Platform = Platforms[0];
                
            SetErrorCodeToSuccess();
        }
         
        private void CreateContext()
        {
            if (Platform == null) return;

            Id = CL.CreateContext(
                Platform.Id,
                (uint)Platform.NumDevices,
                Platform.GetDeviceIds());
        }

        protected override void Release()
        {
            CL.ReleaseContext(Id);
        }

    }
}
