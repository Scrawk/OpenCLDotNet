using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLImageRegion
    {
        /// <summary>
        /// 
        /// </summary>
        public CLPoint3t Origion;

        /// <summary>
        /// 
        /// </summary>
        public CLPoint3t Size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origion"></param>
        /// <param name="size"></param>
        public CLImageRegion(CLPoint3t origion, CLPoint3t size)
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
        public CLImageRegion(uint x, uint y, uint width, uint height)
        {
            Origion = new CLPoint3t(x, y);
            Size =new CLPoint3t(width, height, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public CLImageRegion(uint x, uint y, uint z, uint width, uint height, uint depth)
        {
            Origion = new CLPoint3t(x, y, z);
            Size = new CLPoint3t(width, height, depth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLImageRegion: Origin={0}, Size={1}]",
                Origion, Size);
        }

    }
}
