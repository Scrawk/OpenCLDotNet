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
        /// Creates a empty buffer.
        /// </summary>
        /// <param name="context">context is a valid OpenCL context used to create the buffer object.</param>
        /// <param name="flags">flags is a bit-field that is used to specify allocation and usage
        /// information such as the memory arena that should be used to allocate the buffer object
        /// and how it will be used. The Memory Flags table describes the possible values for flags. 
        /// If the value specified for flags is 0, the default is used which is CL_​MEM_​READ_​WRITE.</param>
        /// <param name="error">The error code.</param>
        /// <returns>The buffers id.</returns>
        public static cl_mem CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            out CL_ERROR error)
        {
            return CL_CreateEmptyBuffer(context, flags, out error);
        }

        /// <summary>
        /// Creates a buffer filled with the data arrays contents. Data maybe null to create a empty buffer.
        /// </summary>
        /// <param name="context">context is a valid OpenCL context used to create the buffer object.</param>
        /// <param name="flags">flags is a bit-field that is used to specify allocation and usage
        /// information such as the memory arena that should be used to allocate the buffer object
        /// and how it will be used. The Memory Flags table describes the possible values for flags. 
        /// If the value specified for flags is 0, the default is used which is CL_​MEM_​READ_​WRITE.</param>
        /// <param name="byte_size">size is the size in bytes of the buffer memory object to be allocated.</param>
        /// <param name="data">data is a pointer to the buffer data that may already be allocated by the application. 
        /// The size of the buffer that data points to must be ≥ size bytes.</param>
        /// <param name="error">The error code.</param>
        /// <returns>The buffers id.</returns>
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
        /// Gets the siz in bytes of the mem object ie buffers and images.
        /// </summary>
        /// <param name="memobj">The mem objects id.</param>
        /// <param name="name">The infos name.</param>
        /// <param name="size">The objects size in bytes.</param>
        /// <returns>The error code.</returns>
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
        /// Gets the value for a UInt64 info property of the mem object.
        /// </summary>
        /// <param name="memobj">The mem objects id.</param>
        /// <param name="name">The info properties name.</param>
        /// <param name="size">The size in bytes of the object.</param>
        /// <param name="info">The info objects value.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UInt64 info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        /// <summary>
        /// Gets the value for a UIntPtr info property of the mem object.
        /// </summary>
        /// <param name="memobj">The mem objects id.</param>
        /// <param name="name">The info properties name.</param>
        /// <param name="size">The size in bytes of the object.</param>
        /// <param name="info">The info objects value.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UIntPtr info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        //// <summary>
        /// Gets the value for a cl_object info property of the mem object.
        /// </summary>
        /// <param name="memobj">The mem objects id.</param>
        /// <param name="name">The info properties name.</param>
        /// <param name="size">The size in bytes of the object.</param>
        /// <param name="info">The info objects value.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        /// <summary>
        /// Increment the reference counter for the mem object.
        /// </summary>
        /// <param name="mem">The mem objects id.</param>
        /// <returns>The error code.</returns>
        public static cl_int RetainMemObject(cl_mem mem)
        {
            return CL_RetainMemObject(mem);
        }

       // <summary>
        /// Decrement the reference counter for the mem object.
        /// </summary>
        /// <param name="mem">The mem objects id.</param>
        /// <returns>The error code.</returns>
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
