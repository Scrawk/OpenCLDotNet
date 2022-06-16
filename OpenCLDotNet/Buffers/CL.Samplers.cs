using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_sampler CL_CreateSamplerWithProperties(
            cl_context context,
            cl_sampler_properties[] sampler_properties,
            [Out] out CL_ERROR errcode_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_GetSamplerInfoSize(
            cl_sampler sampler,
            cl_sampler_info param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_GetSamplerInfo(
            cl_sampler sampler,
            cl_sampler_info param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_RetainSampler(cl_sampler sampler);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_ReleaseSampler(cl_sampler sampler);
    }
}
