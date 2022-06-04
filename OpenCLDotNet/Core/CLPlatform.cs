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
        }

        public cl_platform_id Id { get; private set; }

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

            string info;

            builder.Append("Vendor: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.VENDOR, out info);
            builder.AppendLine(info);

            builder.Append("Name: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.NAME, out info);
            builder.AppendLine(info);

            builder.Append("Version: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.VERSION, out info);
            builder.AppendLine(info);

            builder.Append("Profile: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PROFILE, out info);
            builder.AppendLine(info);

            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);
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
