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
    /*
            switch (type)
            {
                case CL_MEM_DATA_TYPE.FLOAT:
                    var f_data = data as float[];

                case CL_MEM_DATA_TYPE.INT:
                    var i_data = data as int[];

                case CL_MEM_DATA_TYPE.UINT:
                    var ui_data = data as uint[];

                case CL_MEM_DATA_TYPE.SHORT:
                    var s_data = data as short[];

                case CL_MEM_DATA_TYPE.USHORT:
                    var us_data = data as ushort[];

                case CL_MEM_DATA_TYPE.BYTE:
                    var b_data = data as byte[];

                case CL_MEM_DATA_TYPE.SBYTE:
                    var sb_data = data as sbyte[];

                default:
                    _event = new cl_event();
                    return CL_ERROR.INVALID_DATA_TYPE;
            }
    */

    public static partial class CL
    {

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
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueReadBufferRect(command, buffer, blocking_read, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out _event);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_buffer"></param>
        /// <param name="dst_buffer"></param>
        /// <param name="src_offset"></param>
        /// <param name="dst_offset"></param>
        /// <param name="size"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyBuffer(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            uint src_offset,
            uint dst_offset,
            uint size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyBuffer(command, src_buffer, dst_buffer, src_offset, dst_offset, size,
               wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_buffer"></param>
        /// <param name="dst_buffer"></param>
        /// <param name="src_origin"></param>
        /// <param name="dst_origin"></param>
        /// <param name="region"></param>
        /// <param name="src_row_pitch"></param>
        /// <param name="src_slice_pitch"></param>
        /// <param name="dst_row_pitch"></param>
        /// <param name="dst_slice_pitch"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyBufferRect(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            uint src_row_pitch,
            uint src_slice_pitch,
            uint dst_row_pitch,
            uint dst_slice_pitch,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyBufferRect(command, src_buffer, dst_buffer, src_origin, dst_origin, region,
                src_row_pitch, src_slice_pitch, dst_row_pitch, dst_slice_pitch,
                wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="image"></param>
        /// <param name="fill_color"></param>
        /// <param name="origin"></param>
        /// <param name="region"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueFillImage(command, image, fill_color, origin, region,
                wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_image"></param>
        /// <param name="dst_image"></param>
        /// <param name="src_origin"></param>
        /// <param name="dst_origin"></param>
        /// <param name="region"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyImage(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_image,
            CLPoint3t src_origin,
            CLPoint3t dst_origin,
            CLPoint3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyImage(command, src_image, dst_image, src_origin, dst_origin, region,
                wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_image"></param>
        /// <param name="dst_buffer"></param>
        /// <param name="src_origin"></param>
        /// <param name="region"></param>
        /// <param name="dst_offset"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyImageToBuffer(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_buffer,
            CLPoint3t src_origin,
            CLPoint3t region,
            uint dst_offset,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyImageToBuffer(command, src_image, dst_buffer, src_origin, region, dst_offset,
                wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_buffer"></param>
        /// <param name="dst_image"></param>
        /// <param name="src_offset"></param>
        /// <param name="dst_origin"></param>
        /// <param name="region"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyBufferToImage(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_image,
            uint src_offset,
            CLPoint3t dst_origin,
            CLPoint3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueCopyBufferToImage(command, src_buffer, dst_image, src_offset, dst_origin, region,
                wait_list_size, wait_list, out _event);
        }

        private static Array EnqueueMapBuffer(
            cl_command_queue command,
            cl_mem buffer,
            bool blocking_map,
            cl_map_flags map_flags,
            uint offset,
            uint size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event,
            out CL_ERROR error)
        {
            return CL_EnqueueMapBuffer(command, buffer, blocking_map, map_flags, offset, size,
                wait_list_size, wait_list, out _event, out error);
        }

        private static Array EnqueueMapImage(
            cl_command_queue command,
            cl_mem image,
            bool blocking_map,
            cl_map_flags map_flags,
            CLPoint3t origin,
            CLPoint3t region,
            CLPoint3t image_row_pitch,
            CLPoint3t image_slice_pitch,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event,
            out CL_ERROR error)
        {
            return CL_EnqueueMapImage(command, image, blocking_map, map_flags, origin,
                region, image_row_pitch, image_slice_pitch,
                wait_list_size, wait_list, out _event, out error);
        }

        private static CL_ERROR EnqueueUnmapMemObject(
            cl_command_queue command,
            cl_mem memobj,
            Array mapped_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
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
            out cl_event _event)
        {
            return CL_EnqueueMigrateMemObjects(command, num_mem_objects, mem_objects, flags,
                wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="kernel"></param>
        /// <param name="work_dim"></param>
        /// <param name="global_work_offset"></param>
        /// <param name="global_work_size"></param>
        /// <param name="local_work_size"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueNDRangeKernel(
            cl_command_queue command,
            cl_kernel kernel,
            uint work_dim,
            size_t[] global_work_offset,
            size_t[] global_work_size,
            size_t[] local_work_size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueNDRangeKernel(command, kernel, work_dim,
                global_work_offset, global_work_size, local_work_size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueMarkerWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            return CL_EnqueueMarkerWithWaitList(command, wait_list_size, wait_list, out _event);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueBarrierWithWaitList(
            cl_command_queue command,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
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
            out cl_event _event)
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
            out cl_event _event)
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
            out cl_event _event)
        {
            return CL_EnqueueSVMMap(command, blocking_map, flags, svm_ptr, size,
                wait_list_size, wait_list, out _event);
        }

        private static CL_ERROR EnqueueSVMUnmap(
            cl_command_queue command,
            Array svm_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
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
            out cl_event _event)
        {
            return CL_EnqueueSVMMigrateMem(command, num_svm_pointers, svm_pointers, sizes, flags,
                wait_list_size, wait_list, out _event);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

       
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
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

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
            [Out] out cl_event _event);

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
           [Out] out cl_event _event);

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
            [Out] out cl_event _event);

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
            [Out] out cl_event _event,
            [Out] out CL_ERROR error);

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
            [Out] out cl_event _event,
            [Out] out CL_ERROR error);

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
