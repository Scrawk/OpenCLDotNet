using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLWriteBufferCommand : CLCommandNode
    {
        public CLWriteBufferCommand(CLBuffer image, Array src)
        {
            Buffer = image;
            Src = src;
            Blocking = false;
        }

        private CLBuffer Buffer { get; set; }

        private Array Src { get; set; }

        private bool Blocking { get; set; } 

        internal override cl_event Run(CLCommand cmd)
        {
            var e = Buffer.Write(cmd, Src, Blocking);
            return e;
        }
    }
}
