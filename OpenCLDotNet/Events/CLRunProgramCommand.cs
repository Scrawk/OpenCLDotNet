using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Programs;

namespace OpenCLDotNet.Events
{
    public sealed class CLRunProgramCommand : CLCommandNode
    {
        public CLRunProgramCommand(CLProgram program, CLKernelParameter kernel_params)
        {
            Program = program;
            KernelParams = kernel_params;
        }

        private CLProgram Program { get; set; }

        private CLKernelParameter KernelParams { get; set; }

        internal override cl_event Run(CLCommand cmd)
        {
            var e = Program.Run(KernelParams);
            return e;
        }
    }
}
