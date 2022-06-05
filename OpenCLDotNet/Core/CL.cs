﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("OpenGLDotNetConsole")]
[assembly: InternalsVisibleTo("OpenGLDotNetTest")]

namespace OpenCLDotNet.Core
{
    public static class CL
    {
        /// <summary>
        /// 
        /// </summary>
        private const string DLL_NAME = "OpenCLWrapper.dll";

        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="num_platforms"></param>
        /// <returns></returns>
        public static CL_ERROR GetPlatformIDs(
            out uint num_platforms)
        {
            cl_uint platforms;
            var error_code = CL_GetPlatformIDs(0, null, out platforms);

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
            cl_uint num_platforms;
            return CL_GetPlatformIDs(num_entries, platform_ids, out num_platforms);
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

            error_code = GetPlatformIDs(out num_platforms);

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
        public static CL_ERROR GetPlatformInfo(
         cl_platform_id platform,
         CL_PLATFORM_INFO name,
         out uint info_size)
        {
            size_t size;
            var error_code = CL_GetPlatformInfo(platform, name, 0, null, out size);

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
            char[] info_array)
        {
            size_t size;
            return CL_GetPlatformInfo(platform, name, info_size, info_array, out size);
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
            uint info_size;
            var error_code = GetPlatformInfo(platform, name, out info_size);

            if (error_code != CL_ERROR.SUCCESS || info_size <= 0)
                return error_code;

            size_t size;
            char[] info_array = new char[info_size];
            error_code = CL_GetPlatformInfo(platform, name, info_size, info_array, out size);

            info = new string(info_array);
            return error_code;
        }

        public static CL_ERROR GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            out uint num_devices)
        {
            cl_uint num;
            var error_code = CL_GetDeviceIDs(platform, device_type, 0, null, out num);

            num_devices = num;
            return error_code;
        }

        public static CL_ERROR GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            uint num_devices,
            cl_device_id[] devices)
        {
            cl_uint num;
            return CL_GetDeviceIDs(platform, device_type, num_devices, devices, out num);
        }

        public static CL_ERROR GetDeviceInfoSize(
            cl_device_id device,
            CL_DEVICE_INFO name,
            out uint info_size)
        {
            size_t num;
            var error = CL_GetDeviceInfoSize(device, name, out num);

            info_size = (uint)num;
            return error;
        }

        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            out UInt64 info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, out info);
            return error;
        }

        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            char[] info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, info);
            return error;
        }

        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            size_t[] info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, info);
            return error;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern int CL_VersionNumber();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num_entries"></param>
        /// <param name="platforms"></param>
        /// <param name="num_platforms"></param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformIDs(
            cl_uint num_entries,
            [In] [Out] cl_platform_id[] platforms, 
            [Out] out cl_uint num_platforms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="param_name"></param>
        /// <param name="param_value_size"></param>
        /// <param name="param_value"></param>
        /// <param name="param_value_size_ret"></param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetPlatformInfo(
            cl_platform_id platform,
            CL_PLATFORM_INFO param_name, 
            size_t param_value_size, 
            [Out] char[] param_value, 
            [Out] out size_t param_value_size_ret);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="device_type"></param>
        /// <param name="num_entries"></param>
        /// <param name="devices"></param>
        /// <param name="num_devices"></param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            cl_uint num_entries,
            [In][Out] cl_device_id[] devices,
            [Out] out cl_uint num_devices);


        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfoSize(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] size_t[] param_value);

    }
}
