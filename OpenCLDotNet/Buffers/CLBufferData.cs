using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public struct CLBufferData
    {
        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_FLAGS Flags;

        /// <summary>
        /// 
        /// </summary>
        public Array Source {  get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string len_or_null = Source != null ? Source.Length.ToString() : "NULL";
  
            return String.Format("[CLImageData: Flags={0}, SourceLen={1}]", 
                Flags, len_or_null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(float[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(int[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(uint[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(short[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(ushort[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(byte[] source)
        {
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void SetSource(sbyte[] source)
        {
            Source = source;
        }
    }
}
