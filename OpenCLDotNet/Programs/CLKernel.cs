using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Programs
{

    public class CLKernel : CLObject
    {

        public CLKernel(CLProgram program, string name)
        {
            Create(program, name);
        }

        public cl_kernel Id { get; private set; }

        public string Name { get; private set; }

        public CLProgram Program { get; private set; }

        public uint NumArguments { get; private set; }

        public string Error { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLKernel: Id={0}, Name={1}, NumArguments={2}, ProgramID={3}, Error={4}]",
                Id.Value, Name, NumArguments, Program.Id.Value, Error);
        }

        private void Create(CLProgram program, string name)
        {
            Error = "NONE";
            Program = program;
            Name = name;

            CL_ERROR err;
            Id = CL.CreateKernel(Program.Id, name.ToCLCharArray(), out err);
            if(err != CL_ERROR.SUCCESS)
            {
                Error = err.ToString();
                return;
            }

            NumArguments = (uint)GetInfoUInt64(CL_KERNEL_INFO.NUM_ARGS);
        }

        public CL_ERROR SetMemArg(cl_mem arg, uint index)
        {
            return CL.SetKernelArg(Id, index, arg);
        }

        public CL_ERROR SetSamplerArg(cl_sampler arg, uint index)
        {
            return CL.SetKernelArg(Id, index, arg);
        }

        public CL_ERROR SetIntArg(int arg, uint index)
        {
            return CL.SetKernelArg(Id, index, arg);
        }

        public CL_ERROR SetFloatArg(float arg, uint index)
        {
            return CL.SetKernelArg(Id, index, arg);
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            builder.AppendLine("");
            builder.AppendLine("Kernel info:");
            builder.AppendLine("");
            builder.AppendLine("NUM_ARGS : " + NumArguments);

            var values = Enum.GetValues<CL_KERNEL_INFO>();

            foreach (var e in values)
            {
                if (e == CL_KERNEL_INFO.NUM_ARGS)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

            if (!Program.HasKernelArgumentInfo)
                return;

            builder.AppendLine("");
            builder.AppendLine("Kernel arg info:");

            var arg_values = Enum.GetValues<CL_KERNEL_ARG_INFO>();

            for(uint i = 0; i < NumArguments; i++)
            {
                builder.AppendLine("");
                builder.AppendLine("Kernel arg " + i);

                foreach (var e in arg_values)
                {
                    builder.AppendLine(e + ": " + GetInfo(e, i));
                }
            }

        }

        public string GetInfo(CL_KERNEL_INFO info)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();
            else
                str = "Unknown";

            return str;
        }

        public string GetInfo(CL_KERNEL_ARG_INFO info, uint index)
        {
            if (!Program.HasKernelArgumentInfo)
                return "Unavailable";

            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if(type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info, index);

                if(info == CL_KERNEL_ARG_INFO.ADDRESS_QUALIFIER)
                    str = ((CL_KERNEL_ARG_ADDRESS_QUALIFIER)i).ToString();
                else if (info == CL_KERNEL_ARG_INFO.ACCESS_QUALIFIER)
                    str = ((CL_KERNEL_ARG_ACCESS_QUALIFIER)i).ToString();
                else if (info == CL_KERNEL_ARG_INFO.TYPE_QUALIFIER)
                    str = ((CL_KERNEL_ARG_TYPE_QUALIFIER)i).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info, index).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info, index);
            else
                str = "Unknown";

            return str;
        }

        private UInt64 GetInfoUInt64(CL_KERNEL_INFO name)
        {
            CL.GetKernelInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetKernelInfo(Id, name, size, out info);
            return info;
        }

        private UInt64 GetInfoUInt64(CL_KERNEL_ARG_INFO name, uint index)
        {
            CL.GetKernelArgInfoSize(Id, index, name, out uint size);

            UInt64 info;
            var err = CL.GetKernelArgInfo(Id, index, name, size, out info);
            return info;
        }

        private string GetInfoString(CL_KERNEL_INFO name)
        {
            CL.GetKernelInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            CL.GetKernelInfo(Id, name, size, info);

            var text = info.ToText();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            else
                return text;
        }

        private cl_object GetInfoObject(CL_KERNEL_INFO name)
        {
            CL.GetKernelInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetKernelInfo(Id, name, size, out info);
            return info;
        }

        private string GetInfoString(CL_KERNEL_ARG_INFO name, uint index)
        {
            CL.GetKernelArgInfoSize(Id, index, name, out uint size);

            var info = new cl_char[size];
            CL.GetKernelArgInfo(Id, index, name, size, info);

            var text = info.ToText();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            else
                return text;
        }

        protected override void Release()
        {
            CL.ReleaseKernel(Id);
        }
    }

    
}
