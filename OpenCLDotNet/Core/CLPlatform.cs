using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCLDotNet.Core
{
    public class CLPlatform : CLObject
    {

        public CLPlatform(cl_platform_id id)
        {
            Id = id;
            GetInfo();
            GetExtensions();
        }

        public cl_platform_id Id { get; private set; }

        public string Vendor { get; private set; }

        public string Name { get; private set; }

        public string Version { get; private set; }

        public string Profile { get; private set; }

        private List<string> Extensions { get; set; }

        public override string ToString()
        {
            return String.Format("[CLPlatform: Id={0}]", Id.Value);
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
            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);
        }

        public string GetInfo(CL_PLATFORM_INFO info)
        {
            string str;
            CL.GetPlatformInfo(Id, info, out str);
            return str;
        }

        private void  GetInfo()
        {
            Vendor = GetInfo(CL_PLATFORM_INFO.VENDOR);
            Name = GetInfo(CL_PLATFORM_INFO.NAME);
            Version = GetInfo(CL_PLATFORM_INFO.VERSION);
            Profile = GetInfo(CL_PLATFORM_INFO.PROFILE);
        }

        private void GetExtensions()
        {
            if (Extensions != null)
                return;

            string info;
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.EXTENSIONS, out info);

            Extensions = new List<string>(info.Split(' '));
        }
    }
}
