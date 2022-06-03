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

        public static CL_ERROR GetPlatformIDs(out uint num_platforms)
        {
            var platforms = new cl_uint[1];
            var error_code = CL_GetPlatformIDs(0, null, platforms);

            num_platforms = platforms[0].Value;
            return error_code;
        }

        public static CL_ERROR GetPlatformIDs(uint num_entries, cl_platform_id[] platformIds)
        {
            return CL_GetPlatformIDs(num_entries, platformIds, null);
        }

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
            [In] [Out] cl_platform_id[] platforms, 
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
