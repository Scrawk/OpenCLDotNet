using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("OpenGLDotNetConsole")]
[assembly: InternalsVisibleTo("OpenGLDotNetTest")]

namespace OpenCLDotNet
{
    public static class CL
    {

        private const string DLL_NAME = "OpenCLWrapper.dll";

        private const CallingConvention CDECL = CallingConvention.Cdecl;

        /// <summary>
        /// 
        /// </summary>
        public static string Version
        {
            get
            {
                return CL_VersionNumber().ToString();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num_entries"></param>
        /// <param name="platforms"></param>
        /// <param name="num_platforms"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformIDs(
            cl_uint num_entries,
            cl_platform_id[] platforms,
            cl_uint[] num_platforms)
        {
            return CL_GetPlatformIDs(num_entries, platforms, num_platforms);    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="param_name"></param>
        /// <param name="param_value_size"></param>
        /// <param name="param_value"></param>
        /// <param name="param_value_size_ret"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformInfo(
         cl_platform_id platform,
         cl_platform_info param_name,
         size_t param_value_size,
         IntPtr param_value,
         size_t[] param_value_size_ret)
        {
            return CL_GetPlatformInfo(platform, param_name, param_value_size, 
                param_value, param_value_size_ret);
        }



        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern int CL_VersionNumber();

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformIDs(
            cl_uint num_entries, 
            cl_platform_id[] platforms, 
            [Out] cl_uint[] num_platforms);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformInfo(
            cl_platform_id platform,
            cl_platform_info param_name, 
            size_t param_value_size, 
            IntPtr param_value, 
            [Out] size_t[] param_value_size_ret);

    }
}
