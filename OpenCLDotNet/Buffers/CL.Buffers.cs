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
        /// <param name="flags"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_mem CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            out CL_ERROR error)
        {
            return CL_CreateEmptyBuffer(context, flags, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="byte_size"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_mem CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            uint byte_size,
            Array data,  
            out CL_ERROR error)
        {
            byte[] bytes = null;
            if(data != null)
            {
                bytes = new byte[byte_size];
                Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
            }

            return CL_CreateBuffer(context, flags, byte_size, bytes, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="flags"></param>
        /// <param name="region"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_mem CreateSubBuffer(
            cl_mem buffer,
            CL_MEM_FLAGS flags,
            CLRegion1t region,
            out CL_ERROR error)
        {
            var type = CL_BUFFER_CREATION_TYPE.REGION;
            return CL_CreateSubBuffer(buffer, flags, type, region, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memobj"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetMemObjectInfoSize(
            cl_mem memobj,
            CL_MEM_INFO name,
            out uint size)
        {
            size_t sizet;
            var err = CL_GetMemObjectInfoSize(memobj, name, out sizet);
            size = (uint)sizet;
            return err;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memobj"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UInt64 info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memobj"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UIntPtr info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memobj"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public static cl_int RetainMemObject(cl_mem mem)
        {
            return CL_RetainMemObject(mem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public static cl_int ReleaseMemObject(cl_mem mem)
        {
            return CL_ReleaseMemObject(mem);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateEmptyBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            size_t size,
            byte[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateSubBuffer(
            cl_mem buffer,
            CL_MEM_FLAGS flags,
            CL_BUFFER_CREATION_TYPE buffer_create_type,
            CLRegion1t region,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetMemObjectInfoSize(
            cl_mem memobj,
            CL_MEM_INFO name,
            [Out] out size_t size);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            size_t size,
            [Out] out UInt64 info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            size_t size,
            [Out] out UIntPtr info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            size_t size,
            [Out] out cl_object info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_RetainMemObject(cl_mem mem);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_ReleaseMemObject(cl_mem mem);

    }
}
