using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLMemData
    {
        /// <summary>
        /// 
        /// </summary>
        public CLMemData()
        {
            Data = null;
            DataType = CL_MEM_DATA_TYPE.UNKNOWN;
            ElementSize = 0;
            RowPitch = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public CLMemData(Array data)
        {
            Data = data;
            DataType = CL_MEM_DATA_TYPE.UNKNOWN;
            ElementSize = 0;
            RowPitch = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <param name="elementSize"></param>
        /// <param name="rowPitch"></param>
        public CLMemData(Array data, CL_MEM_DATA_TYPE type, uint elementSize, uint rowPitch)
        {
            Data = data;
            DataType = type;
            ElementSize = elementSize;
            RowPitch = rowPitch;    
        }

        /// <summary>
        /// 
        /// </summary>
        internal Array Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_DATA_TYPE DataType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint ElementSize { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint RowPitch { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint DataLength => (uint)(Data != null ? Data.Length : 0);

        /// <summary>
        /// 
        /// </summary>
        public uint DataByteSize => ElementSize * DataLength;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLMemData: DataLength={0}, DataType={1}, ElementSize={2}, RowPitch={3}]",
                DataLength, DataType, ElementSize, RowPitch);
        }
    }
}
