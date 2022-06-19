using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public struct CLSamplerProperties
    {
        public cl_bool NormalizedCoords;

        public CL_SAMPLER_ADDRESSING_MODE AddressingMode;

        public CL_SAMPLER_FILTER_MODE FilterMode;

        //public CL_SAMPLER_FILTER_MODE MipFilterMode;

        //public cl_float LODMin;

        //public cl_float LODMax;

        public CLSamplerProperties()
        {
            NormalizedCoords = cl_bool.True;
            AddressingMode = CL_SAMPLER_ADDRESSING_MODE.CLAMP_TO_EDGE;
            FilterMode = CL_SAMPLER_FILTER_MODE.LINEAR;
            //MipFilterMode = CL_SAMPLER_FILTER_MODE.LINEAR;
            //LODMin = 0;
            //LODMax = CL.MAX_FLOAT;
        }

        public static CLSamplerProperties Default
        {
            get
            {
                var param = new CLSamplerProperties();
                return param;
            }
        }

        public override string ToString()
        {
            return String.Format("[CLSamplerProperties: NormalizedCoords={0}, AddressingMode={1}, FilterMode={2}]",
                NormalizedCoords, AddressingMode, FilterMode);
        }

    }
}
