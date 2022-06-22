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
        /// <param name="command"></param>
        /// <param name="image"></param>
        /// <param name="blocking_read"></param>
        /// <param name="region"></param>
        /// <param name="data"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueReadImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_read,
            CLImageRegion region,
            Array data,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            uint row_pitch = image.RowPitch;
            uint slice_pitch = 0;
            var type = image.DataType;

            switch (type)
            {
                case CL_MEM_DATA_TYPE.FLOAT:
                    var f_data = data as float[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, f_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.INT:
                    var i_data = data as int[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, i_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.UINT:
                    var ui_data = data as uint[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, ui_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SHORT:
                    var s_data = data as short[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, s_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.USHORT:
                    var us_data = data as ushort[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, us_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.BYTE:
                    var b_data = data as byte[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, b_data, wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SBYTE:
                    var sb_data = data as sbyte[];
                    return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size,
                        row_pitch, slice_pitch, sb_data, wait_list_size, wait_list, out _event);

                default:
                    _event = new cl_event();
                    return CL_ERROR.INVALID_DATA_TYPE;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="image"></param>
        /// <param name="blocking_write"></param>
        /// <param name="region"></param>
        /// <param name="data"></param>
        /// <param name="wait_list_size"></param>
        /// <param name="wait_list"></param>
        /// <param name="_event"></param>
        /// <returns></returns>
        public static CL_ERROR EnqueueWriteImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_write,
            CLImageRegion region,
            Array data,
            uint wait_list_size,
            cl_event[] wait_list,
            out cl_event _event)
        {
            uint input_row_pitch = image.RowPitch;
            uint input_slice_pitch = 0;
            var type = image.DataType;

            switch (type)
            {
                case CL_MEM_DATA_TYPE.FLOAT:
                    var f_data = data as float[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, f_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.INT:
                    var i_data = data as int[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, i_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.UINT:
                    var ui_data = data as uint[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, ui_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SHORT:
                    var s_data = data as short[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, s_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.USHORT:
                    var us_data = data as ushort[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, us_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.BYTE:
                    var b_data = data as byte[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, b_data,
                        wait_list_size, wait_list, out _event);

                case CL_MEM_DATA_TYPE.SBYTE:
                    var sb_data = data as sbyte[];
                    return CL_EnqueueWriteImage(command, image.Id, blocking_write, region.Origion, region.Size,
                        input_row_pitch, input_slice_pitch, sb_data,
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
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            float[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            int[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            uint[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            short[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            ushort[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            sbyte[] data,
            uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3t origin,
            CLPoint3t region,
            size_t input_row_pitch,
            size_t input_slice_pitch,
            byte[] data,
            uint wait_list_size,
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
            float[] data,
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
            int[] data,
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
            uint[] data,
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
            short[] data,
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
            ushort[] data,
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
            sbyte[] data,
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
            byte[] data,
            cl_uint wait_list_size,
            [Out] cl_event[] wait_list,
            [Out] out cl_event _event);

    }
}
