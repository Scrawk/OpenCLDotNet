using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="format"></param>
        /// <param name="desc"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_mem CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            CL_MEM_DATA_TYPE type,
            Array data,
            out CL_ERROR error)
        {
            switch (type)
            {
                case CL_MEM_DATA_TYPE.FLOAT:
                    var f_data = data as float[];
                    return CL_CreateImage(context, flags, format, desc, f_data, out error);

                case CL_MEM_DATA_TYPE.INT:
                    var i_data = data as int[];
                    return CL_CreateImage(context, flags, format, desc, i_data, out error);

                case CL_MEM_DATA_TYPE.UINT:
                    var ui_data = data as uint[];
                    return CL_CreateImage(context, flags, format, desc, ui_data, out error);

                case CL_MEM_DATA_TYPE.SHORT:
                    var s_data = data as short[];
                    return CL_CreateImage(context, flags, format, desc, s_data, out error);

                case CL_MEM_DATA_TYPE.USHORT:
                    var us_data = data as ushort[];
                    return CL_CreateImage(context, flags, format, desc, us_data, out error);

                case CL_MEM_DATA_TYPE.BYTE:
                    var b_data = data as byte[];
                    return CL_CreateImage(context, flags, format, desc, b_data, out error);

                case CL_MEM_DATA_TYPE.SBYTE:
                    var sb_data = data as sbyte[];
                    return CL_CreateImage(context, flags, format, desc, sb_data, out error);

                default:
                    error = CL_ERROR.INVALID_DATA_TYPE;
                    return new cl_mem();

            }

            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            float[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            int[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            uint[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            short[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            ushort[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            byte[] data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            sbyte[] data,
            [Out] out CL_ERROR error);
    }
}
