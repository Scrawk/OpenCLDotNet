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
        public static CL_ERROR EnqueueReadImage(
            cl_command_queue command,
            CLImage image,
            bool blocking_read,
            CLImageRegion region)
        {
            uint row_pitch = image.Source.RowPitch;
            uint slice_pitch = 0;
            var data = image.Source.Data;

            cl_event _event;
            return CL_EnqueueReadImage(command, image.Id, blocking_read, region.Origion, region.Size, 
                row_pitch, slice_pitch, data, 0, null, out _event);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_EnqueueReadImage(
            cl_command_queue command,
            cl_mem image,
            cl_bool blocking_read,
            CLPoint3ui origin,
            CLPoint3ui region,
            size_t row_pitch,
            size_t slice_pitch,
            Array data,
            cl_uint wait_list_size,
            cl_event[] wait_list,
            [Out] out cl_event _event);
    }
}
