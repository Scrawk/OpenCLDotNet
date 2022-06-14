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

        public static cl_program CreateProgramWithBinary(
            cl_context context,
            cl_uint num_devices,
            cl_device_id[] device_list,
            size_t[] lengths,
            byte[] binaries,
            CL_ERROR[] binary_status,
            out CL_ERROR errcode)
        {
            return CL_CreateProgramWithBinary(context, num_devices, device_list,
                    lengths, binaries, binary_status, out errcode);
        }

        public static CL_ERROR BuildProgram(
            cl_program program,
            uint num_devices,
            cl_device_id[] device_list,
            string options)
        {
            cl_char[] options_char = null;
            if (!string.IsNullOrEmpty(options))
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
            var error = CL_GetProgramBuildInfoSize(program, device, name, out sizet);

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

        public static CL_ERROR GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO name,
            uint size,
            out cl_object info)
        {
            var error = CL_GetProgramInfo(program, name, size, out info);
            return error;
        }

        public static CL_ERROR GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO name,
            uint size,
            cl_object[] info)
        {
            var error = CL_GetProgramInfo(program, name, size, info);
            return error;
        }

        public static CL_ERROR GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO name,
            uint size,
            size_t[] info)
        {
            var error = CL_GetProgramInfo(program, name, size, info);
            return error;
        }

        public static CL_ERROR GetProgramBinaries(
            cl_program program,
            int num_devices,
            size_t[] sizes,
            byte[] binaries)
        {
            return CL_GetProgramBinaries(program, num_devices, sizes, binaries);
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
        private static extern cl_program CL_CreateProgramWithSource(
            cl_context context,
            cl_uint count,
            cl_char[] strings,
            size_t length,
            [Out] out cl_int errcode_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_program CL_CreateProgramWithBinary(
            cl_context context,
            cl_uint num_devices,
            cl_device_id[] device_list,
            size_t[] lengths,
            byte[] binaries,
            [Out] CL_ERROR[] binary_status,
            [Out] out CL_ERROR errcode_ret);

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
        private static extern CL_ERROR CL_GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            size_t param_value_size,
            [Out] out cl_object param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            size_t param_value_size,
            [Out] cl_object[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramInfo(
            cl_program program,
            CL_PROGRAM_INFO param_name,
            size_t param_value_size,
            [Out] size_t[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetProgramBinaries(
            cl_program program,
            int num_devices,
            size_t[] sizes,
            [Out] byte[] binaries);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainProgram(cl_program program);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseProgram(cl_program program);

    }
}
