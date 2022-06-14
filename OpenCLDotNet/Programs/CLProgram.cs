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
            CheckForKernelArgumentInfo(options);
        }

        public CLProgram(CLContext context, IList<byte[]> binaries, string options = "")
        {
            Create(context, binaries, options);
            CheckForKernelArgumentInfo(options);
        }

        public CLProgram(CLContext context, byte[] binary, string options = "")
        {
            Create(context, binary, options);
            CheckForKernelArgumentInfo(options);
        }

        public cl_program Id { get; private set; }

        public CLContext Context { get; private set; }

        public string Error { get; private set; }

        public string Options { get; private set; }

        public bool HasKernelArgumentInfo { get; private set; } 

        public CL_PROGRAM_SOURCE Source { get; private set; }   

        public override string ToString()
        {
            var options = string.IsNullOrEmpty(Options) ? "NONE" : Options;
            return String.Format("[CLProgram: Id={0}, ContextID={1}, Source={2}, Error={3}]",
                Id.Value, Context.Id.Value, Source, Error);
        }

        private void Create(CLContext context, string filename, string options = "")
        {
            Options = options;
            Error = "NONE";
            Context = context;
            Source = CL_PROGRAM_SOURCE.TEXT;

            var file = File.ReadAllText(filename, Encoding.UTF8);

            CL_ERROR error;
            Id = CL.CreateProgramWithSource(context.Id, file, out error);
            if(error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            var devices = context.GetDeviceIds();

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

        private unsafe void Create(CLContext context, IList<byte[]> binarys, string options = "")
        {
            Options = options;
            Error = "NONE";
            Context = context;
            Source = CL_PROGRAM_SOURCE.BINARY;

            var devices = context.GetDeviceIds();
            uint num_devices = (uint)devices.Length;
            if (num_devices != binarys.Count)
            {
                Error = "INVALID_NUM_DEVICES";
                return;
            }

            int sum = 0;
            var sizes = new size_t[num_devices];
            var status = new CL_ERROR[num_devices];

            for (int i = 0; i < num_devices; i++)
            {
                sizes[i] = (size_t)binarys[i].Length;
                sum += binarys[i].Length;
            }

            int index = 0;
            var bytes = new Byte[sum];
            for (int i = 0; i < num_devices; i++)
            {
                for (int j = 0; j < binarys[i].Length; j++)
                    bytes[index++] = binarys[i][j];
            }

            CL_ERROR error;
            Id = CL.CreateProgramWithBinary(Context.Id, num_devices, devices, 
                sizes, bytes, status, out error);

            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

        private unsafe void Create(CLContext context, byte[] binary, string options = "")
        {
            Options = options;
            Error = "NONE";
            Context = context;
            Source = CL_PROGRAM_SOURCE.BINARY;

            var devices = context.GetDeviceIds();
            uint num_devices = (uint)devices.Length;
            if (num_devices != 1)
            {
                Error = "INVALID_NUM_DEVICES";
                return;
            }

            var sizes = new size_t[num_devices];
            var status = new CL_ERROR[num_devices];
            sizes[0] = (size_t)binary.Length;

            CL_ERROR error;
            Id = CL.CreateProgramWithBinary(Context.Id, num_devices, devices,
                sizes, binary, status, out error);

            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

        private void CheckForKernelArgumentInfo(string options)
        {
            HasKernelArgumentInfo = options.Contains("-cl-kernel-arg-info");
        }

        public List<byte[]> GetBinary()
        {
            var binary = new List<byte[]>();
            GetBinary(binary);
            return binary;
        }

        public unsafe CL_ERROR GetBinary(List<byte[]> binaries)
        {
            int num_devices = Context.NumDevices;
            uint size = (uint)(sizeof(size_t) * num_devices);

            var sizes = new size_t[num_devices];
            var err = CL.GetProgramInfo(Id, CL_PROGRAM_INFO.BINARY_SIZES, size, sizes);
            if (err != CL_ERROR.SUCCESS)
                return err;

            uint binary_size = 0;
            for (int i = 0; i < num_devices; i++)
                binary_size += (uint)sizes[i];

            var cl_binaries = new Byte[binary_size];
            err = CL.GetProgramBinaries(Id, num_devices, sizes, cl_binaries);
            if (err != CL_ERROR.SUCCESS)
                return err;

            for (int i = 0; i < num_devices; i++)
            {
                var bytes = new byte[sizes[i]];

                for(int j = 0; j < bytes.Length; j++)
                {
                    bytes[j] = cl_binaries[i * num_devices + j];
                }

                binaries.Add(bytes);
            }

            return CL_ERROR.SUCCESS;
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

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetBuildInfoUInt64(info, device);

                if(info == CL_PROGRAM_BUILD_INFO.BINARY_TYPE)
                    str = ((CL_PROGRAM_BINARY_TYPE)i).ToString();
                else if (info == CL_PROGRAM_BUILD_INFO.STATUS)
                    str = ((CL_BUILD_STATUS)i).ToString();
            }
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
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.SIZET_ARRAY)
                str = GetInfoSizetArray(info);
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

        private string GetBuildInfoString(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            var info = new cl_char[size];
            CL.GetProgramBuildInfo(Id, device, name, size, info);

            var text = info.ToText();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            else
                return text;
        }

        private string GetInfoString(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            CL.GetProgramInfo(Id, name, size, info);

            var text = info.ToText();
            if (string.IsNullOrWhiteSpace(text))
                return "";
            else
                return text;
        }

        private bool GetInfoBool(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetProgramInfo(Id, name, size, out info);
            return info > 0;
        }

        private cl_object GetInfoObject(CL_PROGRAM_INFO name)
        {
            CL.GetProgramInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetProgramInfo(Id, name, size, out info);
            return info;
        }

        private unsafe string GetInfoObjectArray(CL_PROGRAM_INFO name)
        {
            int size_of = sizeof(cl_object);
            CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetProgramInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        private unsafe string GetInfoSizetArray(CL_PROGRAM_INFO name)
        {
            int size_of = sizeof(size_t);

            CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new size_t[size / size_of];
            CL.GetProgramInfo(Id, name, size, info);

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

        protected override void Release()
        {
            CL.ReleaseProgram(Id);
        }
    }
}
