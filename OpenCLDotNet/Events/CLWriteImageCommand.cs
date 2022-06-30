using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLWriteImageCommand : CLCommandNode
    {
        public CLWriteImageCommand(CLImage image, Array src)
        {
            Image = image;
            Src = src;
            Region = image.Region;
            Blocking = false;
        }

        private CLImage Image { get; set; }

        private Array Src { get; set; }

        private CLRegion3t Region { get; set; }

        private bool Blocking { get; set; }

        internal override cl_event Run(CLCommand cmd)
        {
            var e =  Image.Write(cmd, Src, Region, Blocking);
            return e;   
        }
    }
}
