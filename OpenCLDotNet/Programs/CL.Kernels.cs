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
        /// <param name="program"></param>
        /// <param name="kernel_name"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_kernel CreateKernel(
            cl_program program,
            cl_char[] kernel_name,
            [Out] out CL_ERROR error)
        {
            cl_kernel kernel = CL_CreateKernel(program, kernel_name, out error);
            return kernel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            cl_mem arg_value)
        {  
            return CL_SetKernelArgMem(kernel, arg_index, arg_value); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            cl_sampler arg_value)
        {
            return CL_SetKernelArgSampler(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            double arg_value)
        {
            return CL_SetKernelArgDouble(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            float arg_value)
        {
            return CL_SetKernelArgFloat(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            Half arg_value)
        {
            return CL_SetKernelArgHalf(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            long arg_value)
        {
            return CL_SetKernelArgLong(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            ulong arg_value)
        {
            return CL_SetKernelArgULong(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            int arg_value)
        {
            return CL_SetKernelArgInt(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            uint arg_value)
        {
            return CL_SetKernelArgUInt(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            short arg_value)
        {
            return CL_SetKernelArgShort(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            ushort arg_value)
        {
            return CL_SetKernelArgUShort(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            sbyte arg_value)
        {
            return CL_SetKernelArgSByte(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="arg_index"></param>
        /// <param name="arg_value"></param>
        /// <returns></returns>
        public static CL_ERROR SetKernelArg(
            cl_kernel kernel,
            uint arg_index,
            byte arg_value)
        {
            return CL_SetKernelArgByte(kernel, arg_index, arg_value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelInfoSize(
            cl_kernel kernel,
            CL_KERNEL_INFO name,
            [Out] out uint size)
        {
            size_t sizet;
            var err = CL_GetKernelInfoSize(kernel, name, out sizet);

            size = (uint)sizet;
            return err;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO name,
            uint size,
            [Out] out UInt64 info)
        {
            return CL_GetKernelInfo(kernel, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO name,
            uint size,
            [Out] out cl_object info)
        {
            return CL_GetKernelInfo(kernel, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO name,
            uint size,
            [Out] cl_char[] info)
        {
            return CL_GetKernelInfo(kernel, name, size, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelArgInfoSize(
            cl_kernel kernel,
            uint index,
            CL_KERNEL_ARG_INFO name,
            [Out] out uint size)
        {
            size_t sizet;
            var err = CL_GetKernelArgInfoSize(kernel, index, name, out sizet);

            size = (uint)sizet;
            return err;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelArgInfo(
            cl_kernel kernel,
            uint index,
            CL_KERNEL_ARG_INFO name,
            uint size,
            [Out] out UInt64 info)
        {
            return CL_GetKernelArgInfo(kernel, index, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelArgInfo(
            cl_kernel kernel,
            uint index,
            CL_KERNEL_ARG_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetKernelArgInfo(kernel, index, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelArgInfo(
            cl_kernel kernel,
            uint index,
            CL_KERNEL_ARG_INFO name,
            uint size,
            cl_char[] info)
        {
            return CL_GetKernelArgInfo(kernel, index, name, size, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelWorkGroupInfoSize(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO name,
            [Out] out uint size)
        {
            size_t sizet;
            var err = CL_GetKernelWorkGroupInfoSize(kernel, device, name, out sizet);

            size = (uint)sizet;
            return err;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelWorkGroupInfo(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO name,
            uint size,
            [Out] out UInt64 info)
        {
            return CL_GetKernelWorkGroupInfo(kernel, device, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetKernelWorkGroupInfo(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO name,
            uint size,
            [Out] size_t[] info)
        {
            return CL_GetKernelWorkGroupInfo(kernel, device, name, size, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns></returns>
        public static CL_ERROR RetainKernel(cl_kernel kernel)
        {
            return CL_RetainKernel(kernel); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns></returns>
        public static CL_ERROR ReleaseKernel(cl_kernel kernel)
        {
            return CL_ReleaseKernel(kernel);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_kernel CL_CreateKernel(
            cl_program program,
            cl_char[] kernel_name,
            [Out] out CL_ERROR errcode_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgMem(
            cl_kernel kernel,
            cl_uint arg_index,
            cl_mem arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgSampler(
            cl_kernel kernel,
            cl_uint arg_index,
            cl_sampler arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgDouble(
            cl_kernel kernel,
            cl_uint arg_index,
            double arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgFloat(
            cl_kernel kernel,
            cl_uint arg_index,
            float arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgHalf(
            cl_kernel kernel,
            cl_uint arg_index,
            Half arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgLong(
            cl_kernel kernel,
            cl_uint arg_index,
            long arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgULong(
            cl_kernel kernel,
            cl_uint arg_index,
            ulong arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgInt(
            cl_kernel kernel,
            cl_uint arg_index,
            int arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgUInt(
            cl_kernel kernel,
            cl_uint arg_index,
            uint arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgShort(
            cl_kernel kernel,
            cl_uint arg_index,
            short arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgUShort(
            cl_kernel kernel,
            cl_uint arg_index,
            ushort arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgSByte(
            cl_kernel kernel,
            cl_uint arg_index,
            sbyte arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetKernelArgByte(
            cl_kernel kernel,
            cl_uint arg_index,
            byte arg_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelInfoSize(
            cl_kernel kernel,
            CL_KERNEL_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO param_name,
            size_t param_value_size,
            [Out] out cl_object param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelInfo(
            cl_kernel kernel,
            CL_KERNEL_INFO param_name,
            size_t param_value_size,
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelArgInfoSize(
            cl_kernel kernel,
            cl_uint arg_indx,
            CL_KERNEL_ARG_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelArgInfo(
            cl_kernel kernel,
            cl_uint arg_indx,
            CL_KERNEL_ARG_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelArgInfo(
            cl_kernel kernel,
            cl_uint arg_indx,
            CL_KERNEL_ARG_INFO param_name,
            size_t param_value_size,
            [Out] out cl_object param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelArgInfo(
            cl_kernel kernel,
            cl_uint arg_indx,
            CL_KERNEL_ARG_INFO param_name,
            size_t param_value_size,
            [Out] cl_char[] param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelWorkGroupInfoSize(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO param_name,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelWorkGroupInfo(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO param_name,
            size_t param_value_size,
            [Out] out UInt64 param_value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelWorkGroupInfo(
            cl_kernel kernel,
            cl_device_id device,
            CL_KERNEL_WORK_GROUP_INFO param_name,
            size_t param_value_size,
            [Out] size_t[] param_value);

        /*
        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelSubGroupInfoSize(
            cl_kernel kernel,
            cl_device_id device,
            cl_kernel_sub_group_info param_name,
            size_t input_value_size,
            UInt64 input_value,
            [Out] out size_t param_value_size_ret);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetKernelSubGroupInfo(
            cl_kernel kernel,
            cl_device_id device,
            cl_kernel_sub_group_info param_name,
            size_t input_value_size,
            UInt64 input_value,
            size_t param_value_size,
            [Out] out UInt64 param_value);
        */

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_kernel CL_CloneKernel(
            cl_kernel kernel,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainKernel(cl_kernel kernel);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseKernel(cl_kernel kernel);
    }
}
