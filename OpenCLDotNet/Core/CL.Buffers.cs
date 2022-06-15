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

        public static cl_mem CreateBuffer(
            cl_context context,
            CL_MEM_FLAGS flags,
            float[] data,
            out CL_ERROR error)
        {
            size_t size = (size_t)(sizeof(float) * data.Length);
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
            float[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateSubBuffer(
            cl_mem buffer,
            CL_MEM_FLAGS flags,
            CL_BUFFER_CREATION_TYPE buffer_create_type,
            CLBufferRegion region,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_RetainMemObject(cl_mem mem);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_int CL_ReleaseMemObject(cl_mem mem);

    }
}
