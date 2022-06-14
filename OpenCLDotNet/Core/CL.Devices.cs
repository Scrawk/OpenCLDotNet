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
        /// <param name="device_type"></param>
        /// <param name="num_devices"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceCount(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            out uint num_devices)
        {
            cl_uint num;
            var error_code = CL_GetDeviceCount(platform, device_type, out num);

            num_devices = num;
            return error_code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="device_type"></param>
        /// <param name="num_devices"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            uint num_devices,
            cl_device_id[] devices)
        {
            return CL_GetDeviceIDs(platform, device_type, num_devices, devices);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="device_type"></param>
        /// <param name="device_ids"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            List<cl_device_id> device_ids)
        {
            CL_ERROR error_code;
            uint num_devices;

            error_code = GetDeviceCount(platform, device_type, out num_devices);

            if (error_code != CL_ERROR.SUCCESS || num_devices <= 0)
                return error_code;

            var devices = new cl_device_id[num_devices];
            error_code = GetDeviceIDs(platform, device_type, num_devices, devices);

            device_ids.AddRange(devices);
            return error_code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfoSize(
            cl_device_id device,
            CL_DEVICE_INFO name,
            out uint size)
        {
            size_t num;
            var error = CL_GetDeviceInfoSize(device, name, out num);

            size = (uint)num;
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            out UInt64 info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, out info);
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            cl_char[] info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, info);
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            size_t[] info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, info);
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            out cl_object info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, out info);
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="info_size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO name,
            uint info_size,
            cl_object[] info)
        {
            var error = CL_GetDeviceInfo(device, name, info_size, info);
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static CL_ERROR RetainDevice(cl_device_id device)
        {
            return CL_RetainDevice(device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static CL_ERROR ReleaseDevice(cl_device_id device)
        {
            return CL_ReleaseDevice(device);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceIDs(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
            cl_uint num_entries,
            [In][Out] cl_device_id[] devices);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceCount(
            cl_platform_id platform,
            CL_DEVICE_TYPE device_type,
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
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] size_t[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] out cl_object param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetDeviceInfo(
            cl_device_id device,
            CL_DEVICE_INFO param_name,
            size_t param_value_size,
            [Out] cl_object[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainDevice(cl_device_id device);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseDevice(cl_device_id device);
    }
}
