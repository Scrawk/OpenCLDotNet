using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLBufferRegion
    {
        /// <summary>
        /// 
        /// </summary>
        public size_t Origion;

        /// <summary>
        /// 
        /// </summary>
        public size_t Size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origion"></param>
        /// <param name="size"></param>
        public CLBufferRegion(size_t origion, size_t size)
        {
            Origion = origion;
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLBufferRegion: Origin={0}, Size={1}]",
                Origion, Size);
        }

    }
}
