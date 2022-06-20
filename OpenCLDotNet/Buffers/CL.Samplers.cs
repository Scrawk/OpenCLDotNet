using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_sampler CreateSamplerWithProperties(
            cl_context context,
            CLSamplerProperties properties,
            out CL_ERROR error)
        {
            return CL_CreateSamplerWithProperties(context, properties, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            uint size,
            out UInt64 info)
        {
            return CL_GetSamplerInfo(sampler, name, size, out info);  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetSamplerInfo(
            cl_sampler sampler,
            CL_SAMPLER_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetSamplerInfo(sampler, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <returns></returns>
        public static CL_ERROR RetainSampler(cl_sampler sampler)
        {
            return CL_RetainSampler(sampler);   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampler"></param>
        /// <returns></returns>
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
            CLSamplerProperties sampler_properties,
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
