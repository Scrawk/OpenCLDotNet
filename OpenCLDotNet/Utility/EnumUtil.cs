﻿using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Utility
{
    public class EnumUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_DEVICE_INFO_RETURN_TYPE GetReturnType(CL_DEVICE_INFO info)
        {

            switch (info)
            {
                //OBJECT
                case CL_DEVICE_INFO.TYPE:
                case CL_DEVICE_INFO.SINGLE_FP_CONFIG:
                case CL_DEVICE_INFO.GLOBAL_MEM_CACHE_TYPE:
                case CL_DEVICE_INFO.LOCAL_MEM_TYPE:
                case CL_DEVICE_INFO.EXECUTION_CAPABILITIES:
                case CL_DEVICE_INFO.PLATFORM:
                case CL_DEVICE_INFO.ENQUEUE_CAPABILITIES:
                case CL_DEVICE_INFO.ATOMIC_FENCE_CAPABILITIES:
                case CL_DEVICE_INFO.ATOMIC_MEMORY_CAPABILITIES:
                case CL_DEVICE_INFO.SVM_CAPABILITIES:
                case CL_DEVICE_INFO.PARTITION_AFFINITY_DOMAIN:
                case CL_DEVICE_INFO.NUMERIC_VERSION:
                case CL_DEVICE_INFO.QUEUE_ON_QUEUE_PROPERTIES:
                case CL_DEVICE_INFO.DOUBLE_FP_CONFIG:
                case CL_DEVICE_INFO.PARENT_DEVICE:
                    return CL_DEVICE_INFO_RETURN_TYPE.OBJECT;

                //OBJECT ARRAY
                case CL_DEVICE_INFO.PARTITION_TYPE:
                case CL_DEVICE_INFO.PARTITION_PROPERTIES:
                case CL_DEVICE_INFO.EXTENSIONS_WITH_VERSION:
                case CL_DEVICE_INFO.OPENC_FEATURES:
                case CL_DEVICE_INFO.OPENC_ALL_VERSIONS:
                case CL_DEVICE_INFO.BUILT_IN_KERNELS_WITH_VERSION:
                case CL_DEVICE_INFO.ILS_WITH_VERSION:
                    return CL_DEVICE_INFO_RETURN_TYPE.OBJECT_ARRAY;

                //UINT
                case CL_DEVICE_INFO.VENDOR_ID:
                case CL_DEVICE_INFO.MAX_COMPUTE_UNITS:
                case CL_DEVICE_INFO.MAX_WORK_ITEM_DIMENSIONS:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_CHAR:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_SHORT:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_INT:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_LONG:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_FLOAT:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_DOUBLE:
                case CL_DEVICE_INFO.PREFERRED_VECTOR_WIDTH_HALF:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_CHAR:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_SHORT:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_INT:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_LONG:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_FLOAT:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_DOUBLE:
                case CL_DEVICE_INFO.NATIVE_VECTOR_WIDTH_HALF:
                case CL_DEVICE_INFO.MAX_CLOCK_FREQUENCY:
                case CL_DEVICE_INFO.ADDRESS_BITS:
                case CL_DEVICE_INFO.MAX_READ_IMAGE_ARGS:
                case CL_DEVICE_INFO.MAX_WRITE_IMAGE_ARGS:
                case CL_DEVICE_INFO.MAX_SAMPLERS:
                case CL_DEVICE_INFO.MEM_BASE_ADDR_ALIGN:
                case CL_DEVICE_INFO.MIN_DATA_TYPE_ALIGN_SIZE:
                case CL_DEVICE_INFO.GLOBAL_MEM_CACHELINE_SIZE:
                case CL_DEVICE_INFO.MAX_CONSTANT_ARGS:
                case CL_DEVICE_INFO.MAX_READ_WRITE_IMAGE_ARGS:
                case CL_DEVICE_INFO.IMAGE_PITCH_ALIGNMENT:
                case CL_DEVICE_INFO.IMAGE_BASE_ADDRESS_ALIGNMENT:
                case CL_DEVICE_INFO.MAX_PIPE_ARGS:
                case CL_DEVICE_INFO.PIPE_MAX_ACTIVE_RESERVATIONS:
                case CL_DEVICE_INFO.QUEUE_ON_PREFERRED_SIZE:
                case CL_DEVICE_INFO.QUEUE_ON_MAX_SIZE:
                case CL_DEVICE_INFO.PARTITION_MAX_SUB_DEVICES:
                case CL_DEVICE_INFO.REFERENCE_COUNT:
                case CL_DEVICE_INFO.PREFERRED_PLATFORM_ATOMIC_ALIGNMENT:
                case CL_DEVICE_INFO.PREFERRED_GLOBAL_ATOMIC_ALIGNMENT:
                case CL_DEVICE_INFO.PREFERRED_LOCAL_ATOMIC_ALIGNMENT:
                case CL_DEVICE_INFO.MAX_NUM_SUB_GROUPS:
                case CL_DEVICE_INFO.PIPE_MAX_PACKET_SIZE:
                case CL_DEVICE_INFO.MAX_ON_QUEUES:
                case CL_DEVICE_INFO.MAX_ON_EVENTS:
                    return CL_DEVICE_INFO_RETURN_TYPE.UINT;

                //SIZE_ARRAY
                case CL_DEVICE_INFO.MAX_WORK_ITEM_SIZES:
                    return CL_DEVICE_INFO_RETURN_TYPE.SIZET_ARRAY;

                //SIZE
                case CL_DEVICE_INFO.MAX_WORK_GROUP_SIZE:
                case CL_DEVICE_INFO.IMAGE2D_MAX_WIDTH:
                case CL_DEVICE_INFO.IMAGE2D_MAX_HEIGHT:
                case CL_DEVICE_INFO.IMAGE3D_MAX_WIDTH:
                case CL_DEVICE_INFO.IMAGE3D_MAX_HEIGHT:
                case CL_DEVICE_INFO.IMAGE3D_MAX_DEPTH:
                case CL_DEVICE_INFO.MAX_PARAMETER_SIZE:
                case CL_DEVICE_INFO.PROFILING_TIMER_RESOLUTION:
                case CL_DEVICE_INFO.IMAGE_MAX_BUFFER_SIZE:
                case CL_DEVICE_INFO.MAX_GLOBAL_VARIABLE_SIZE:
                case CL_DEVICE_INFO.GLOBAL_VARIABLE_PREFERRED_TOTAL_SIZE:
                case CL_DEVICE_INFO.PREFERRED_WORK_GROUP_SIZE_MULTIPLE:
                case CL_DEVICE_INFO.IMAGE_MAX_ARRAY_SIZE:
                case CL_DEVICE_INFO.PRINTF_BUFFER_SIZE:
                    return CL_DEVICE_INFO_RETURN_TYPE.SIZET;

                //ULONG
                case CL_DEVICE_INFO.MAX_MEM_ALLOC_SIZE:
                case CL_DEVICE_INFO.GLOBAL_MEM_CACHE_SIZE:
                case CL_DEVICE_INFO.GLOBAL_MEM_SIZE:
                case CL_DEVICE_INFO.MAX_CONSTANT_BUFFER_SIZE:
                case CL_DEVICE_INFO.LOCAL_MEM_SIZE:
                    return CL_DEVICE_INFO_RETURN_TYPE.ULONG;

                //BOOL
                case CL_DEVICE_INFO.IMAGE_SUPPORT:
                case CL_DEVICE_INFO.ERROR_CORRECTION_SUPPORT:
                case CL_DEVICE_INFO.ENDIAN_LITTLE:
                case CL_DEVICE_INFO.DEVICE_AVAILABLE:
                case CL_DEVICE_INFO.COMPILER_AVAILABLE:
                case CL_DEVICE_INFO.LINKER_AVAILABLE:
                case CL_DEVICE_INFO.PREFERRED_INTEROP_USER_SYNC:
                case CL_DEVICE_INFO.SUB_GROUP_INDEPENDENT_FORWARD_PROGRESS:
                case CL_DEVICE_INFO.NON_UNIFORM_WORK_GROUP_SUPPORT:
                case CL_DEVICE_INFO.WORK_GROUP_COLLECTIVE_FUNCTIONS_SUPPORT:
                case CL_DEVICE_INFO.GENERIC_ADDRESS_SPACE_SUPPORT:
                case CL_DEVICE_INFO.PIPE_SUPPORT:
                    return CL_DEVICE_INFO_RETURN_TYPE.BOOL;

                //CHAR_ARRAY
                case CL_DEVICE_INFO.NAME:
                case CL_DEVICE_INFO.VENDOR:
                case CL_DEVICE_INFO.DRIVER_VERSION:
                case CL_DEVICE_INFO.PROFILE:
                case CL_DEVICE_INFO.VERSION:
                case CL_DEVICE_INFO.EXTENSIONS:
                case CL_DEVICE_INFO.IL_VERSION:
                case CL_DEVICE_INFO.BUILT_IN_KERNELS:
                case CL_DEVICE_INFO.OPENC_VERSION:
                case CL_DEVICE_INFO.LATEST_CONFORMANCE_VERSION_PASSED:
                    return CL_DEVICE_INFO_RETURN_TYPE.CHAR_ARRAY;
            }


            return CL_DEVICE_INFO_RETURN_TYPE.UNKNOWN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_CONTEXT_INFO_RETURN_TYPE GetReturnType(CL_CONTEXT_INFO info)
        {
            switch (info)
            {
                case CL_CONTEXT_INFO.DEVICES:
                case CL_CONTEXT_INFO.PROPERTIES:
                    return CL_CONTEXT_INFO_RETURN_TYPE.OBJECT_ARRAY;

                case CL_CONTEXT_INFO.REFERENCE_COUNT:
                case CL_CONTEXT_INFO.NUM_DEVICES:
                    return CL_CONTEXT_INFO_RETURN_TYPE.UINT;
            }

            return CL_CONTEXT_INFO_RETURN_TYPE.UNKNOWN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_PROGRAM_BUILD_INFO_RETURN_TYPE GetReturnType(CL_PROGRAM_BUILD_INFO info)
        {
            switch (info)
            {
                case CL_PROGRAM_BUILD_INFO.STATUS:
                    return CL_PROGRAM_BUILD_INFO_RETURN_TYPE.UINT;

                case CL_PROGRAM_BUILD_INFO.OPTIONS:
                case CL_PROGRAM_BUILD_INFO.LOG:
                    return CL_PROGRAM_BUILD_INFO_RETURN_TYPE.CHAR_ARRAY;
            }

            return CL_PROGRAM_BUILD_INFO_RETURN_TYPE.UNKNOWN;
        }

    }
}
