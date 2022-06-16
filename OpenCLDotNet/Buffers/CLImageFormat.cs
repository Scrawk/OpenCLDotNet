using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public record struct CLImageFormat
    {
        public CL_CHANNEL_ORDER ChannelOrder;

        public CL_CHANNEL_TYPE ChannelType;

        public CLImageFormat(CL_CHANNEL_ORDER order, CL_CHANNEL_TYPE type)
        {
            ChannelOrder = order;
            ChannelType = type;
        }

        public override string ToString()
        {
            return String.Format("[CLImageFormat: Order={0}, Type={1}]",
                ChannelOrder, ChannelType);
        }
    }
}
