using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Core
{
    public partial class CL
    {

        public static uint GetNumChannels(CL_CHANNEL_ORDER order)
        {
            switch (order)
            {
                case CL_CHANNEL_ORDER.R:
                case CL_CHANNEL_ORDER.A:
                case CL_CHANNEL_ORDER.INTENSITY:
                case CL_CHANNEL_ORDER.LUMINANCE:
                case CL_CHANNEL_ORDER.Rx:
                case CL_CHANNEL_ORDER.DEPTH:
                    return 1;

                case CL_CHANNEL_ORDER.RG:
                case CL_CHANNEL_ORDER.RA:
                case CL_CHANNEL_ORDER.RGx:
                case CL_CHANNEL_ORDER.DEPTH_STENCIL:
                    return 2;

                case CL_CHANNEL_ORDER.RGB:
                case CL_CHANNEL_ORDER.RGBx:
                case CL_CHANNEL_ORDER.sRGB:
                case CL_CHANNEL_ORDER.sRGBx:
                    return 3;

                case CL_CHANNEL_ORDER.RGBA:
                case CL_CHANNEL_ORDER.BGRA:
                case CL_CHANNEL_ORDER.ARGB:
                case CL_CHANNEL_ORDER.sRGBA:
                case CL_CHANNEL_ORDER.sBGRA:
                case CL_CHANNEL_ORDER.ABGR:
                    return 4;
            }

            return 0;
        }

        public static bool IsValidChannelType(CL_CHANNEL_ORDER order, CL_CHANNEL_TYPE type)
        {
            switch (order)
            {
                case CL_CHANNEL_ORDER.INTENSITY:
                case CL_CHANNEL_ORDER.LUMINANCE:
                    {
                        if (type == CL_CHANNEL_TYPE.UNORM_INT8 ||
                           type == CL_CHANNEL_TYPE.UNORM_INT16 ||
                           type == CL_CHANNEL_TYPE.SNORM_INT8 ||
                           type == CL_CHANNEL_TYPE.SNORM_INT16 ||
                           type == CL_CHANNEL_TYPE.HALF_FLOAT ||
                           type == CL_CHANNEL_TYPE.FLOAT)
                            return true;
                        else
                            return false;
                    }

                case CL_CHANNEL_ORDER.RGB:
                case CL_CHANNEL_ORDER.RGBx:
                    {
                        if (type == CL_CHANNEL_TYPE.UNORM_SHORT_555 ||
                           type == CL_CHANNEL_TYPE.UNORM_SHORT_565 ||
                           type == CL_CHANNEL_TYPE.UNORM_INT_101010)
                            return true;
                        else
                            return false;
                    }

                case CL_CHANNEL_ORDER.RGBA:
                case CL_CHANNEL_ORDER.ARGB:
                case CL_CHANNEL_ORDER.BGRA:
                    {
                        if (type == CL_CHANNEL_TYPE.SIGNED_INT8 ||
                           type == CL_CHANNEL_TYPE.UNSIGNED_INT8 ||
                           type == CL_CHANNEL_TYPE.UNORM_INT8 ||
                           type == CL_CHANNEL_TYPE.SNORM_INT8)
                            return true;
                        else
                            return false;
                    }
            }

            return true;
        }

        public static uint SizeOf(CL_CHANNEL_TYPE type)
        {
            switch (type)
            {
                case CL_CHANNEL_TYPE.SNORM_INT8:
                case CL_CHANNEL_TYPE.SIGNED_INT8:
                    return sizeof(sbyte);

                case CL_CHANNEL_TYPE.UNORM_INT8:
                case CL_CHANNEL_TYPE.UNSIGNED_INT8:
                    return sizeof(byte);

                case CL_CHANNEL_TYPE.SNORM_INT16:
                case CL_CHANNEL_TYPE.UNORM_INT16:
                case CL_CHANNEL_TYPE.UNORM_SHORT_565:
                case CL_CHANNEL_TYPE.UNORM_SHORT_555:
                case CL_CHANNEL_TYPE.SIGNED_INT16:
                case CL_CHANNEL_TYPE.HALF_FLOAT:
                    return sizeof(short);

                case CL_CHANNEL_TYPE.UNSIGNED_INT16:
                    return sizeof(ushort);

                case CL_CHANNEL_TYPE.UNORM_INT_101010:
                case CL_CHANNEL_TYPE.SIGNED_INT32:
                case CL_CHANNEL_TYPE.UNORM_INT24:
                case CL_CHANNEL_TYPE.UNORM_INT_101010_2:
                    return sizeof(int);

                case CL_CHANNEL_TYPE.UNSIGNED_INT32:
                    return sizeof(uint);

                case CL_CHANNEL_TYPE.FLOAT:
                    return sizeof(float);
            }

            return 0;
        }

        public static bool IsValidArrayData(CL_CHANNEL_TYPE type, Array array)
        {
            switch (type)
            {
                case CL_CHANNEL_TYPE.SNORM_INT8:
                case CL_CHANNEL_TYPE.SIGNED_INT8:
                    return array is sbyte[];

                case CL_CHANNEL_TYPE.UNORM_INT8:
                case CL_CHANNEL_TYPE.UNSIGNED_INT8:
                    return array is byte[];

                case CL_CHANNEL_TYPE.SNORM_INT16:
                case CL_CHANNEL_TYPE.UNORM_INT16:
                case CL_CHANNEL_TYPE.UNORM_SHORT_565:
                case CL_CHANNEL_TYPE.UNORM_SHORT_555:
                case CL_CHANNEL_TYPE.SIGNED_INT16:
                case CL_CHANNEL_TYPE.HALF_FLOAT:
                    return array is short[];

                case CL_CHANNEL_TYPE.UNSIGNED_INT16:
                    return array is ushort[];

                case CL_CHANNEL_TYPE.UNORM_INT_101010:
                case CL_CHANNEL_TYPE.SIGNED_INT32:
                case CL_CHANNEL_TYPE.UNORM_INT24:
                case CL_CHANNEL_TYPE.UNORM_INT_101010_2:
                    return array is int[];

                case CL_CHANNEL_TYPE.UNSIGNED_INT32:
                    return array is uint[];

                case CL_CHANNEL_TYPE.FLOAT:
                    return array is float[];
            }

            return false;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_PLATFORM_INFO info)
        {

            switch (info)
            {
                case CL_PLATFORM_INFO.PROFILE:
                case CL_PLATFORM_INFO.VERSION:
                case CL_PLATFORM_INFO.NAME:
                case CL_PLATFORM_INFO.VENDOR:
                case CL_PLATFORM_INFO.EXTENSIONS:
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;

                //case CL_PLATFORM_INFO.HOST_TIMER_RESOLUTION:
                //case CL_PLATFORM_INFO.NUMERIC_VERSION:
                //case CL_PLATFORM_INFO.EXTENSIONS_WITH_VERSION:
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_DEVICE_INFO info)
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
                    return CL_INFO_RETURN_TYPE.OBJECT;

                //OBJECT ARRAY
                case CL_DEVICE_INFO.PARTITION_TYPE:
                case CL_DEVICE_INFO.PARTITION_PROPERTIES:
                case CL_DEVICE_INFO.EXTENSIONS_WITH_VERSION:
                case CL_DEVICE_INFO.OPENC_FEATURES:
                case CL_DEVICE_INFO.OPENC_ALL_VERSIONS:
                case CL_DEVICE_INFO.BUILT_IN_KERNELS_WITH_VERSION:
                case CL_DEVICE_INFO.ILS_WITH_VERSION:
                    return CL_INFO_RETURN_TYPE.OBJECT_ARRAY;

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
                    return CL_INFO_RETURN_TYPE.UINT;

                //SIZE_ARRAY
                case CL_DEVICE_INFO.MAX_WORK_ITEM_SIZES:
                    return CL_INFO_RETURN_TYPE.SIZET_ARRAY;

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
                    return CL_INFO_RETURN_TYPE.SIZET;

                //ULONG
                case CL_DEVICE_INFO.MAX_MEM_ALLOC_SIZE:
                case CL_DEVICE_INFO.GLOBAL_MEM_CACHE_SIZE:
                case CL_DEVICE_INFO.GLOBAL_MEM_SIZE:
                case CL_DEVICE_INFO.MAX_CONSTANT_BUFFER_SIZE:
                case CL_DEVICE_INFO.LOCAL_MEM_SIZE:
                    return CL_INFO_RETURN_TYPE.ULONG;

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
                    return CL_INFO_RETURN_TYPE.BOOL;

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
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;
            }


            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_CONTEXT_INFO info)
        {
            switch (info)
            {
                case CL_CONTEXT_INFO.DEVICES:
                case CL_CONTEXT_INFO.PROPERTIES:
                    return CL_INFO_RETURN_TYPE.OBJECT_ARRAY;

                case CL_CONTEXT_INFO.REFERENCE_COUNT:
                case CL_CONTEXT_INFO.NUM_DEVICES:
                    return CL_INFO_RETURN_TYPE.UINT;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_PROGRAM_BUILD_INFO info)
        {
            switch (info)
            {
                case CL_PROGRAM_BUILD_INFO.STATUS:
                case CL_PROGRAM_BUILD_INFO.BINARY_TYPE:
                    return CL_INFO_RETURN_TYPE.ENUM;

                case CL_PROGRAM_BUILD_INFO.GLOBAL_VARIABLE_TOTAL_SIZE:
                    return CL_INFO_RETURN_TYPE.UINT;

                case CL_PROGRAM_BUILD_INFO.OPTIONS:
                case CL_PROGRAM_BUILD_INFO.LOG:
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_PROGRAM_INFO info)
        {
            switch (info)
            {
                case CL_PROGRAM_INFO.REFERENCE_COUNT:
                case CL_PROGRAM_INFO.NUM_DEVICES:
                    return CL_INFO_RETURN_TYPE.UINT;

                case CL_PROGRAM_INFO.CONTEXT:
                    return CL_INFO_RETURN_TYPE.OBJECT;

                case CL_PROGRAM_INFO.DEVICES:
                    return CL_INFO_RETURN_TYPE.OBJECT_ARRAY;

                case CL_PROGRAM_INFO.SOURCE:
                case CL_PROGRAM_INFO.IL:
                case CL_PROGRAM_INFO.KERNEL_NAMES:
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;

                case CL_PROGRAM_INFO.BINARY_SIZES:
                    return CL_INFO_RETURN_TYPE.SIZET_ARRAY;

                case CL_PROGRAM_INFO.NUM_KERNELS:
                    return CL_INFO_RETURN_TYPE.SIZET;

                case CL_PROGRAM_INFO.SCOPE_GLOBAL_CTORS_PRESENT:
                case CL_PROGRAM_INFO.SCOPE_GLOBAL_DTORS_PRESENT:
                    return CL_INFO_RETURN_TYPE.BOOL;

                case CL_PROGRAM_INFO.BINARIES:
                    return CL_INFO_RETURN_TYPE.UNKNOWN;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_KERNEL_INFO info)
        {
            switch (info)
            {
                case CL_KERNEL_INFO.FUNCTION_NAME:
                case CL_KERNEL_INFO.ATTRIBUTES:
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;

                case CL_KERNEL_INFO.NUM_ARGS:
                case CL_KERNEL_INFO.REFERENCE_COUNT:
                    return CL_INFO_RETURN_TYPE.UINT;

                case CL_KERNEL_INFO.CONTEXT:
                case CL_KERNEL_INFO.PROGRAM:
                    return CL_INFO_RETURN_TYPE.OBJECT;  
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_KERNEL_ARG_INFO info)
        {
            switch (info)
            {
                case CL_KERNEL_ARG_INFO.ADDRESS_QUALIFIER:
                case CL_KERNEL_ARG_INFO.ACCESS_QUALIFIER:
                case CL_KERNEL_ARG_INFO.TYPE_QUALIFIER:
                    return CL_INFO_RETURN_TYPE.ENUM;

                case CL_KERNEL_ARG_INFO.TYPE_NAME:
                case CL_KERNEL_ARG_INFO.NAME:
                    return CL_INFO_RETURN_TYPE.CHAR_ARRAY;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_KERNEL_WORK_GROUP_INFO info)
        {
            switch (info)
            {
                case CL_KERNEL_WORK_GROUP_INFO.WORK_GROUP_SIZE:
                case CL_KERNEL_WORK_GROUP_INFO.PREFERRED_WORK_GROUP_SIZE_MULTIPLE:
                    return CL_INFO_RETURN_TYPE.SIZET;

                case CL_KERNEL_WORK_GROUP_INFO.COMPILE_WORK_GROUP_SIZE:
                case CL_KERNEL_WORK_GROUP_INFO.GLOBAL_WORK_SIZE:
                    return CL_INFO_RETURN_TYPE.SIZET_ARRAY;

                case CL_KERNEL_WORK_GROUP_INFO.LOCAL_MEM_SIZE:
                case CL_KERNEL_WORK_GROUP_INFO.PRIVATE_MEM_SIZE:
                    return CL_INFO_RETURN_TYPE.ULONG;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_MEM_INFO info)
        {
            switch (info)
            {
                case CL_MEM_INFO.TYPE:
                case CL_MEM_INFO.FLAGS:
                    return CL_INFO_RETURN_TYPE.ENUM;

                case CL_MEM_INFO.SIZE:
                case CL_MEM_INFO.OFFSET:
                    return CL_INFO_RETURN_TYPE.SIZET;

                case CL_MEM_INFO.REFERENCE_COUNT:
                case CL_MEM_INFO.MAP_COUNT:
                    return CL_INFO_RETURN_TYPE.UINT;

                case CL_MEM_INFO.CONTEXT:
                case CL_MEM_INFO.ASSOCIATED_MEMOBJECT:
                    return CL_INFO_RETURN_TYPE.OBJECT;

                case CL_MEM_INFO.USES_SVM_POINTER:
                    return CL_INFO_RETURN_TYPE.BOOL;

                case CL_MEM_INFO.HOST_PTR:
                    return CL_INFO_RETURN_TYPE.VOID_PTR;

                //case CL_MEM_INFO.PROPERTIES:
                //    break;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_IMAGE_INFO info)
        {
            switch (info)
            {
                case CL_IMAGE_INFO.FORMAT:
                    return CL_INFO_RETURN_TYPE.STRUCT;

                case CL_IMAGE_INFO.ELEMENT_SIZE:
                case CL_IMAGE_INFO.ROW_PITCH:
                case CL_IMAGE_INFO.SLICE_PITCH:
                case CL_IMAGE_INFO.WIDTH:
                case CL_IMAGE_INFO.HEIGHT:
                case CL_IMAGE_INFO.DEPTH:
                case CL_IMAGE_INFO.ARRAY_SIZE:
                    return CL_INFO_RETURN_TYPE.SIZET;

                case CL_IMAGE_INFO.NUM_MIP_LEVELS:
                case CL_IMAGE_INFO.NUM_SAMPLES:
                    return CL_INFO_RETURN_TYPE.UINT;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

        public static CL_INFO_RETURN_TYPE GetReturnType(CL_SAMPLER_INFO info)
        {
            switch (info)
            {
                case CL_SAMPLER_INFO.REFERENCE_COUNT:
                    return CL_INFO_RETURN_TYPE.UINT;

                case CL_SAMPLER_INFO.CONTEXT:
                    return CL_INFO_RETURN_TYPE.OBJECT;

                case CL_SAMPLER_INFO.NORMALIZED_COORDS:
                    return CL_INFO_RETURN_TYPE.BOOL;

                case CL_SAMPLER_INFO.ADDRESSING_MODE:
                case CL_SAMPLER_INFO.FILTER_MODE:
                case CL_SAMPLER_INFO.MIP_FILTER_MODE:
                    return CL_INFO_RETURN_TYPE.ENUM;

                case CL_SAMPLER_INFO.LOD_MIN:
                case CL_SAMPLER_INFO.LOD_MAX:
                    return CL_INFO_RETURN_TYPE.FLOAT;

                //case CL_SAMPLER_INFO.PROPERTIES:
                //    break;
            }

            return CL_INFO_RETURN_TYPE.UNKNOWN;
        }

    }
}
