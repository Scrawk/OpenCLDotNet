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
        /// Enqueue commands to read from a buffer object to host memory.
        /// </summary>
        /// <param name="command">command_queue is a valid host  
        /// command-queue in which the read command will be queued. 
        /// command_queue and buffer must be created with the same 
        /// OpenCL context.></param>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="blocking_read">If the read and write operations 
        /// are blocking or non-blocking</param>
        /// <param name="byte_offset">offset is the offset in bytes in the buffer
        /// object to read from or write to</param>
        /// <param name="byte_size">The size of the data in bytes.</param>
        /// <param name="data">The buffer to read into.</param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns>The error code</returns>
        public static CL_ERROR EnqueueReadBuffer(
            cl_command_queue command,
            cl_mem buffer,
            bool blocking_read,
            uint byte_offset,
            uint byte_size,
            Array data,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            byte[] bytes = new byte[byte_size];
    
            var error = CL_EnqueueReadBuffer(command, buffer, blocking_read, 
                byte_offset, byte_size, bytes, wait_list_size, wait_list, out e);

            Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);
            return error;
        }

        /// <summary>
        /// Enqueue commands to write to a buffer object from host memory.
        /// </summary>
        /// <param name="command">Refers to the command-queue in which the
        /// write command will be queued. command_queue and buffer must be
        /// created with the same OpenCL context.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="blocking_write">Indicates if the write operations 
        /// are blocking or nonblocking.</param>
        /// <param name="byte_offset">The offset in bytes in the buffer 
        /// object to write to.</param>
        /// <param name="byte_size">The size of the data in bytes.</param>
        /// <param name="data">The data to write from.</param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR EnqueueWriteBuffer(
            cl_command_queue command,
            cl_mem buffer,
            bool blocking_write,
            uint byte_offset,
            uint byte_size,
            Array data,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            byte[] bytes = new byte[byte_size];
            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

            var error = CL_EnqueueWriteBuffer(command, buffer, blocking_write, byte_offset,
                        byte_size, bytes, wait_list_size, wait_list, out e);

            return error;
        }


        /// <summary>
        /// Enqueues a command to read from a 2D or 3D image object to host memory.
        /// </summary>
        /// <param name="command">Refers to the command-queue in which the read command will be queued. 
        /// command_queue and image must be created with the same OpenCL context</param>
        /// <param name="image">The image to read from.</param>
        /// <param name="blocking_read">Indicates if the read operations are blocking or non-blocking.</param>
        /// <param name="region">Defines the region in the image to read from.</param>
        /// <param name="data"></param>
        /// <param name="byte_size"></param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns></returns>
        public static CL_ERROR EnqueueReadImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_read,
            CLRegion3t region,
            Array data,
            uint byte_size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            uint row_pitch = image.RowPitch;
            uint slice_pitch = 0;

            var region_origin = region.Origion;
            var region_size = region.Size;

            var bytes = new byte[byte_size];

            var error = CL_EnqueueReadImage(command, image.Id, blocking_read, region_origin, region_size,
                row_pitch, slice_pitch, bytes, wait_list_size, wait_list, out e);

            Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);

            return error;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="image"></param>
        /// <param name="blocking_write"></param>
        /// <param name="region"></param>
        /// <param name="data"></param>
        /// <param name="byte_size"></param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns></returns>
        public static CL_ERROR EnqueueWriteImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_write,
            CLRegion3t region,
            Array data, 
            uint byte_size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            uint input_row_pitch = image.RowPitch;
            uint input_slice_pitch = 0;

            var region_origin = region.Origion;
            var region_size = region.Size;

            byte[] bytes = new byte[byte_size];
            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

            return CL_EnqueueWriteImage(command, image.Id, blocking_write, region_origin, region_size,
                input_row_pitch, input_slice_pitch, bytes,
                wait_list_size, wait_list, out e);
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
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueReadBufferRect(command, buffer, blocking_read, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueWriteBufferRect(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            CLPoint3t buffer_origin,
            CLPoint3t host_origin,
            CLPoint3t region,
            size_t buffer_row_pitch,
            uint buffer_slice_pitch,
            uint host_row_pitch,
            uint host_slice_pitch,
            Array ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueWriteBufferRect(command, buffer, blocking_write, buffer_origin, host_origin, region,
                buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
                ptr, wait_list_size, wait_list, out e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buffer"></param>
        /// <param name="pattern">A pointer to the data pattern of size pattern_size in bytes.</param>
        /// <param name="pattern_size">pattern_size in bytes.</param>
        /// <param name="offset">The location in bytes of the region being 
        /// filled in buffer and must be a multiple of pattern_size.</param>
        /// <param name="size">The size in bytes of region being filled 
        /// in buffer and must be a multiple of pattern_size.</param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns>The error code.</returns>
        private static CL_ERROR EnqueueFillBuffer(
            cl_command_queue command,
            cl_mem buffer,
            Array pattern,
            uint pattern_size,
            uint offset,
            uint size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueFillBuffer(command, buffer, pattern, pattern_size, offset, size,
                wait_list_size, wait_list, out e);
        }

        /// <summary>
        /// Enqueues a command to copy a buffer object to another buffer object.
        /// </summary>
        /// <param name="command">The command-queue in which the copy command will 
        /// be queued. The OpenCL context associated with command_queue, src_buffer, 
        /// and dst_buffer must be the same.</param>
        /// <param name="src_buffer">The buffer to read from.</param>
        /// <param name="dst_buffer">The buffer to write to.</param>
        /// <param name="byte_offset">The offset in bytes where to begin copying data from src_buffer.</param>
        /// <param name="byte_size">Refers to the size in bytes to copy.</param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR EnqueueCopyBuffer(
            cl_command_queue command,
            cl_mem src_buffer,
            cl_mem dst_buffer,
            uint byte_offset,
            uint byte_size,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueCopyBuffer(command, src_buffer, dst_buffer, 
                byte_offset, 0, byte_size, wait_list_size, wait_list, out e);
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
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
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
            out cl_event e)
        {
            return CL_EnqueueCopyBufferRect(command, src_buffer, dst_buffer, src_origin, dst_origin, region,
                src_row_pitch, src_slice_pitch, dst_row_pitch, dst_slice_pitch,
                wait_list_size, wait_list, out e);
        }

        /// <summary>
        /// Enqueues a command to fill an image object with a specified color.
        /// </summary>
        /// <param name="command">command_queue refers to the host command-queue 
        /// in which the fill command will be queued. The OpenCL context associated
        /// with command_queue and image must be the same.</param>
        /// <param name="image">The image to fill.</param>
        /// <param name="fill_color">
        /// 
        /// fill_color is the color used to fill the image.
        /// 
        /// The fill color is a single floating point value if the channel order is CL_​DEPTH.
        /// 
        /// The fill color is a four component RGBA floating-point color value 
        /// if the image channel data type is not an unnormalized signed or unsigned integer
        /// 
        /// Is a four component signed integer value if the image channel data type 
        /// is an unnormalized signed integer type.
        /// 
        /// Is a four component unsigned integer value if the image channel data type is 
        /// an unnormalized unsigned integer type.
        /// 
        /// The fill color will be converted to the appropriate image channel format and 
        /// order associated with image.
        /// 
        /// </param>
        /// <param name="region">Defines the region in the image to fill.</param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns>The error code.</returns>
        public static CL_ERROR EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLRegion3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueFillImage(command, image, fill_color, region.Origion, region.Size,
                wait_list_size, wait_list, out e);
        }

        public static CL_ERROR EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            Array color,
            CL_DATA_TYPE type,
            CLRegion3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            switch (type)
            {
                case CL_DATA_TYPE.FLOAT:
                    return CL_EnqueueFillImage(command, image, color as float[], region.Origion, region.Size, 
                        wait_list_size, wait_list, out e);

                case CL_DATA_TYPE.INT:
                    return CL_EnqueueFillImage(command, image, color as int[], region.Origion, region.Size, 
                        wait_list_size, wait_list, out e);

                case CL_DATA_TYPE.UINT:
                    return CL_EnqueueFillImage(command, image, color as uint[], region.Origion, region.Size, 
                        wait_list_size, wait_list, out e);

                default:
                    e = new cl_event();   
                    return CL_ERROR.INVALID_DATA_TYPE;
                }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="src_image"></param>
        /// <param name="dst_image"></param>
        /// <param name="src_origin"></param>
        /// <param name="region"></param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns></returns>
        public static CL_ERROR EnqueueCopyImage(
            cl_command_queue command,
            cl_mem src_image,
            cl_mem dst_image,
            CLPoint3t src_origin,
            CLRegion3t region,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueCopyImage(command, src_image, dst_image, src_origin, region.Origion, region.Size,
                wait_list_size, wait_list, out e);
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
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
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
            out cl_event e)
        {
            return CL_EnqueueCopyImageToBuffer(command, src_image, dst_buffer, src_origin, region, dst_offset,
                wait_list_size, wait_list, out e);
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
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
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
            out cl_event e)
        {
            return CL_EnqueueCopyBufferToImage(command, src_buffer, dst_image, src_offset, dst_origin, region,
                wait_list_size, wait_list, out e);
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
            out cl_event e,
            out CL_ERROR error)
        {
            return CL_EnqueueMapBuffer(command, buffer, blocking_map, map_flags, offset, size,
                wait_list_size, wait_list, out e, out error);
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
            out cl_event e,
            out CL_ERROR error)
        {
            return CL_EnqueueMapImage(command, image, blocking_map, map_flags, origin,
                region, image_row_pitch, image_slice_pitch,
                wait_list_size, wait_list, out e, out error);
        }

        private static CL_ERROR EnqueueUnmapMemObject(
            cl_command_queue command,
            cl_mem memobj,
            Array mapped_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueUnmapMemObject(command, memobj, mapped_ptr,
                wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueMigrateMemObjects(
            cl_command_queue command,
            cl_uint num_mem_objects,
            cl_mem[] mem_objects,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueMigrateMemObjects(command, num_mem_objects, mem_objects, flags,
                wait_list_size, wait_list, out e);
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
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
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
            out cl_event e)
        {
            return CL_EnqueueNDRangeKernel(command, kernel, work_dim,
                global_work_offset, global_work_size, local_work_size,
                wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueMarkerWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueMarkerWithWaitList(command, wait_list_size, wait_list, out e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="wait_list_size">The event wait list size.</param>
        /// <param name="wait_list">The event wait list. 
        /// This is all the events that will need to completed before 
        /// this event is executed.</param>
        /// <param name="e">The event generated for this command.</param>
        /// <returns></returns>
        public static CL_ERROR EnqueueBarrierWithWaitList(
            cl_command_queue command,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueBarrierWithWaitList(command, wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueSVMMemcpy(
            cl_command_queue command,
            cl_bool blocking_copy,
            Array dst_ptr,
            Array src_ptr,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueSVMMemcpy(command, blocking_copy, dst_ptr, src_ptr, size,
                wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueSVMMemFill(
            cl_command_queue command,
            Array svm_ptr,
            Array pattern,
            size_t pattern_size,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueSVMMemFill(command, svm_ptr, pattern, pattern_size, size,
                wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueSVMMap(
            cl_command_queue command,
            cl_bool blocking_map,
            cl_map_flags flags,
            Array svm_ptr,
            size_t size,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueSVMMap(command, blocking_map, flags, svm_ptr, size,
                wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueSVMUnmap(
            cl_command_queue command,
            Array svm_ptr,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueSVMUnmap(command, svm_ptr, wait_list_size, wait_list, out e);
        }

        private static CL_ERROR EnqueueSVMMigrateMem(
            cl_command_queue command,
            cl_uint num_svm_pointers,
            Array[] svm_pointers,
            CLPoint3t sizes,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            out cl_event e)
        {
            return CL_EnqueueSVMMigrateMem(command, num_svm_pointers, svm_pointers, sizes, flags,
                wait_list_size, wait_list, out e);
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
            [Out] byte[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueWriteBuffer(
            cl_command_queue command,
            cl_mem buffer,
            cl_bool blocking_write,
            size_t offset,
            size_t size,
            byte[] ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            [Out] byte[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueWriteImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_write,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            byte[] data,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

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
            [Out] out cl_event e);

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
            [Out] out cl_event e);

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
            [Out] out cl_event e);

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
            [Out] out cl_event e);

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
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            CLColorRGBA fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            int[] fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            uint[] fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueFillImage(
            cl_command_queue command,
            cl_mem image,
            float[] fill_color,
            CLPoint3t origin,
            CLPoint3t region,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

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
            [Out] out cl_event e);

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
           [Out] out cl_event e);

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
            [Out] out cl_event e);

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
            [Out] out cl_event e,
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
            [Out] out cl_event e,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueUnmapMemObject(
            cl_command_queue command,
            cl_mem memobj,
            Array mapped_ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueMigrateMemObjects(
            cl_command_queue command,
            cl_uint num_mem_objects,
            cl_mem[] mem_objects,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

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
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueMarkerWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueBarrierWithWaitList(
            cl_command_queue command,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMemcpy(
            cl_command_queue command,
            cl_bool blocking_copy,
            Array dst_ptr,
            Array src_ptr,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMemFill(
            cl_command_queue command,
            Array svm_ptr,
            Array pattern,
            size_t pattern_size,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMap(
            cl_command_queue command,
            cl_bool blocking_map,
            cl_map_flags flags,
            Array svm_ptr,
            size_t size,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMUnmap(
            cl_command_queue command,
            Array svm_ptr,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueSVMMigrateMem(
            cl_command_queue command,
            cl_uint num_svm_pointers,
            Array[] svm_pointers,
            CLPoint3t sizes,
            cl_mem_migration_flags flags,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event e);
    }
}
