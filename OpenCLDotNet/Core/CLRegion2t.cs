using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLRegion2t
    {
        /// <summary>
        /// 
        /// </summary>
        public CLPoint2t Origion;

        /// <summary>
        /// 
        /// </summary>
        public CLPoint2t Size;

        /// <summary>
        /// 
        /// </summary>
        public ulong Length => Size.x + Size.y;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origion"></param>
        /// <param name="size"></param>
        public CLRegion2t(CLPoint2t origion, CLPoint2t size)
        {
            Origion = origion;
            Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public CLRegion2t(uint x, uint y, uint width, uint height)
        {
            Origion = new CLPoint2t(x, y);
            Size = new CLPoint2t(width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLRegion2t: Origin={0}, Size={1}]",
                Origion, Size);
        }

    }
}
