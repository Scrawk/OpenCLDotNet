using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public readonly struct CLImageFormat
    {
        public readonly CL_CHANNEL_ORDER Order {  get; init; }

        public readonly CL_CHANNEL_TYPE Type { get; init; }

        public CLImageFormat(CL_CHANNEL_ORDER order, CL_CHANNEL_TYPE type)
        {
            Order = order;
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("[CLImageFormat: Order={0}, Type={1}]",
                Order, Type);
        }
    }
}
