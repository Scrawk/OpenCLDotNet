using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    internal readonly record struct CLImageFormatKey
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly cl_context Context { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public readonly CL_MEM_FLAGS Flags { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public readonly CL_MEM_OBJECT_TYPE MemType { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        public CLImageFormatKey(cl_context context, CL_MEM_FLAGS flags, CL_MEM_OBJECT_TYPE type)
        {
            Context = context;
            Flags = flags;
            MemType = type;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLImageFormatKey: ContextId={0}, Flags={1}, MemType={2}]",
                Context.Value, Flags, MemType);
        }
    }
}
