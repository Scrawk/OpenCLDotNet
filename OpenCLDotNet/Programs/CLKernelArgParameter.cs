using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Programs
{
    /// <summary>
    /// 
    /// </summary>
    public record struct CLKernelArgParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name;

        /// <summary>
        /// 
        /// </summary>
        public int Index;

        /// <summary>
        /// 
        /// </summary>
        public object Value;   
        
        /// <summary>
        /// 
        /// </summary>
        public CLKernelArgParameter()
        {
            Name = "";
            Index = -1; 
            Value = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg_name"></param>
        /// <param name="obj"></param>
        public CLKernelArgParameter(string arg_name, object obj)
        {
            Name = arg_name;
            Index = -1;
            Value = obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg_index"></param>
        /// <param name="obj"></param>
        public CLKernelArgParameter(int arg_index, object obj)
        {
            Name = "";
            Index = arg_index;
            Value = obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[CLKernelArgParameter: Name={0}, Index={1}, Value={2}]", 
                Name, Index, Value);
        }

    }
}
