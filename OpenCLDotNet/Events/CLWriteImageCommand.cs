﻿using System;
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
        }

        private CLImage Image { get; set; }

        private Array Src { get; set; }

        private CLRegion3t Region { get; set; }

        internal override void Run(CLCommand cmd)
        {
           Image.Write(cmd, Src, Region, false);
        }
    }
}
