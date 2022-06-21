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
            cl_bool blocking_read,
            size_t offset,
            size_t size,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, ptr, 
                wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueReadBufferRect(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            CLPoint3t buffer_origin,
            CLPoint3t host_origin,
            CLPoint3t region,
            size_t buffer_row_pitch,
            size_t buffer_slice_pitch,
            size_t host_row_pitch,
            size_t host_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueReadBufferRect(command, buffer, blocking_read, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueWriteBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            size_t offset,
            size_t size,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueWriteBuffer(command, buffer, blocking_write, offset, size, ptr,
                wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueWriteBufferRect(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            CLPoint3t buffer_origin,
            CLPoint3t host_origin,
            CLPoint3t region,
            size_t buffer_row_pitch,
            size_t buffer_slice_pitch,
            size_t host_row_pitch,
            size_t host_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueWriteBufferRect(command, buffer, blocking_write, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueFillBuffer(
            cl_command_queue command,
            cl_mem buffer,
            Array pattern,
            size_t pattern_size,
            size_t offset,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueFillBuffer(command, buffer, pattern, pattern_size, offset, size,
                wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueCopyBuffer(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            size_t src_offset,
            size_t dst_offset,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyBuffer(command, src_buffer, dst_buffer, src_offset, dst_offset, size,
               wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueCopyBufferRect(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            size_t src_row_pitch,
            size_t src_slice_pitch,
            size_t dst_row_pitch,
            size_t dst_slice_pitch,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyBufferRect(command, src_buffer, dst_buffer, src_origin, dst_origin, region,
                src_row_pitch, src_slice_pitch, dst_row_pitch, dst_slice_pitch,
                wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueReadImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_read,
            CLImageRegion region,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            uint row_pitch = image.Source.RowPitch;
            uint slice_pitch = 0;
            var data = image.Source.Data;

            return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size, 
                row_pitch, slice_pitch, data, wait_list_size, wait_list, out _event);
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
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadBufferRect(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_read,
            CLPoint3t buffer_origin,
            CLPoint3t host_origin,
            CLPoint3t region,
            size_t buffer_row_pitch,
            size_t buffer_slice_pitch,
            size_t host_row_pitch,
            size_t host_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueWriteBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            size_t offset,
            size_t size,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueWriteBufferRect(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            CLPoint3t buffer_origin,
            CLPoint3t host_origin,
            CLPoint3t region,
            size_t buffer_row_pitch,
            size_t buffer_slice_pitch,
            size_t host_row_pitch,
            size_t host_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillBuffer(
            cl_command_queue command,
            cl_mem buffer,
            Array pattern,
            size_t pattern_size,
            size_t offset,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueCopyBuffer(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            size_t src_offset,
            size_t dst_offset,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueCopyBufferRect(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            size_t src_row_pitch,
            size_t src_slice_pitch,
            size_t dst_row_pitch,
            size_t dst_slice_pitch,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t row_pitch,
            size_t slice_pitch,
            Array data,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);
    }
}
