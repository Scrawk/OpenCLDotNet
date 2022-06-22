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
        public static CL_ERROR EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            bool blocking_read,
            uint offset,
            uint size,
            Array data,
            CL_MEM_DATA_TYPE type,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            switch (type)
            {
                case CL_MEM_DATA_TYPE.FLOAT:
                    var f_data = data as float[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, f_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.INT:
                    var i_data = data as int[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, i_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.UINT:
                    var ui_data = data as uint[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, ui_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SHORT:
                    var s_data = data as short[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, s_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.USHORT:
                    var us_data = data as ushort[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, us_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.BYTE:
                    var b_data = data as byte[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, b_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SBYTE:
                    var sb_data = data as sbyte[];
                    return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, sb_data,
                        wait_list_size, wait_list, out _event);

                default:
                    _event = new cl_event();
                    return CL_ERROR.INVALID_DATA_TYPE;
            }


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////


        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
           cl_command_queue command,
           cl_mem buffer,
           cl_bool blocking_read,
           size_t offset,
           size_t size,
           [Out] float[] ptr,
           cl_uint wait_list_size,
           [Out] cl_event[] wait_list,
           [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] int[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] uint[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] short[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] ushort[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] byte[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            [Out] sbyte[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);
    }
}
