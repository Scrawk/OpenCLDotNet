using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Programs
{
    public record struct CLKernelParameter
    {
        public string Name;

        public uint Dimension;

        public CLPoint3t GlobalOffset;

        public CLPoint3t GlobalSize;

        public CLPoint3t LocalSize;

        public List<CLKernelArgParameter> Args;

        public CLKernelParameter()
        {
            Name = "";
            Dimension = 0;
            GlobalOffset = new CLPoint3t();
            GlobalSize = new CLPoint3t();
            LocalSize = new CLPoint3t();
            Args = new List<CLKernelArgParameter>();
        }

    }
}
