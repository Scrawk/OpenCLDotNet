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
        /// <param name="num_platforms"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformCount(
            out uint num_platforms)
        {
            cl_uint platforms;
            var error_code = CL_GetPlatformCount(out platforms);

            num_platforms = platforms;
            return error_code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num_entries"></param>
        /// <param name="platform_ids"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformIDs(
            uint num_entries,
            cl_platform_id[] platform_ids)
        {
            return CL_GetPlatformIDs(num_entries, platform_ids);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform_ids"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformIDs(
            List<cl_platform_id> platform_ids)
        {
            CL_ERROR error_code;
            uint num_platforms;

            error_code = GetPlatformCount(out num_platforms);

            if (error_code != CL_ERROR.SUCCESS || num_platforms <= 0)
                return error_code;

            var platforms = new cl_platform_id[num_platforms];
            error_code = GetPlatformIDs(num_platforms, platforms);

            platform_ids.AddRange(platforms);
            return error_code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformInfoSize(
         cl_platform_id platform,
         CL_PLATFORM_INFO name,
         out uint info_size)
        {
            size_t size;
            var error_code = CL_GetPlatformInfoSize(platform, name, out size);

            info_size = (uint)size;
            return error_code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info_array"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformInfo(
            cl_platform_id platform,
            CL_PLATFORM_INFO name,
            uint info_size,
            cl_char[] info_array)
        {
            return CL_GetPlatformInfo(platform, name, info_size, info_array);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="name"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformInfo(
            cl_platform_id platform,
            CL_PLATFORM_INFO name,
            out string info)
        {
            info = "";
            uint size;
            var error_code = GetPlatformInfoSize(platform, name, out size);

            if (error_code != CL_ERROR.SUCCESS || size <= 0)
                return error_code;

            var info_array = new cl_char[size];
            error_code = CL_GetPlatformInfo(platform, name, size, info_array);

            info = info_array.ToText();
            return error_code;
        }

        public static CL_ERROR UnloadPlatformCompiler(cl_platform_id platform)
        {
            return CL_UnloadPlatformCompiler(platform);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformCount(
            [Out] out cl_uint num_platforms);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformIDs(
            cl_uint num_entries,
            [Out] cl_platform_id[] platforms);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformInfoSize(
            cl_platform_id platform,
            CL_PLATFORM_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformInfo(
            cl_platform_id platform,
            CL_PLATFORM_INFO param_name,
            size_t param_value_size,
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_UnloadPlatformCompiler(cl_platform_id platform);
    }
}
