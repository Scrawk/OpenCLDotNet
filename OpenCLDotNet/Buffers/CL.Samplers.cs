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

        public static cl_sampler CreateSamplerWithProperties(
            cl_context context,
            UInt64[] properties,
            out CL_ERROR error)
        {
            return CL_CreateSamplerWithProperties(context, properties, out error);
        }

        public static CL_ERROR GetSamplerInfoSize(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            out uint size)
        {
            size_t sizet;
            var error = CL_GetSamplerInfoSize(sampler, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        public static CL_ERROR GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            uint size,
            out UInt64 info)
        {
            return CL_GetSamplerInfo(sampler, name, size, out info);  
        }

        public static CL_ERROR GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            uint size,
            out float info)
        {
            cl_float _info;
            var error = CL_GetSamplerInfo(sampler, name, size, out _info);

            info = _info;
            return error;
        }

        public static CL_ERROR GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetSamplerInfo(sampler, name, size, out info);
        }

        public static CL_ERROR RetainSampler(cl_sampler sampler)
        {
            return CL_RetainSampler(sampler);   
        }

        public static CL_ERROR ReleaseSampler(cl_sampler sampler)
        {
            return CL_ReleaseSampler(sampler);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_sampler CL_CreateSamplerWithProperties(
            cl_context context,
            UInt64[] sampler_properties,
            [Out] out CL_ERROR errcode_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSamplerInfoSize(
            cl_sampler sampler,
            CL_SAMPLER_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO param_name,
            size_t param_value_size,
            [Out] out cl_float param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO param_name,
            size_t param_value_size,
            [Out] out cl_object param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainSampler(cl_sampler sampler);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseSampler(cl_sampler sampler);
    }
}
