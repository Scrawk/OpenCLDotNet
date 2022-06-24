using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLRegion1t
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
        public CLRegion1t(size_t origion, size_t size)
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
            return String.Format("[CLRegion1t: Origin={0}, Size={1}]",
                Origion, Size);
        }

    }
}
