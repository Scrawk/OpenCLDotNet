using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLReadBufferCommand : CLCommandNode
    {
        public CLReadBufferCommand(CLBuffer image, Array dst)
        {
            Buffer = image;
            Dst = dst;
            Blocking = false;
        }

        private CLBuffer Buffer { get; set; }

        private Array Dst { get; set; }

        private bool Blocking { get; set; }

        internal override cl_event Run(CLCommand cmd)
        {
            var e = Buffer.Read(cmd, Dst, Blocking);
            return e;
        }
    }
}
