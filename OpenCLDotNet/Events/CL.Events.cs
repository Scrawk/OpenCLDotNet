using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Utility;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {
        public static CL_ERROR WaitForEvents(
            uint num_events,
            cl_event[] event_list)
        {
            return CL_WaitForEvents(num_events, event_list);
        }

        public static cl_event CreateUserEvent(
            cl_context context,
            out CL_ERROR error)
        {
            return CL_CreateUserEvent(context, out error);
        }

        public static CL_ERROR GetEventInfoSize(
            cl_event _event,
            CL_EVENT_INFO name,
            out uint size)
        {
            size_t sizet;
            var error = CL_GetEventInfoSize(_event, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        public static CL_ERROR GetEventInfo(
            cl_event _event,
            CL_EVENT_INFO name,
            uint size,
            out UInt64 value)
        {
            return CL_GetEventInfo(_event, name, size, out value);
        }

        public static CL_ERROR GetEventProfilingInfoSize(
            cl_event _event,
            CL_PROFILING_INFO name,
            out uint size)
        {
            size_t sizet;
            var error = CL_GetEventProfilingInfoSize(_event, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        public static CL_ERROR GetEventProfilingInfo(
            cl_event _event,
            CL_PROFILING_INFO name,
            uint size,
            out UInt64 value)
        {
            return CL_GetEventProfilingInfo(_event, name, size, out value);
        }

        public static CL_ERROR RetainEvent(cl_event _event)
        {
            return CL_RetainEvent(_event);
        }

        public static CL_ERROR ReleaseEvent(cl_event _event)
        {
            return CL_ReleaseEvent(_event);
        }

        public static CL_ERROR SetUserEventStatus(cl_event _event, int status)
        {
            return CL_SetUserEventStatus(_event, status);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_WaitForEvents(
            cl_uint num_events,
            [Out] cl_event[] event_list);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_event CL_CreateUserEvent(
            cl_context context,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetEventInfoSize(
            cl_event _event,
            CL_EVENT_INFO name,
            [Out] out size_t size);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetEventInfo(
            cl_event _event,
            CL_EVENT_INFO name,
            size_t size,
            [Out] out UInt64 value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetEventProfilingInfoSize(
            cl_event _event,
            CL_PROFILING_INFO name,
            [Out] out size_t size);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetEventProfilingInfo(
            cl_event _event,
            CL_PROFILING_INFO name,
            size_t size,
            [Out] out UInt64 value);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainEvent(cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseEvent(cl_event _event);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_SetUserEventStatus(
            cl_event _event,
            cl_int status);

    }
}
