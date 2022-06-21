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
            float[] ptr,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueReadBuffer(command, buffer, blocking_read, offset, size, ptr,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueReadBufferRect(
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
            int[] ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueReadBufferRect(command, buffer, blocking_read, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueWriteBuffer(
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

        private static CL_ERROR EnqueueWriteBufferRect(
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

        private static CL_ERROR EnqueueFillBuffer(
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

        private static CL_ERROR EnqueueCopyBuffer(
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

        private static CL_ERROR EnqueueCopyBufferRect(
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
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            uint row_pitch = image.RowPitch;
            uint slice_pitch = 0;
            //var data = image.Source;
            Array data = null;

            return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                row_pitch, slice_pitch, data, wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueWriteImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_write,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event)
        {
            return CL_EnqueueWriteImage(command, image, blocking_write, origin, region,
                input_row_pitch, input_slice_pitch, ptr,
                wait_list_size, wait_list, _event);
        }

        private static CL_ERROR EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event)
        {
            return CL_EnqueueFillImage(command, image, fill_color, origin, region,
                wait_list_size, wait_list, _event);
        }

        private static CL_ERROR EnqueueCopyImage(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_image,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event)
        {
            return CL_EnqueueCopyImage(command, src_image, dst_image, src_origin, dst_origin, region,
                wait_list_size, wait_list, _event);
        }

        private static CL_ERROR EnqueueCopyImageToBuffer(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t region,
            size_t dst_offset,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event)
        {
            return CL_EnqueueCopyImageToBuffer(command, src_image, dst_buffer, src_origin, region, dst_offset,
                wait_list_size, wait_list, _event);
        }

        private static CL_ERROR EnqueueCopyBufferToImage(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_image,
            size_t src_offset,
            CLPoint3t dst_origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event)
        {
            return CL_EnqueueCopyBufferToImage(command, src_buffer, dst_image, src_offset, dst_origin, region,
                wait_list_size, wait_list, _event);
        }

        private static Array EnqueueMapBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_map,
            cl_map_flags map_flags,
            size_t offset,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event,
            out cl_int error)
        {
            return CL_EnqueueMapBuffer(command, buffer, blocking_map, map_flags, offset, size,
                wait_list_size, wait_list, _event, out error);
        }

        private static Array EnqueueMapImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_map,
            cl_map_flags map_flags,
            CLPoint3t origin,
            CLPoint3t region,
            CLPoint3t image_row_pitch,
            CLPoint3t image_slice_pitch,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            cl_event[] _event,
            out cl_int error)
        {
            return CL_EnqueueMapImage(command, image, blocking_map, map_flags, origin,
                region, image_row_pitch, image_slice_pitch,
                wait_list_size, wait_list, _event, out error);
        }

        private static CL_ERROR EnqueueUnmapMemObject(
            cl_command_queue command,
            cl_mem memobj,
            Array mapped_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueUnmapMemObject(command, memobj, mapped_ptr,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueMigrateMemObjects(
            cl_command_queue command,
            cl_uint num_mem_objects,
            cl_mem[] mem_objects,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueMigrateMemObjects(command, num_mem_objects, mem_objects, flags,
                wait_list_size, wait_list, out _event);
        }

        public static CL_ERROR EnqueueNDRangeKernel(
            cl_command_queue command,
            cl_kernel kernel,
            uint work_dim,
            size_t[] global_work_offset,
            size_t[] global_work_size,
            size_t[] local_work_size,
            uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueNDRangeKernel(command, kernel, work_dim,
                global_work_offset, global_work_size, local_work_size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueMarkerWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueMarkerWithWaitList(command, wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueBarrierWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueBarrierWithWaitList(command, wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMMemcpy(
            cl_command_queue command,
            cl_bool blocking_copy,
            Array dst_ptr,
            Array src_ptr,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueSVMMemcpy(command, blocking_copy, dst_ptr, src_ptr, size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMMemFill(
            cl_command_queue command,
            Array svm_ptr,
            Array pattern,
            size_t pattern_size,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueSVMMemFill(command, svm_ptr, pattern, pattern_size, size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMMap(
            cl_command_queue command,
            cl_bool blocking_map,
            cl_map_flags flags,
            Array svm_ptr,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueSVMMap(command, blocking_map, flags, svm_ptr, size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMUnmap(
            cl_command_queue command,
            Array svm_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueSVMUnmap(command, svm_ptr, wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMMigrateMem(
            cl_command_queue command,
            cl_uint num_svm_pointers,
            Array[] svm_pointers,
            CLPoint3t sizes,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event)
        {
            return CL_EnqueueSVMMigrateMem(command, num_svm_pointers, svm_pointers, sizes, flags,
                wait_list_size, wait_list, out _event);
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
            int[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
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
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueWriteImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_write,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueCopyImage(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_image,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueCopyImageToBuffer(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t region,
            size_t dst_offset,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueCopyBufferToImage(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_image,
            size_t src_offset,
            CLPoint3t dst_origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern Array CL_EnqueueMapBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_map,
            cl_map_flags map_flags,
            size_t offset,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event,
            out cl_int error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern Array CL_EnqueueMapImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_map,
            cl_map_flags map_flags,
            CLPoint3t origin,
            CLPoint3t region,
            CLPoint3t image_row_pitch,
            CLPoint3t image_slice_pitch,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            cl_event[] _event,
            out cl_int error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueUnmapMemObject(
            cl_command_queue command,
            cl_mem memobj,
            Array mapped_ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueMigrateMemObjects(
            cl_command_queue command,
            cl_uint num_mem_objects,
            cl_mem[] mem_objects,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueNDRangeKernel(
            cl_command_queue command,
            cl_kernel kernel,
            cl_uint work_dim,
            size_t[] global_work_offset,
            size_t[] global_work_size,
            size_t[] local_work_size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueMarkerWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueBarrierWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMemcpy(
            cl_command_queue command,
            cl_bool blocking_copy,
            Array dst_ptr,
            Array src_ptr,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMemFill(
            cl_command_queue command,
            Array svm_ptr,
            Array pattern,
            size_t pattern_size,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMap(
            cl_command_queue command,
            cl_bool blocking_map,
            cl_map_flags flags,
            Array svm_ptr,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMUnmap(
            cl_command_queue command,
            Array svm_ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMigrateMem(
            cl_command_queue command,
            cl_uint num_svm_pointers,
            Array[] svm_pointers,
            CLPoint3t sizes,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);
    }
}
