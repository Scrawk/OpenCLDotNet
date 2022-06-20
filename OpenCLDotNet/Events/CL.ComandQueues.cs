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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="device"></param>
        /// <param name="properties"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_command_queue CreateCommandQueueWithProperties(
            cl_context context,
            cl_device_id device,
            CLCommandQueueProperties properties,
            [Out] out CL_ERROR error)
        {
            return CL_CreateCommandQueueWithProperties(context, device, properties, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static CL_ERROR GetCommandQueueInfoSize(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            [Out] out uint size)
        {
            size_t sizet;
            var error = CL_GetCommandQueueInfoSize(command, name, out sizet);

            size = (uint)sizet;
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            uint size,
            [Out] out UInt64 info)
        {
            return CL_GetCommandQueueInfo(command, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            uint size,
            [Out] out cl_object info)
        {
            return CL_GetCommandQueueInfo(command, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            uint size,
            cl_object[] info)
        {
            return CL_GetCommandQueueInfo(command, name, size, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CL_ERROR RetainCommandQueue(cl_command_queue command)
        {
            return CL_RetainCommandQueue(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CL_ERROR ReleaseCommandQueue(cl_command_queue command)
        {
            return CL_ReleaseCommandQueue(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CL_ERROR Flush(cl_command_queue command)
        {
            return CL_Flush(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CL_ERROR Finish(cl_command_queue command)
        {
            return CL_Finish(command);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_command_queue CL_CreateCommandQueueWithProperties(
            cl_context context,
            cl_device_id device,
            CLCommandQueueProperties properties,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetCommandQueueInfoSize(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            [Out] out size_t size);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            size_t size,
            [Out] out UInt64 info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            size_t size,
            [Out] out cl_object info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetCommandQueueInfo(
            cl_command_queue command,
            CL_COMMAND_QUEUE_INFO name,
            size_t size,
            [Out] cl_object[] info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_RetainCommandQueue(cl_command_queue command);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_ReleaseCommandQueue(cl_command_queue command);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_Flush(cl_command_queue command);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_Finish(cl_command_queue command);
    }
}
