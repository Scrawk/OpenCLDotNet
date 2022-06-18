using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public struct CLBufferData
    {
        public CL_MEM_FLAGS Flags;

        public Array Source;

        public override string ToString()
        {
            return String.Format("[CLImageData: ]");
        }
    }
}
