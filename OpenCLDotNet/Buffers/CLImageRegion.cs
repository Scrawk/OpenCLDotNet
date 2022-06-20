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
        public CLPoint3ui Origion;

        /// <summary>
        /// 
        /// </summary>
        public CLPoint3ui Size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origion"></param>
        /// <param name="size"></param>
        public CLImageRegion(CLPoint3ui origion, CLPoint3ui size)
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
            Origion = new CLPoint3ui(x, y);
            Size =new CLPoint3ui(width, height);
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
            Origion = new CLPoint3ui(x, y, z);
            Size = new CLPoint3ui(width, height, depth);
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
