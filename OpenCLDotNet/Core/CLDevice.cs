using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CLDevice : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platform"></param>
        public CLDevice(cl_device_id id, CLPlatform platform)
        {
            Id = id;
            Platform = platform;
            GetInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private CLPlatform Platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Vendor { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Profile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_DEVICE_TYPE DeviceType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SupportsImages { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SupportsFP64 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGPU => DeviceType == CL_DEVICE_TYPE.GPU;

        /// <summary>
        /// 
        /// </summary>
        public bool IsCPU => DeviceType == CL_DEVICE_TYPE.CPU;

        /// <summary>
        /// 
        /// </summary>
        private List<string> Extensions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLDevice: Id={0}, PlatformID={1}, Type={2}, Vendor={3}, Error={4}]", 
                Id, Platform.Id, DeviceType, Vendor, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasExtension(string name)
        {
            GetExtensions();
            return Extensions.Contains(name);
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

            builder.AppendLine("Vendor: " + Vendor);
            builder.AppendLine("Name: " + Name);
            builder.AppendLine("Version: " + Version);
            builder.AppendLine("Profile: " + Profile);
            builder.AppendLine("Type: " + DeviceType);
            builder.AppendLine("SupportsImages: " + SupportsImages);
            builder.AppendLine("SupportsFP64: " + SupportsFP64);
            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);

            builder.AppendLine("");

            var values = CL.GetValues<CL_DEVICE_INFO>();

            foreach (var e in values)
            {
                if (e == CL_DEVICE_INFO.VENDOR ||
                   e == CL_DEVICE_INFO.NAME ||
                   e == CL_DEVICE_INFO.VERSION ||
                   e == CL_DEVICE_INFO.PROFILE ||
                   e == CL_DEVICE_INFO.EXTENSIONS ||
                   e == CL_DEVICE_INFO.TYPE||
                   e == CL_DEVICE_INFO.IMAGE_SUPPORT)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_DEVICE_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.UINT ||
                type == CL_INFO_RETURN_TYPE.ULONG ||
                type == CL_INFO_RETURN_TYPE.SIZET)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.BOOL)
                str = GetInfoBool(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else if (type == CL_INFO_RETURN_TYPE.SIZET_ARRAY)
                str = GetInfoSizetArray(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);

            return str; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetInfoString(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            CL.GetDeviceInfo(Id, name, size, info);

            var text = info.ToText();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            else
                return text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetDeviceInfo(Id, name, size, out info);

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool GetInfoBool(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoSizetArray(CL_DEVICE_INFO name)
        {
            int size_of = sizeof(size_t);

            CL.GetDeviceInfoSize(Id, name, out uint size);

            var info = new size_t[size / size_of];
            CL.GetDeviceInfo(Id, name, size, info);

            string str = "{";

            for(int i = 0; i < info.Length; i++)
            {
                str += info[i];
                if (i < info.Length - 1)
                    str += ", ";
            }

            str += "}";
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_DEVICE_INFO name)
        {
            int size_of = sizeof(cl_object);

            CL.GetDeviceInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetDeviceInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetInfo()
        {
            GetExtensions();

            Vendor = GetInfoString(CL_DEVICE_INFO.VENDOR);
            Name = GetInfoString(CL_DEVICE_INFO.NAME);
            Version = GetInfoString(CL_DEVICE_INFO.VERSION);
            Profile = GetInfoString(CL_DEVICE_INFO.PROFILE);
            SupportsImages = GetInfoBool(CL_DEVICE_INFO.IMAGE_SUPPORT);
            SupportsFP64 = HasExtension("cl_khr_fp64");

            var type = GetInfoUInt64(CL_DEVICE_INFO.TYPE);
            DeviceType = (CL_DEVICE_TYPE)type;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetExtensions()
        {
            if (Extensions != null || !IsValid)
                return;

            string info = GetInfoString(CL_DEVICE_INFO.EXTENSIONS);
      
            Extensions = new List<string>(info.Split(' '));
            Extensions.Sort();  
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Release()
        {
            CL.ReleaseDevice(Id);
        }

    }
}
