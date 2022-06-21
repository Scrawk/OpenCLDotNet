using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public class CLBufferData
    {
        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_FLAGS Flags;

        /// <summary>
        /// 
        /// </summary>
        internal CLMemData Source {  get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string source_or_null = Source == null ? "NULL" : Source.ToString();

            return String.Format("[CLImageData: Flags={0}, Source={1}]", 
                Flags, source_or_null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(float[] source)
        {
            if(source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new float[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.FLOAT;
            uint size = sizeof(float);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(int[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new int[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.INT;
            uint size = sizeof(int);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(uint[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new uint[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.UINT;
            uint size = sizeof(uint);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(short[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new short[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.SHORT;
            uint size = sizeof(short);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(ushort[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new ushort[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.USHORT;
            uint size = sizeof(ushort);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(byte[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new byte[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.BYTE;
            uint size = sizeof(byte);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetSource(sbyte[] source)
        {
            if (source == null)
                throw new ArgumentNullException("Source is null");

            var copy = new sbyte[source.Length];
            Array.Copy(source, copy, source.Length);

            var type = CL_MEM_DATA_TYPE.SBYTE;
            uint size = sizeof(sbyte);
            uint rowPitch = 0;
            Source = new CLMemData(source, type, size, rowPitch);
        }
    }
}
