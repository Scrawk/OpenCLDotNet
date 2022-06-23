using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;

namespace OpenCLDotNet.Programs
{
    /// <summary>
    /// 
    /// </summary>
    public class CLKernel : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="name"></param>
        public CLKernel(CLProgram program, string name)
        {
            Create(program, name);
            CreateArguments(program.HasKernelArgumentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CLProgram Program { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int NumArguments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CLKernelArg> Arguments  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLKernel: Id={0}, Name={1}, NumArguments={2}, ProgramID={3}, Error={4}]",
                Id, Name, NumArguments, Program.Id, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="name"></param>
        private void Create(CLProgram program, string name)
        {
            ResetErrorCode();
            Program = program;
            Name = name;

            CL_ERROR err;
            Id = Core.CL.CreateKernel(Program.Id, name.ToCLCharArray(), out err);
            if(err != CL_ERROR.SUCCESS)
            {
                Error = err.ToString();
                return;
            }

            NumArguments = (int)GetInfoUInt64(CL_KERNEL_INFO.NUM_ARGS);
            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasArgInfo"></param>
        private void CreateArguments(bool hasArgInfo)
        {
            Arguments = new List<CLKernelArg>(NumArguments);

            for (uint i = 0; i < NumArguments; i++)
            {
                var arg = new CLKernelArg();

                if (hasArgInfo)
                {
                    string name = GetInfo(CL_KERNEL_ARG_INFO.NAME, i);
                    string address = GetInfo(CL_KERNEL_ARG_INFO.ADDRESS_QUALIFIER, i);
                    string access = GetInfo(CL_KERNEL_ARG_INFO.ACCESS_QUALIFIER, i);

                    arg.Name = name;
                    arg.AddressQualifier = address;
                }
                
                Arguments.Add(arg);
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
       public CLKernelArg GetArgument(uint index)
        {
            if(index >= Arguments.Count) 
                throw new ArgumentOutOfRangeException($"Argument index {index} out of range,");
            
            return Arguments[(int)index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AllArgumentSet()
        {
            foreach(CLKernelArg arg in Arguments)
                if(arg.Arg == null)
                    return false;
            
            return true;    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SetBuffer(CLBuffer arg, uint index)
        {
            if (arg == null)
                throw new NullReferenceException("Buffer is null.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(CLBuffer).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SetSubBuffer(CLSubBuffer arg, uint index)
        {
            if (arg == null)
                throw new NullReferenceException("Buffer is null.");

            if(index >= NumArguments)
                throw new ArgumentOutOfRangeException($"Index {index} out of argumant range.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(CLSubBuffer).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetImage2D(CLImage2D arg, uint index)
        {
            if (arg == null)
                throw new NullReferenceException("Image is null.");

            if (index >= NumArguments)
                throw new ArgumentOutOfRangeException($"Index {index} out of argumant range.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(CLImage2D).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SetSampler(CLSampler arg, uint index)
        {
            if (arg == null)
                throw new NullReferenceException("CLSampler is null.");

            if (index >= NumArguments)
                throw new ArgumentOutOfRangeException($"Index {index} out of argumant range.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(CLSampler).Name;

            cl_sampler arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetInt(int arg, uint index)
        {
            if (index >= NumArguments)
                throw new ArgumentOutOfRangeException($"Index {index} out of argumant range.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(int).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetFloat(float arg, uint index)
        {
            if (index >= NumArguments)
                throw new ArgumentOutOfRangeException($"Index {index} out of argumant range.");

            var kernel_arg = GetArgument(index);
            kernel_arg.Arg = arg;
            kernel_arg.ArgType = typeof(float).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid) return;

            builder.AppendLine("");
            builder.AppendLine("Kernel info:");
            builder.AppendLine("NUM_ARGS : " + NumArguments);
            builder.AppendLine("Arguments :");
            builder.AppendLine("");

            foreach (var arg in Arguments)
                builder.AppendLine(arg.ToString());

            builder.AppendLine("");

            var kernel_values = CL.GetValues<CL_KERNEL_INFO>();

            foreach (var e in kernel_values)
            {
                if (e == CL_KERNEL_INFO.NUM_ARGS)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

            builder.AppendLine("");
            builder.AppendLine("Kernel work group info:");

            var work_values = CL.GetValues<CL_KERNEL_WORK_GROUP_INFO>();
            var devices = Program.Context.GetDeviceIds();

            foreach(var device in devices)
            {
                builder.AppendLine("");
                builder.AppendLine("Device " + device.Value);

                foreach (var e in work_values)
                {
                    builder.AppendLine(e + ": " + GetInfo(e, device));
                }
            }

            if (!Program.HasKernelArgumentInfo)
                return;

            builder.AppendLine("");
            builder.AppendLine("Kernel arg info:");

            var arg_values = CL.GetValues<CL_KERNEL_ARG_INFO>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_KERNEL_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetInfo(CL_KERNEL_ARG_INFO info, uint index)
        {
            if (!Program.HasKernelArgumentInfo)
                return ERROR_NO_KERNEL_ARGS_FOUND;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

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

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public string GetInfo(CL_KERNEL_WORK_GROUP_INFO info, cl_device_id device)
        {
            if (!Program.HasKernelArgumentInfo)
                return ERROR_NO_KERNEL_ARGS_FOUND;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.UINT ||
                type == CL_INFO_RETURN_TYPE.ULONG ||
                type == CL_INFO_RETURN_TYPE.SIZET)
                str = GetInfoUInt64(info, device).ToString();
            else if (type == CL_INFO_RETURN_TYPE.SIZET_ARRAY)
                str = GetInfoSizetArray(info, device);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_KERNEL_INFO name)
        {
            Core.CL.GetKernelInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetKernelInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_KERNEL_ARG_INFO name, uint index)
        {
            Core.CL.GetKernelArgInfoSize(Id, index, name, out uint size);

            UInt64 info;
            var err = Core.CL.GetKernelArgInfo(Id, index, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_KERNEL_WORK_GROUP_INFO name, cl_device_id device)
        {
            Core.CL.GetKernelWorkGroupInfoSize(Id, device, name, out uint size);

            UInt64 info;
            var err = Core.CL.GetKernelWorkGroupInfo(Id, device, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetInfoString(CL_KERNEL_INFO name)
        {
            Core.CL.GetKernelInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            Core.CL.GetKernelInfo(Id, name, size, info);

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
        private cl_object GetInfoObject(CL_KERNEL_INFO name)
        {
            Core.CL.GetKernelInfoSize(Id, name, out uint size);

            cl_object info;
            Core.CL.GetKernelInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetInfoString(CL_KERNEL_ARG_INFO name, uint index)
        {
            Core.CL.GetKernelArgInfoSize(Id, index, name, out uint size);

            var info = new cl_char[size];
            Core.CL.GetKernelArgInfo(Id, index, name, size, info);

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
        /// <param name="device"></param>
        /// <returns></returns>
        private unsafe string GetInfoSizetArray(CL_KERNEL_WORK_GROUP_INFO name, cl_device_id device)
        {
            int size_of = sizeof(size_t);

            Core.CL.GetKernelWorkGroupInfoSize(Id, device, name, out uint size);

            var info = new size_t[size / size_of];
            Core.CL.GetKernelWorkGroupInfo(Id, device, name, size, info);

            string str = "{";

            for (int i = 0; i < info.Length; i++)
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
        protected override void Release()
        {
            Core.CL.ReleaseKernel(Id);
        }
    }

    
}
