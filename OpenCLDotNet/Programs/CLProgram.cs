using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Programs
{
    public partial class CLProgram : CLObject
    {

        /*
         https://www.khronos.org/registry/OpenCL/sdk/1.0/docs/man/xhtml/clBuildProgram.html
         
         These options control the OpenCL preprocessor which is run on each program 
         source before actual compilation. -D options are processed in the order they
         are given in the options argument to clBuildProgram.

         -D name
         Predefine name as a macro, with definition 1.      
       
         -D name = definition
         The contents of definition are tokenized and processed as if they appeared during 
         translation phase three in a `#define' directive. In particular, the definition
         will be truncated by embedded newline characters.
        
         -I dir
         Add the directory dir to the list of directories to be searched for header files.
        */

        /// <summary>
        /// Enables the kerenls argument info in the program to be available.
        /// </summary>
        public static readonly string OPTION_KERNEL_ARG_INFO = "-cl-kernel-arg-info";

        /// <summary>
        /// Treat double precision floating-point constant as single precision constant.
        /// </summary>
        public static readonly string OPTION_SINGLE_PRECISION_CONSTANT = "-cl-single-precision-constant";

        /// <summary>
        /// This option controls how single precision and double precision denormalized numbers are handled. 
        /// If specified as a build option, the single precision denormalized numbers may be flushed
        /// to zero and if the optional extension for double precision is supported, double precision
        /// denormalized numbers may also be flushed to zero. This is intended to be a performance 
        /// hint and the OpenCL compiler can choose not to flush denorms to zero if the device supports 
        /// single precision (or double precision) denormalized numbers.
        /// This option is ignored for single precision numbers if the device does not support single 
        /// precision denormalized numbers i.e.CL_FP_DENORM bit is not set in CL_DEVICE_SINGLE_FP_CONFIG.
        /// This option is ignored for double precision numbers if the device does not support double 
        /// precision or if it does support double precison but CL_FP_DENORM bit is not set in CL_DEVICE_DOUBLE_FP_CONFIG.
        /// This flag only applies for scalar and vector single precision floating-point variables and
        /// computations on these floating-point variables inside a program.It does not apply to reading
        /// from or writing to image objects.
        /// </summary>
        public static readonly string OPTION_DENORMS_ARE_ZERO = "-cl-denorms-are-zero";

        /// <summary>
        /// This option disables all optimizations. The default is optimizations are enabled.
        /// </summary>
        public static readonly string OPTION_OPT_DISABLE = "-cl-opt-disable";

        /// <summary>
        /// This option allows the compiler to assume the strictest aliasing rules.
        /// </summary>
        public static readonly string OPTION_STRICT_ALIASING = "-cl-strict-aliasing";

        /// <summary>
        /// Allow a * b + c to be replaced by a mad. The mad computes a * b + c with reduced accuracy. 
        /// For example, some OpenCL devices implement mad as truncate the result of a * b before adding it to c.
        /// </summary>
        public static readonly string OPTION_MAD_ENABLED = "-cl-mad-enable";

        /// <summary>
        /// Allow optimizations for floating-point arithmetic that ignore the signedness of zero. 
        /// IEEE 754 arithmetic specifies the behavior of distinct +0.0 and -0.0 values, 
        /// which then prohibits simplification of expressions such as x+0.0 or 0.0*x (even with -clfinite-math only). 
        /// This option implies that the sign of a zero result isn't significant.
        /// </summary>
        public static readonly string OPTION_NO_SIGNED_ZEROS = "-cl-no-signed-zeros";

        /// <summary>
        /// Allow optimizations for floating-point arithmetic that (a) assume that arguments and results are valid,
        /// (b) may violate IEEE 754 standard and (c) may violate the OpenCL numerical compliance requirements as 
        /// defined in section 7.4 for single-precision floating-point, section 9.3.9 for double-precision floating-point, 
        /// and edge case behavior in section 7.5. This option includes the -cl-no-signed-zeros and -cl-mad-enable options.
        /// </summary>
        public static readonly string OPTION_UNSAFE_MATH_OPTIMIZATIONS = "-cl-unsafe-math-optimizations";

        /// <summary>
        /// Allow optimizations for floating-point arithmetic that assume that arguments and results are not NaNs or ±∞.
        /// This option may violate the OpenCL numerical compliance requirements defined in in section 7.4 
        /// for single-precision floating-point, section 9.3.9 for double-precision floating-point, 
        /// and edge case behavior in section 7.5.
        /// </summary>
        public static readonly string OPTION_FINITE_MATH_ONLY = "-cl-finite-math-only";

        /// <summary>
        /// Sets the optimization options -cl-finite-math-only and -cl-unsafe-math-optimizations. 
        /// This allows optimizations for floating-point arithmetic that may violate the IEEE 754 standard and the
        /// OpenCL numerical compliance requirements defined in the specification in section 7.4 for single-precision
        /// floating-point, section 9.3.9 for double-precision floating-point, and edge case behavior in section 7.5.
        /// This option causes the preprocessor macro __FAST_RELAXED_MATH__ to be defined in the OpenCL program.
        /// </summary>
        public static readonly string OPTION_FAST_RELAXED_MATH = "-cl-fast-relaxed-math";

        /// <summary>
        /// Inhibit all warning messages.
        /// </summary>
        public static readonly string OPTION_DISABLE_WARNINGS = "-W";

        /// <summary>
        /// Make all warnings into errors.
        /// </summary>
        public static readonly string OPTION_WARNINGS_AS_ERRORS = "-Werror";

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultOptions
        {
            get
            {
                var options = CLProgram.OPTION_KERNEL_ARG_INFO;
                return options;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filename"></param>
        /// <param name="encoding"></param>
        /// <param name="options"></param>
        public CLProgram(CLContext context, string filename, Encoding encoding, string options = "")
        {
            if (string.IsNullOrEmpty(options))
                options = CLProgram.DefaultOptions;

            var file = File.ReadAllText(filename, encoding);

            CreateOptions(options);
            CreateProgramFromText(context, file);
            CreateKerels();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program_text"></param>
        /// <param name="options"></param>
        public CLProgram(string program_text, string options = "")
            : this(new CLContext(), program_text, options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="program_text"></param>
        /// <param name="options"></param>
        public CLProgram(CLContext context, string program_text, string options = "")
        {
            if (string.IsNullOrEmpty(options))
                options = CLProgram.DefaultOptions;

            CreateOptions(options);
            CreateProgramFromText(context, program_text);
            CreateKerels();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="binaries"></param>
        /// <param name="options"></param>
        public CLProgram(CLContext context, IList<byte[]> binaries, string options = "")
        {
            if (string.IsNullOrEmpty(options))
                options = CLProgram.DefaultOptions;

            CreateOptions(options);
            CreateProgramFromBinaries(context, binaries);
            CreateKerels();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="binary"></param>
        /// <param name="options"></param>
        public CLProgram(CLContext context, byte[] binary, string options = "")
        {
            if (string.IsNullOrEmpty(options))
                options = CLProgram.DefaultOptions;

            CreateOptions(options);
            CreateFromBinary(context, binary);
            CreateKerels();
        }

        /// <summary>
        /// 
        /// </summary>
        private CLContext Context { get; set; }

        public CLCommand Command { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<string> Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_PROGRAM_SOURCE Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int NumKernels => Kernels.Count;

        /// <summary>
        /// 
        /// </summary>
        private List<CLKernel> Kernels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLProgram: Id={0}, ContextID={1}, Source={2}, Kernels={3}, Error={4}]",
                Id, Context.Id, Source, Kernels.Count, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="program_text"></param>
        private void CreateProgramFromText(CLContext context, string program_text)
        {
            ResetErrorCode();
            Context = context;
            Command = new CLCommand(context);
            Source = CL_PROGRAM_SOURCE.TEXT;

            CL_ERROR error;
            Id = CL.CreateProgramWithSource(context.Id, program_text, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            var devices = context.GetDeviceIds();
            string options = GetOptionsString();

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="binarys"></param>
        private unsafe void CreateProgramFromBinaries(CLContext context, IList<byte[]> binarys)
        {
            ResetErrorCode();
            Context = context;
            Command = new CLCommand(context);
            Source = CL_PROGRAM_SOURCE.BINARY;

            var devices = context.GetDeviceIds().ToArray();
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

            string options = GetOptionsString();

            error = CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="binary"></param>
        private unsafe void CreateFromBinary(CLContext context, byte[] binary)
        {
            ResetErrorCode();
            Context = context;
            Command = new CLCommand(context);
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
            Id = Core.CL.CreateProgramWithBinary(Context.Id, num_devices, devices,
                sizes, binary, status, out error);

            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            string options = GetOptionsString();

            error = Core.CL.BuildProgram(Id, (uint)devices.Length, devices, options);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_param"></param>
        public void Run(CLKernelParameter kernel_param)
        {
            var kernel = FindKernel(kernel_param.Name);
            if (kernel == null)
                return;

            foreach (var arg in kernel_param.Args)
            {
                if(!string.IsNullOrEmpty(arg.Name))
                    kernel.SetArgument(arg.Name, arg.Value);
                else if(arg.Index >= 0)
                    kernel.SetArgument((uint)arg.Index, arg.Value);   
            }

            Run(kernel, kernel_param.Dimension, kernel_param.GlobalOffset, kernel_param.GlobalSize, kernel_param.LocalSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_params"></param>
        public void Run(IList<CLKernelParameter> kernel_params)
        {
            foreach(var kernel_param in kernel_params)
            {
                var kernel = FindKernel(kernel_param.Name);
                if (kernel == null)
                    return;

               foreach(var arg in kernel_param.Args)
               {
                    kernel.SetArgument(arg.Name, arg.Value); 
               }

                Run(kernel, kernel_param.Dimension, kernel_param.GlobalOffset, kernel_param.GlobalSize, kernel_param.LocalSize);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="global_offset"></param>
        /// <param name="global_size"></param>
        /// <param name="local_size"></param>
        public void Run(string kernel_name, uint global_offset, uint global_size, uint local_size)
        {
            var kernel = FindKernel(kernel_name);
            if (kernel == null)
                return;

            var global_offset_3 = new CLPoint3t(global_offset, 0, 0);
            var global_size_3 = new CLPoint3t(global_size, 1, 1);
            var local_size_3 = new CLPoint3t(local_size, 1, 1);

            Run(kernel, 1, global_offset_3, global_size_3, local_size_3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="global_offset"></param>
        /// <param name="global_size"></param>
        /// <param name="local_size"></param>
        public void Run(string kernel_name, CLPoint2t global_offset, CLPoint2t global_size, CLPoint2t local_size)
        {
            var kernel = FindKernel(kernel_name);
            if (kernel == null)
                return;

            var global_offset_3 = new CLPoint3t(global_offset, 0);
            var global_size_3 = new CLPoint3t(global_size, 1);
            var local_size_3 = new CLPoint3t(local_size, 1);

            Run(kernel, 2, global_offset_3, global_size_3, local_size_3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="dimension"></param>
        /// <param name="global_offset"></param>
        /// <param name="global_size"></param>
        /// <param name="local_size"></param>
        private void Run(CLKernel kernel, uint dimension, CLPoint3t global_offset, CLPoint3t global_size, CLPoint3t local_size)
        {
            CheckKernel(kernel, "", true);

            cl_event e;
            size_t[] globalOffset = { global_offset.x, global_offset.y, global_offset.z };
            size_t[] localSize = { local_size.x, local_size.y, local_size.z };
            size_t[] globalSize =  { RoundUp(localSize[0], global_size.x),
                                     RoundUp(localSize[1], global_size.y),
                                     RoundUp(localSize[2], global_size.z)};

            Error = CL.EnqueueNDRangeKernel(Command.Id, kernel.Id, dimension, globalOffset,
                       globalSize, localSize, 0, null, out e).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupSize"></param>
        /// <param name="globalSize"></param>
        /// <returns></returns>
        private size_t RoundUp(size_t groupSize, size_t globalSize)
        {
            var r = globalSize.Value % groupSize.Value;
            if (r == 0)
                return globalSize;
            else
                return globalSize + groupSize - r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetOptionsString()
        {
            string options = "";
            foreach (var option in Options)
                options += option + " ";

            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateKerels()
        {
            Kernels = new List<CLKernel>();

            if (!IsValid) return;

            var names_info = GetInfo(CL_PROGRAM_INFO.KERNEL_NAMES);
            var names_array = names_info.Split(';');

            foreach(var name in names_array)
            {
                var kernel_name = name.RemoveWhiteSpaces();

                if (!string.IsNullOrEmpty(kernel_name))
                {
                    var kernel = new CLKernel(this, kernel_name);
                    Kernels.Add(kernel);
                }
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private CLKernel FindKernel(string name)
        {
            foreach(var kernel in Kernels)  
                if (kernel.Name == name)
                    return kernel;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasKernel(string name)
        {
            return FindKernel(name) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetKernelName(int index)
        {
            if(index < 0 || index >= Kernels.Count) 
                throw new ArgumentOutOfRangeException($"Kernel index {index} out of range.");

            return Kernels[index].Name; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public cl_device_id[] GetDeviceIds()
        {
            return Context.GetDeviceIds();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public CLBuffer CreateReadBuffer(Array data, CL_DATA_TYPE type)
        {
            var buffer = CLBuffer.CreateReadBuffer(Context, data, type);
            Error = buffer.Error;
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public CLBuffer CreateReadBuffer(string kernel_name, uint index, Array data, CL_DATA_TYPE type)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var buffer = CLBuffer.CreateReadBuffer(Context, data, type);
            kernel.SetBuffer(buffer, index);

            Error = buffer.Error;
            return buffer;  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public CLBuffer CreateWriteBuffer(CL_DATA_TYPE type, uint length)
        {
            var buffer = CLBuffer.CreateWriteBuffer(Context, type, length);
            Error = buffer.Error;
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public CLBuffer CreateWriteBuffer(string kernel_name, uint index, CL_DATA_TYPE type, uint length)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var buffer = CLBuffer.CreateWriteBuffer(Context, type, length);
            kernel.SetBuffer(buffer, index);

            Error = buffer.Error;
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CLImage2D CreateReadImage2D(CLImageParameters2D param)
        {
            var image = CLImage2D.CreateReadImage2D(Context, param);
            Error = image.Error;
            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public CLImage2D CreateReadImage2D(string kernel_name, uint index, CLImageParameters2D param)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var image = CLImage2D.CreateReadImage2D(Context, param);
            kernel.SetImage(image, index);

            Error = image.Error;
            return image;   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CLImage2D CreateWriteImage2D(CLImageParameters2D param)
        {
            var image = CLImage2D.CreateWriteImage2D(Context, param);
            Error = image.Error;
            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public CLImage2D CreateWriteImage2D(string kernel_name, uint index, CLImageParameters2D param)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var image = CLImage2D.CreateWriteImage2D(Context, param);
            kernel.SetImage(image, index);

            Error = image.Error;
            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CLSampler CreateSamplerUV()
        {
            var mode = CL_ADDRESSING_MODE.CLAMP_TO_EDGE;
            var filter = CL_FILTER_MODE.LINEAR;
            return CreateSampler(true, mode, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLSampler CreateSamplerUV(string kernel_name, uint index)
        {
            var mode = CL_ADDRESSING_MODE.CLAMP_TO_EDGE;
            var filter = CL_FILTER_MODE.LINEAR;
            return CreateSampler(kernel_name, index, true, mode, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CLSampler CreateSamplerIndex()
        {
            var mode = CL_ADDRESSING_MODE.CLAMP_TO_EDGE;
            var filter = CL_FILTER_MODE.NEAREST;
            return CreateSampler(false, mode, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLSampler CreateSamplerIndex(string kernel_name, uint index)
        {
            var mode = CL_ADDRESSING_MODE.CLAMP_TO_EDGE;
            var filter = CL_FILTER_MODE.NEAREST;
            return CreateSampler(kernel_name, index, false, mode, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalizedCoords"></param>
        /// <param name="mode"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public CLSampler CreateSampler(bool normalizedCoords, CL_ADDRESSING_MODE mode, CL_FILTER_MODE filter)
        {
            var sampler = new CLSampler(Context, normalizedCoords, mode, filter);
            Error = sampler.Error;
            return sampler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="normalizedCoords"></param>
        /// <param name="mode"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public CLSampler CreateSampler(string kernel_name, uint index,
            bool normalizedCoords,
            CL_ADDRESSING_MODE mode,
            CL_FILTER_MODE filter)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var sampler = new CLSampler(Context, normalizedCoords, mode, filter);
            kernel.SetSampler(sampler, index);

            Error = sampler.Error;
            return sampler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public CLSampler CreateSampler(string kernel_name, uint index, CLSamplerProperties properties)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            var sampler = new CLSampler(Context, properties);
            kernel.SetSampler(sampler, index);

            Error = sampler.Error;
            return sampler; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="kernel_name"></param>
        /// <param name="blocking"></param>
        /// <param name="index"></param>
        /// <param name="dst"></param>
        /// <exception cref="InvalidCastException"></exception>
        public void ReadBuffer(CLCommand cmd, string kernel_name, bool blocking, uint index, Array dst)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, true);

            var arg = kernel.GetArgument(index);
            var buffer = arg.ArgObject as CLBuffer;

            if (buffer == null)
                throw new InvalidCastException($"Kernel named {kernel_name} arg at index {index} is not a buffer");

            buffer.Read(cmd, dst, 0, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="kernel_name"></param>
        /// <param name="blocking"></param>
        /// <param name="index"></param>
        /// <param name="src"></param>
        /// <exception cref="InvalidCastException"></exception>
        public void WriteBuffer(CLCommand cmd, string kernel_name, bool blocking, uint index, Array src)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, true);

            var arg = kernel.GetArgument(index);
            var buffer = arg.ArgObject as CLBuffer;

            if (buffer == null)
                throw new InvalidCastException($"Kernel named {kernel_name} arg at index {index} is not a buffer");

            buffer.Write(cmd, src, 0, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="kernel_name"></param>
        /// <param name="blocking"></param>
        /// <param name="index"></param>
        /// <param name="dst"></param>
        /// <exception cref="InvalidCastException"></exception>
        public void ReadImage(CLCommand cmd, string kernel_name, bool blocking, uint index, Array dst)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, true);

            var arg = kernel.GetArgument(index);
            var image = arg.ArgObject as CLImage;

            if (image == null)
                throw new InvalidCastException($"Kernel named {kernel_name} arg at index {index} is not a image");

            image.Read(cmd, dst, image.Region, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="kernel_name"></param>
        /// <param name="blocking"></param>
        /// <param name="index"></param>
        /// <param name="src"></param>
        /// <exception cref="InvalidCastException"></exception>
        public void WriteImage(CLCommand cmd, string kernel_name, bool blocking, uint index, Array src)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, true);

            var arg = kernel.GetArgument(index);
            var image = arg.ArgObject as CLImage;

            if (image == null)
                throw new InvalidCastException($"Kernel named {kernel_name} arg at index {index} is not a image");

            image.Write(cmd, src, image.Region, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public bool HasOption(string option)
        {
            return Options.Contains(option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        private void CreateOptions(string options)
        {
            Options = new List<string>();

            if (string.IsNullOrEmpty(options))
                return;

            var split = options.Split(' ');

            foreach(var str in split)
            {
                var option = str.RemoveWhiteSpaces();

                if(!string.IsNullOrEmpty(option))
                    Options.Add(option);
            }

            if (!HasOption(OPTION_KERNEL_ARG_INFO))
                Options.Add(OPTION_KERNEL_ARG_INFO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetBuildLog(int index)
        {
            var device = Context.GetDeviceID(index);
            return GetInfo(CL_PROGRAM_BUILD_INFO.LOG, device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetBinary()
        {
            var binary = new List<byte[]>();
            GetBinary(binary);
            return binary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaries"></param>
        /// <returns></returns>
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

                for (int j = 0; j < bytes.Length; j++)
                {
                    bytes[j] = cl_binaries[i * num_devices + j];
                }

                binaries.Add(bytes);
            }

            return CL_ERROR.SUCCESS;
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
            builder.AppendLine("Program info:");

            builder.AppendLine("");
            builder.AppendLine("Options:");

            foreach(var option in Options)
                builder.AppendLine(option);

            builder.AppendLine("");

            var values = CL.GetValues<CL_PROGRAM_INFO>();

            foreach (var e in values)
            {
                if (e == CL_PROGRAM_INFO.SOURCE ||
                    e == CL_PROGRAM_INFO.KERNEL_NAMES ||
                    e == CL_PROGRAM_INFO.NUM_KERNELS ||
                    e == CL_PROGRAM_INFO.BINARIES)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

            var build_values = CL.GetValues<CL_PROGRAM_BUILD_INFO>();
            var devices = Context.GetDeviceIds();

            builder.AppendLine("");
            builder.AppendLine("Device build info:");
            builder.AppendLine("");

            foreach (var device in devices)
            {
                builder.AppendLine("Device : " + device.Value);

                foreach (var e in build_values)
                {
                    if (e == CL_PROGRAM_BUILD_INFO.OPTIONS)
                        continue;

                    builder.AppendLine(e + ": " + GetInfo(e, device));
                }
            }

            builder.AppendLine("");
            builder.AppendLine("Kernels:");

            builder.AppendLine("");
            foreach (var kernel in Kernels)
                 kernel.Print(builder);

            builder.AppendLine("");
            builder.AppendLine("Context:");

            Context.Print(builder);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public string GetInfo(CL_PROGRAM_BUILD_INFO info, cl_device_id device)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetBuildInfoUInt64(info, device);

                if (info == CL_PROGRAM_BUILD_INFO.BINARY_TYPE)
                    str = ((CL_PROGRAM_BINARY_TYPE)i).ToString();
                else if (info == CL_PROGRAM_BUILD_INFO.STATUS)
                    str = ((CL_BUILD_STATUS)i).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetBuildInfoUInt64(info, device).ToString();
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetBuildInfoString(info, device);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_PROGRAM_INFO info)
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
            else if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
                str = GetInfoObject(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.SIZET_ARRAY)
                str = GetInfoSizetArray(info);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        private UInt64 GetBuildInfoUInt64(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            Core.CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            UInt64 info;
            Core.CL.GetProgramBuildInfo(Id, device, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_PROGRAM_INFO name)
        {
            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetProgramInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        private string GetBuildInfoString(CL_PROGRAM_BUILD_INFO name, cl_device_id device)
        {
            Core.CL.GetProgramBuildInfoSize(Id, device, name, out uint size);

            var info = new cl_char[size];
            Core.CL.GetProgramBuildInfo(Id, device, name, size, info);

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
        private string GetInfoString(CL_PROGRAM_INFO name)
        {
            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new cl_char[size];
            Core.CL.GetProgramInfo(Id, name, size, info);

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
        private bool GetInfoBool(CL_PROGRAM_INFO name)
        {
            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetProgramInfo(Id, name, size, out info);
            return info > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_PROGRAM_INFO name)
        {
            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            cl_object info;
            Core.CL.GetProgramInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_PROGRAM_INFO name)
        {
            int size_of = sizeof(cl_object);
            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            Core.CL.GetProgramInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoSizetArray(CL_PROGRAM_INFO name)
        {
            int size_of = sizeof(size_t);

            Core.CL.GetProgramInfoSize(Id, name, out uint size);

            var info = new size_t[size / size_of];
            Core.CL.GetProgramInfo(Id, name, size, info);

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
            Core.CL.ReleaseProgram(Id);
        }

        private void CheckKernel(CLKernel kernel, string kernel_name, bool check_args_set)
        {
            if (kernel == null)
                throw new NullReferenceException($"Kernel named {kernel_name} not found.");

            string name = string.IsNullOrEmpty(kernel_name) ? kernel.Name : kernel_name;

            if (!kernel.IsValid)
                throw new InvalidObjectExeception($"Kernel named {name} is not valid.");

            if (check_args_set && !kernel.AllArgumentSet())
                throw new InvalidOperationException($"Not all kernel arguments are set for kernel {name}.");
        }

        private void CheckKernelArg(CLKernelArg kernel_arg)
        {
            if (kernel_arg == null)
                throw new NullReferenceException($"Kernel arg is null.");

            if (kernel_arg.ArgObject == null)
                throw new NullReferenceException($"Kernel arg object is null.");
        }
    }
}
