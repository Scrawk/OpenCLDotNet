using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLImageDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_OBJECT_TYPE MemType;

        /// <summary>
        /// 
        /// </summary>
        public size_t Width;

        /// <summary>
        /// 
        /// </summary>
        public size_t Height;

        /// <summary>
        /// 
        /// </summary>
        public size_t Depth;

        /// <summary>
        /// 
        /// </summary>
        public size_t ArraySize;

        /// <summary>
        /// 
        /// </summary>
        public size_t RowPitch;

        /// <summary>
        /// 
        /// </summary>
        public size_t SlicePitch;

        /// <summary>
        /// 
        /// </summary>
        public cl_uint NumMipLevels;

        /// <summary>
        /// 
        /// </summary>
        public cl_uint NumSamples;

        /// <summary>
        /// 
        /// </summary>
        public cl_mem Buffer;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLImageDescription: MemType={0}, Width={1}, Height={2}, Depth={3}]",
                MemType, Width, Height, Depth);
        }
    }
}
