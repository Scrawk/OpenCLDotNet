using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLImageFormat
    {
        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_ORDER ChannelOrder;

        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_TYPE ChannelType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="type"></param>
        public CLImageFormat(CL_CHANNEL_ORDER order, CL_CHANNEL_TYPE type)
        {
            ChannelOrder = order;
            ChannelType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLImageFormat: Order={0}, Type={1}]",
                ChannelOrder, ChannelType);
        }
    }
}
