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

        private cl_platform_id Id {  get; set; }

        public override string ToString()
        {
            return String.Format("[CLPlatform: Id={0}]", Id.Value);
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            string info;

            builder.Append("Vendor: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PLATFORM_VENDOR, out info);
            builder.AppendLine(info);

            builder.Append("Name: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PLATFORM_NAME, out info);
            builder.AppendLine(info);

            builder.Append("Version: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PLATFORM_VERSION, out info);
            builder.AppendLine(info);

            builder.Append("Profile: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PLATFORM_PROFILE, out info);
            builder.AppendLine(info);

            builder.AppendLine("Extensions: ");
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.PLATFORM_EXTENSIONS, out info);

            var extensions = info.Split(' ');
            for (int i = 0; i < extensions.Length; i++)
                builder.AppendLine(extensions[i]);
        }
    }
}
