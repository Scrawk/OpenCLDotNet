using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public class CLDevice : CLObject
    {
        public CLDevice(cl_device_id id)
        {
            Id = id;
            GetInfo();
            GetExtensions();
        }

        public cl_device_id Id { get; private set; }

        public string Vendor { get; private set; }

        public string Name { get; private set; }

        public string Version { get; private set; }

        public string Profile { get; private set; }

        public CL_DEVICE_TYPE Type { get; private set; }

        public bool IsGPU => Type == CL_DEVICE_TYPE.GPU;

        private List<string> Extensions { get; set; }

        public override string ToString()
        {
            return String.Format("[CLDevice: Id={0}]", Id.Value);
        }

        public bool HasExtension(string name)
        {
            GetExtensions();
            return Extensions.Contains(name);
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            builder.AppendLine("Vendor: " + Vendor);
            builder.AppendLine("Name: " + Name);
            builder.AppendLine("Version: " + Version);
            builder.AppendLine("Profile: " + Profile);
            builder.AppendLine("Type: " + Type);
            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);

            builder.AppendLine("");

            var values = Enum.GetValues<CL_DEVICE_INFO>();

            foreach (var e in values)
            {
                if (e == CL_DEVICE_INFO.VENDOR ||
                   e == CL_DEVICE_INFO.NAME ||
                   e == CL_DEVICE_INFO.VERSION ||
                   e == CL_DEVICE_INFO.PROFILE ||
                   e == CL_DEVICE_INFO.EXTENSIONS ||
                   e == CL_DEVICE_INFO.TYPE)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

        }

        public string GetInfo(CL_DEVICE_INFO info)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if (type == CL_DEVICE_INFO_RETURN_TYPE.UINT ||
                type == CL_DEVICE_INFO_RETURN_TYPE.ULONG ||
                type == CL_DEVICE_INFO_RETURN_TYPE.SIZET)
                 str = GetInfoUInt64(info).ToString();
            else if (type == CL_DEVICE_INFO_RETURN_TYPE.BOOL)
                str = GetInfoBool(info).ToString();
            else if (type == CL_DEVICE_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();
            else if (type == CL_DEVICE_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else if (type == CL_DEVICE_INFO_RETURN_TYPE.SIZET_ARRAY)
                str = GetInfoSizetArray(info);
            else if (type == CL_DEVICE_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);

            return str; 
        }

        private string GetInfoString(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            char[] info = new char[size];
            CL.GetDeviceInfo(Id, name, size, info);
            return new string(info);
        }

        private cl_object GetInfoObject(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetDeviceInfo(Id, name, size, out info);

            return info;
        }

        private bool GetInfoBool(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info > 0;
        }

        private UInt64 GetInfoUInt64(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info;
        }

        private string GetInfoSizetArray(CL_DEVICE_INFO name)
        {
            int size_of = 0;
            unsafe
            {
                size_of = sizeof(size_t);
            }

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

        private string GetInfoObjectArray(CL_DEVICE_INFO name)
        {
            int size_of = 0;
            unsafe
            {
                size_of = sizeof(cl_object);
            }

            CL.GetDeviceInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetDeviceInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        private void GetInfo()
        {
            Vendor = GetInfoString(CL_DEVICE_INFO.VENDOR);
            Name = GetInfoString(CL_DEVICE_INFO.NAME);
            Version = GetInfoString(CL_DEVICE_INFO.VERSION);
            Profile = GetInfoString(CL_DEVICE_INFO.PROFILE);

            var type = GetInfoUInt64(CL_DEVICE_INFO.TYPE);
            Type = (CL_DEVICE_TYPE)type;
        }

        private void GetExtensions()
        {
            if (Extensions != null)
                return;

            string info = GetInfoString(CL_DEVICE_INFO.EXTENSIONS);
      
            Extensions = new List<string>(info.Split(' '));
        }

    }
}
