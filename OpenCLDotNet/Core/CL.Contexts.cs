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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="num_devices"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static cl_context CreateContext(
            cl_platform_id platform,
            uint num_devices,
            cl_device_id[] devices)
        {
            var properties = new UInt64[]
            {
                (UInt64)CL_CONTEXT_PROPERTIES.PLATFORM,
                (UInt64)platform.Value,
                0
            };

            return CL_CreateContext(properties, num_devices, devices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetContextInfoSize(
            cl_context context,
            CL_CONTEXT_INFO name,
            [Out] out uint size)
        {
            size_t num;
            var error = CL_GetContextInfoSize(context, name, out num);

            size = (uint)num.Value;
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetContextInfo(
            cl_context context,
            CL_CONTEXT_INFO name,
            uint size,
            [Out] cl_object[] info)
        {
            return CL_GetContextInfo(context, name, size, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetContextInfo(
            cl_context context,
            CL_CONTEXT_INFO name,
            uint size,
            [Out] out UInt64 info)
        {
            return CL_GetContextInfo(context, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static CL_ERROR RetainContext(cl_context context)
        {
            return CL_RetainContext(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static CL_ERROR ReleaseContext(cl_context context)
        {
            return CL_ReleaseContext(context);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_context CL_CreateContext(
            UInt64[] properties,
            cl_uint num_devices,
            cl_device_id[] devices);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetContextInfoSize(
            cl_context context,
            CL_CONTEXT_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetContextInfo(
            cl_context context,
            CL_CONTEXT_INFO param_name,
            size_t param_value_size,
            [Out] cl_object[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetContextInfo(
            cl_context context,
            CL_CONTEXT_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainContext(cl_context context);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseContext(cl_context context);
    }
}
