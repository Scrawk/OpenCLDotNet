using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Programs
{
    public class CLProgram : CLObject
    {

        public CLProgram(CLContext context, string filename, string options = "")
        {
            Create(context, filename, options);
        }

        public cl_program Id { get; private set; }

        public CLContext Context { get; private set; }

        public CL_ERROR Error { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLProgram: Id={0}, ContextID={1}, Error={2}]",
                Id.Value, Context.Id.Value, Error);
        }

        private void Create(CLContext context, string filename, string options = "")
        {
            Error = CL_ERROR.NONE;
            Context = context;

            var file = File.ReadAllText(filename, Encoding.UTF8);

            CL_ERROR error;
            Id = CL.CreateProgramWithSource(context.Id, file, out error);
            if(error != CL_ERROR.SUCCESS)
            {
                Error = error;
                return;
            }

            var devices = context.GetDeviceIds();

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error;
                return;
            }
 
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            builder.AppendLine("");
            builder.AppendLine("Program info:");
            builder.AppendLine("");

            var values = Enum.GetValues<CL_PROGRAM_INFO>();

            foreach (var e in values)
            {
                if (e == CL_PROGRAM_INFO.SOURCE ||
                    e == CL_PROGRAM_INFO.BINARIES)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

            var build_values = Enum.GetValues<CL_PROGRAM_BUILD_INFO>();
            var devices = Context.GetDeviceIds();

            builder.AppendLine("");
            builder.AppendLine("Device info:");
            builder.AppendLine("");

            foreach (var device in devices)
            {
                builder.AppendLine("Device : " + device.Value);

                foreach (var e in build_values)
                {
                    builder.AppendLine(e + ": " + GetInfo(e, device));
                }
            }

        }

        public string GetInfo(CL_PROGRAM_BUILD_INFO info, cl_device_id device)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if (info == CL_PROGRAM_BUILD_INFO.STATUS)
                str = GetBuildInfoBuildStatus(info, device).ToString();
            else if (info == CL_PROGRAM_BUILD_INFO.BINARY_TYPE)
                str = GetBuildInfoBinaryType(info, device).ToString();
            else if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetBuildInfoUInt64(info, device).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetBuildInfoString(info, device);
            else
                str = "Unknown";

            return str;
        }

        public string GetInfo(CL_PROGRAM_INFO info)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if (type == CL_INFO_RETURN_TYPE.UINT ||
                type == CL_INFO_RETURN_TYPE.ULONG ||
                type == CL_INFO_RETURN_TYPE.SIZET)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.BOOL)
                str = GetInfoBool(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else
                str = "Unknown";

            return str;
        }

        private UInt64 GetBuildInfoUInt64(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            UInt64 info;
            CL.GetProgramBuildInfo(Id, device, name, size, out info);
            return info;
        }

        private UInt64 GetInfoUInt64(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetProgramInfo(Id, name, size, out info);
            return info;
        }

        private CL_PROGRAM_BINARY_TYPE GetBuildInfoBinaryType(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            UInt64 info;
            CL.GetProgramBuildInfo(Id, device, name, size, out info);
            return (CL_PROGRAM_BINARY_TYPE)info;
        }

        private CL_BUILD_STATUS GetBuildInfoBuildStatus(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            UInt64 info;
            CL.GetProgramBuildInfo(Id, device, name, size, out info);
            return (CL_BUILD_STATUS)info;
        }

        private string GetBuildInfoString(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            var info = new cl_char[size];
            CL.GetProgramBuildInfo(Id, device, name, size, info);

            return info.ToText();
        }

        private string GetInfoString(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            CL.GetProgramInfo(Id, name, size, info);

            return info.ToText();
        }

        private bool GetInfoBool(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetProgramInfo(Id, name, size, out info);
            return info > 0;
        }

        protected override void Release()
        {
            CL.ReleaseProgram(Id);
        }
    }
}
