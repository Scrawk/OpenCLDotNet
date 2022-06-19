﻿using System;
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

        public static cl_mem CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            Array data,
            out CL_ERROR error)
        {
            size_t size = 0;

            if(data != null)
                size = (size_t)(sizeof(float) * data.Length);

            return CL_CreateBuffer(context, flags, size, data, out error);
        }

        public static cl_mem CreateSubBuffer(
            cl_mem buffer,
            CL_MEM_FLAGS flags,
            CLBufferRegion region,
            out CL_ERROR error)
        {
            var type = CL_BUFFER_CREATION_TYPE.REGION;
            return CL_CreateSubBuffer(buffer, flags, type, region, out error);
        }

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

        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UInt64 info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out UIntPtr info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        public static CL_ERROR GetMemObjectInfo(
            cl_mem memobj,
            CL_MEM_INFO name,
            uint size,
            out cl_object info)
        {
            return CL_GetMemObjectInfo(memobj, name, size, out info);
        }

        public static cl_int RetainMemObject(cl_mem mem)
        {
            return CL_RetainMemObject(mem);
        }

        public static cl_int ReleaseMemObject(cl_mem mem)
        {
            return CL_ReleaseMemObject(mem);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            size_t size,
            Array data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateSubBuffer(
            cl_mem buffer,
            CL_MEM_FLAGS flags,
            CL_BUFFER_CREATION_TYPE buffer_create_type,
            CLBufferRegion region,
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