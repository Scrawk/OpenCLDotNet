using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLFillImageCommand : CLCommandNode
    {
        public CLFillImageCommand(CLImage image, CLColorRGBA color)
        {
            Image = image;
            Region = image.Region;
            DataType = CL_DATA_TYPE.FLOAT;

            Fill = new[]
            {
                color.r,
                color.g,
                color.b,
                color.a,
            };
        }

        public CLFillImageCommand(CLImage image, float[] color)
        {
            Image = image;
            Region = image.Region;

            int len = Math.Min(color.Length, 4);

            Fill = new float[len];
            DataType = CL_DATA_TYPE.FLOAT;
            Array.Copy(color, Fill, Fill.Length);   
        }

        public CLFillImageCommand(CLImage image, int[] color)
        {
            Image = image;
            Region = image.Region;

            int len = Math.Min(color.Length, 4);

            Fill = new int[len];
            DataType = CL_DATA_TYPE.INT;
            Array.Copy(color, Fill, Fill.Length);
        }

        public CLFillImageCommand(CLImage image, uint[] color)
        {
            Image = image;
            Region = image.Region;

            int len = Math.Min(color.Length, 4);

            Fill = new uint[len];
            DataType = CL_DATA_TYPE.UINT;
            Array.Copy(color, Fill, Fill.Length);
        }

        private CLImage Image { get; set; }

        private Array Fill { get; set; }

        private  CL_DATA_TYPE DataType { get; set; }

        private CLRegion3t Region { get; set; }

        internal override cl_event Run(CLCommand cmd)
        {
            var e = Image.Fill(cmd, Fill, DataType, Region);
            return e;
        }
 
    }
}
