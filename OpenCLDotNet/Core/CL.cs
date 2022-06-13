using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Utility;

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

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                PLATFORM FUNCTIONS                                         ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

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

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                DEVICE FUNCTIONS                                           ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

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
        //                                CONTEXT FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

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
        public static  CL_ERROR GetContextInfo(
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
        ///                                 PROGRAM FUNCTIONS                                        ///
        ///////////////////////////////////////////////////////////////////////////////////////////////


        public static cl_program CreateProgramWithSource(
            cl_context context,
            string program_text,
            out CL_ERROR error_code)
        {

            var program_char = program_text.ToCLCharArray();
            size_t length = (size_t)program_char.Length;

            cl_int error;
            var program = CL_CreateProgramWithSource(context, 1, program_char, length, out error);
            error_code = (CL_ERROR)error.Value;

            return program;
        }

        public static CL_ERROR BuildProgram(
            cl_program program,
            uint num_devices,
            cl_device_id[] device_list,
            string options)
        {
            cl_char[] options_char = null;
            if(!string.IsNullOrEmpty(options))
                options_char = options.ToCLCharArray();

            return CL_BuildProgram(program, num_devices, device_list, options_char);
        }

        public static CL_ERROR GetProgramBuildInfoSize(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO name,
            out uint size)
        {
            size_t sizet;
            var error =  CL_GetProgramBuildInfoSize(program, device, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        public static CL_ERROR GetProgramBuildInfo(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO name,
            uint size,
            out UInt64 value)
        {
            var error = CL_GetProgramBuildInfo(program, device, name, size, out value);
            return error;
        }

        public static CL_ERROR GetProgramBuildInfo(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO name,
            uint size,
            cl_char[] info)
        {
            var error = CL_GetProgramBuildInfo(program, device, name, size, info);
            return error;
        }

        public static CL_ERROR GetProgramInfoSize(
            cl_program program,
            CL_PROGRAM_INFO name,
            out uint size)
        {
            size_t sizet;
            var error = CL_GetProgramInfoSize(program, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        public static CL_ERROR GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO name,
            uint size,
            out UInt64 value)
        {
            var error = CL_GetProgramInfo(program, name, size, out value);
            return error;
        }

        public static CL_ERROR GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO name,
            uint size,
            cl_char[] info)
        {
            var error = CL_GetProgramInfo(program, name, size, info);
            return error;
        }

        public static CL_ERROR RetainProgram(cl_program program)
        {
            return CL_RetainProgram(program);
        }

        public static CL_ERROR ReleaseProgram(cl_program program)
        {
            return CL_ReleaseProgram(program);
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern int CL_VersionNumber();

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                PLATFORM FUNCTIONS                                         ///
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

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                DEVICE FUNCTIONS                                           ///
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


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                CONTEXT FUNCTIONS                                          ///
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

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                PROGRAM FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_program CL_CreateProgramWithSource(
            cl_context context,
            cl_uint count,
            cl_char[] strings,
            size_t length,
            [Out] out cl_int errcode_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_BuildProgram(
            cl_program program,
            cl_uint num_devices,
            cl_device_id[] device_list,
            cl_char[] options);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramBuildInfoSize(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramBuildInfo(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramBuildInfo(
            cl_program program,
            cl_device_id device,
            CL_PROGRAM_BUILD_INFO param_name,
            size_t param_value_size,
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramInfoSize(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            size_t param_value_size,
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainProgram(cl_program program);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseProgram(cl_program program);

    }
}
