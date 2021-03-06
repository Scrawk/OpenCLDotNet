using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public struct CLSamplerProperties
    {
        /// <summary>
        /// 
        /// </summary>
        public cl_bool NormalizedCoords;

        /// <summary>
        /// 
        /// </summary>
        public CL_ADDRESSING_MODE AddressingMode;
        
        /// <summary>
        /// 
        /// </summary>
        public CL_FILTER_MODE FilterMode;

        //public CL_SAMPLER_FILTER_MODE MipFilterMode;

        //public cl_float LODMin;

        //public cl_float LODMax;

        /// <summary>
        /// 
        /// </summary>
        public CLSamplerProperties()
        {
            NormalizedCoords = true;
            AddressingMode = CL_ADDRESSING_MODE.CLAMP_TO_EDGE;
            FilterMode = CL_FILTER_MODE.LINEAR;
            //MipFilterMode = CL_SAMPLER_FILTER_MODE.LINEAR;
            //LODMin = 0;
            //LODMax = CL.MAX_FLOAT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalizeCoords"></param>
        /// <param name="mode"></param>
        /// <param name="filter"></param>
        public CLSamplerProperties(bool normalizeCoords, CL_ADDRESSING_MODE mode, CL_FILTER_MODE filter)
        {
            NormalizedCoords = normalizeCoords;
            AddressingMode = mode;
            FilterMode = filter;
            //MipFilterMode = CL_SAMPLER_FILTER_MODE.LINEAR;
            //LODMin = 0;
            //LODMax = CL.MAX_FLOAT;
        }

        /// <summary>
        /// 
        /// </summary>
        public static CLSamplerProperties Default
        {
            get
            {
                var param = new CLSamplerProperties();
                return param;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLSamplerProperties: NormalizedCoords={0}, AddressingMode={1}, FilterMode={2}]",
                NormalizedCoords, AddressingMode, FilterMode);
        }

    }
}
