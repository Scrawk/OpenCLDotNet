﻿using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLReadImageCommand : CLCommandNode
    {
        public CLReadImageCommand(CLImage image, Array dst)
        {
            Image = image;
            Dst = dst;
            Region = image.Region;
        }

        private CLImage Image { get; set; }

        private Array Dst { get; set; }

        private CLRegion3t Region { get; set; }

        internal override cl_event Run(CLCommand cmd)
        {
            var e = Image.Read(cmd, Dst, Region, false);
            return e;
        }
    }
}
