using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLPoint3t
    {
        /// <summary>
        /// 
        /// </summary>
        public size_t x, y, z;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public CLPoint3t(uint x, uint y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public CLPoint3t(uint x, uint y, uint z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{x},{y},{z}";
        }
    }
}
