using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Core
{
    public enum CL_ERROR
    {
        SUCCESS = 0,
        DEVICE_NOT_FOUND = -1,
        DEVICE_NOT_AVAILABLE = -2,
        COMPILER_NOT_AVAILABLE = -3,
        MEM_OBJECT_ALLOCATION_FAILURE = -4,
        OUT_OF_RESOURCES = -5,
        OUT_OF_HOST_MEMORY = -6,
        PROFILING_INFO_NOT_AVAILABLE = -7,
        MEM_COPY_OVERLAP = -8,
        IMAGE_FORMAT_MISMATCH = -9,
        IMAGE_FORMAT_NOT_SUPPORTED = -10,
        BUILD_PROGRAM_FAILURE = -11,
        MAP_FAILURE = -12,
        MISALIGNED_SUB_BUFFER_OFFSET = -13,
        EXEC_STATUS_ERROR_FOR_EVENTS_IN_WAIT_LIST = -14,
        COMPILE_PROGRAM_FAILURE = -15,
        LINKER_NOT_AVAILABLE = -16,
        LINK_PROGRAM_FAILURE = -17,
        DEVICE_PARTITION_FAILED = -18,
        KERNEL_ARG_INFO_NOT_AVAILABLE = -19,
        INVALID_VALUE = -30,
        INVALID_DEVICE_TYPE = -31,
        INVALID_PLATFORM = -32,
        INVALID_DEVICE = -33,
        INVALID_CONTEXT = -34,
        INVALID_QUEUE_PROPERTIES = -35,
        INVALID_COMMAND_QUEUE = -36,
        INVALID_HOST_PTR = -37,
        INVALID_MEM_OBJECT = -38,
        INVALID_IMAGE_FORMAT_DESCRIPTOR = -39,
        INVALID_IMAGE_SIZE = -40,
        INVALID_SAMPLER = -41,
        INVALID_BINARY = -42,
        INVALID_BUILD_OPTIONS = -43,
        INVALID_PROGRAM = -44,
        INVALID_PROGRAM_EXECUTABLE = -45,
        INVALID_KERNEL_NAME = -46,
        INVALID_KERNEL_DEFINITION = -47,
        INVALID_KERNEL = -48,
        INVALID_ARG_INDEX = -49,
        INVALID_ARG_VALUE = -50,
        INVALID_ARG_SIZE = -51,
        INVALID_KERNEL_ARGS = -52,
        INVALID_WORK_DIMENSION = -53,
        INVALID_WORK_GROUP_SIZE = -54,
        INVALID_WORK_ITEM_SIZE = -55,
        INVALID_GLOBAL_OFFSET = -56,
        INVALID_EVENT_WAIT_LIST = -57,
        INVALID_EVENT = -58,
        INVALID_OPERATION = -59,
        INVALID_GL_OBJECT = -60,
        INVALID_BUFFER_SIZE = -61,
        INVALID_MIP_LEVEL = -62,
        INVALID_GLOBAL_WORK_SIZE = -63,
        INVALID_PROPERTY = -64,
        INVALID_IMAGE_DESCRIPTOR = -65,
        INVALID_COMPILER_OPTIONS = -66,
        INVALID_LINKER_OPTIONS = -67,
        INVALID_DEVICE_PARTITION_COUNT = -68,
        INVALID_PIPE_SIZE = -69,
        INVALID_DEVICE_QUEUE = -70,
        INVALID_SPEC_ID = -71,
        MAX_SIZE_RESTRICTION_EXCEEDED = -72,
    }

    public enum CL_PLATFORM_INFO
    {
        PROFILE = 0x0900,
        VERSION = 0x0901,
        NAME = 0x0902,
        VENDOR = 0x0903,
        EXTENSIONS = 0x0904,
        HOST_TIMER_RESOLUTION = 0x0905,
        NUMERIC_VERSION = 0x0906,
        EXTENSIONS_WITH_VERSION = 0x0907
    }


    [Flags]
    public enum CL_DEVICE_TYPE
    {
        DEFAULT = (1 << 0),
        CPU = (1 << 1),
        GPU = (1 << 2),
        ACCELERATOR = (1 << 3),
        CUSTOM = (1 << 4),
        ALL = ~0
    }

    public enum CL_DEVICE_INFO_RETURN_TYPE
    {
        UINT,
        ULONG,
        BOOL,
        SIZET,
        SIZET_ARRAY,
        CHAR_ARRAY,
        OBJECT,
        OBJECT_ARRAY,
        UNKNOWN
    }

    public enum CL_CONTEXT_INFO_RETURN_TYPE
    {
        UINT,
        OBJECT_ARRAY,
        UNKNOWN
    }

    public enum CL_PROGRAM_BUILD_INFO_RETURN_TYPE
    {
        UINT,
        CHAR_ARRAY,
        UNKNOWN
    }

    public enum CL_DEVICE_INFO
    {
        TYPE = 0x1000,
        VENDOR_ID = 0x1001,
        MAX_COMPUTE_UNITS = 0x1002,
        MAX_WORK_ITEM_DIMENSIONS = 0x1003,
        MAX_WORK_GROUP_SIZE = 0x1004,
        MAX_WORK_ITEM_SIZES = 0x1005,
        PREFERRED_VECTOR_WIDTH_CHAR = 0x1006,
        PREFERRED_VECTOR_WIDTH_SHORT = 0x1007,
        PREFERRED_VECTOR_WIDTH_INT = 0x1008,
        PREFERRED_VECTOR_WIDTH_LONG = 0x1009,
        PREFERRED_VECTOR_WIDTH_FLOAT = 0x100A,
        PREFERRED_VECTOR_WIDTH_DOUBLE = 0x100B,
        MAX_CLOCK_FREQUENCY = 0x100C,
        ADDRESS_BITS = 0x100D,
        MAX_READ_IMAGE_ARGS = 0x100E,
        MAX_WRITE_IMAGE_ARGS = 0x100F,
        MAX_MEM_ALLOC_SIZE = 0x1010,
        IMAGE2D_MAX_WIDTH = 0x1011,
        IMAGE2D_MAX_HEIGHT = 0x1012,
        IMAGE3D_MAX_WIDTH = 0x1013,
        IMAGE3D_MAX_HEIGHT = 0x1014,
        IMAGE3D_MAX_DEPTH = 0x1015,
        IMAGE_SUPPORT = 0x1016,
        MAX_PARAMETER_SIZE = 0x1017,
        MAX_SAMPLERS = 0x1018,
        MEM_BASE_ADDR_ALIGN = 0x1019,
        MIN_DATA_TYPE_ALIGN_SIZE = 0x101A,
        SINGLE_FP_CONFIG = 0x101B,
        GLOBAL_MEM_CACHE_TYPE = 0x101C,
        GLOBAL_MEM_CACHELINE_SIZE = 0x101D,
        GLOBAL_MEM_CACHE_SIZE = 0x101E,
        GLOBAL_MEM_SIZE = 0x101F,
        MAX_CONSTANT_BUFFER_SIZE = 0x1020,
        MAX_CONSTANT_ARGS = 0x1021,
        LOCAL_MEM_TYPE = 0x1022,
        LOCAL_MEM_SIZE = 0x1023,
        ERROR_CORRECTION_SUPPORT = 0x1024,
        PROFILING_TIMER_RESOLUTION = 0x1025,
        ENDIAN_LITTLE = 0x1026,
        DEVICE_AVAILABLE = 0x1027,
        COMPILER_AVAILABLE = 0x1028,
        EXECUTION_CAPABILITIES = 0x1029,
        QUEUE_ON_HOST_PROPERTIES = 0x102A,
        NAME = 0x102B,
        VENDOR = 0x102C,
        DRIVER_VERSION = 0x102D,
        PROFILE = 0x102E,
        VERSION = 0x102F,
        EXTENSIONS = 0x1030,
        PLATFORM = 0x1031,
        DOUBLE_FP_CONFIG = 0x1032,
        // 0x1033 reserved for HALF_FP_CONFIG which is already defined in "ext.h" 
        PREFERRED_VECTOR_WIDTH_HALF = 0x1034, 
        NATIVE_VECTOR_WIDTH_CHAR = 0x1036,
        NATIVE_VECTOR_WIDTH_SHORT = 0x1037,
        NATIVE_VECTOR_WIDTH_INT = 0x1038,
        NATIVE_VECTOR_WIDTH_LONG = 0x1039,
        NATIVE_VECTOR_WIDTH_FLOAT = 0x103A,
        NATIVE_VECTOR_WIDTH_DOUBLE = 0x103B,
        NATIVE_VECTOR_WIDTH_HALF = 0x103C,
        OPENC_VERSION = 0x103D,
        LINKER_AVAILABLE = 0x103E,
        BUILT_IN_KERNELS = 0x103F,
        IMAGE_MAX_BUFFER_SIZE = 0x1040,
        IMAGE_MAX_ARRAY_SIZE = 0x1041,
        PARENT_DEVICE = 0x1042,
        PARTITION_MAX_SUB_DEVICES = 0x1043,
        PARTITION_PROPERTIES = 0x1044,
        PARTITION_AFFINITY_DOMAIN = 0x1045,
        PARTITION_TYPE = 0x1046,
        REFERENCE_COUNT = 0x1047,
        PREFERRED_INTEROP_USER_SYNC = 0x1048,
        PRINTF_BUFFER_SIZE = 0x1049,
        IMAGE_PITCH_ALIGNMENT = 0x104A,
        IMAGE_BASE_ADDRESS_ALIGNMENT = 0x104B,
        MAX_READ_WRITE_IMAGE_ARGS = 0x104C,
        MAX_GLOBAL_VARIABLE_SIZE = 0x104D,
        QUEUE_ON_QUEUE_PROPERTIES = 0x104E,
        QUEUE_ON_PREFERRED_SIZE = 0x104F,
        QUEUE_ON_MAX_SIZE = 0x1050,
        MAX_ON_QUEUES = 0x1051,
        MAX_ON_EVENTS = 0x1052,
        SVM_CAPABILITIES = 0x1053,
        GLOBAL_VARIABLE_PREFERRED_TOTAL_SIZE = 0x1054,
        MAX_PIPE_ARGS = 0x1055,
        PIPE_MAX_ACTIVE_RESERVATIONS = 0x1056,
        PIPE_MAX_PACKET_SIZE = 0x1057,
        PREFERRED_PLATFORM_ATOMIC_ALIGNMENT = 0x1058,
        PREFERRED_GLOBAL_ATOMIC_ALIGNMENT = 0x1059,
        PREFERRED_LOCAL_ATOMIC_ALIGNMENT = 0x105A,
        IL_VERSION = 0x105B,
        MAX_NUM_SUB_GROUPS = 0x105C,
        SUB_GROUP_INDEPENDENT_FORWARD_PROGRESS = 0x105D,
        NUMERIC_VERSION = 0x105E,
        EXTENSIONS_WITH_VERSION = 0x1060,
        ILS_WITH_VERSION = 0x1061,
        BUILT_IN_KERNELS_WITH_VERSION = 0x1062,
        ATOMIC_MEMORY_CAPABILITIES = 0x1063,
        ATOMIC_FENCE_CAPABILITIES = 0x1064,
        NON_UNIFORM_WORK_GROUP_SUPPORT = 0x1065,
        OPENC_ALL_VERSIONS = 0x1066,
        PREFERRED_WORK_GROUP_SIZE_MULTIPLE = 0x1067,
        WORK_GROUP_COLLECTIVE_FUNCTIONS_SUPPORT = 0x1068,
        GENERIC_ADDRESS_SPACE_SUPPORT = 0x1069,
        // 0x106A to 0x106E - Reserved for upcoming KHR extension 
        OPENC_FEATURES = 0x106F,
        ENQUEUE_CAPABILITIES = 0x1070,
        PIPE_SUPPORT = 0x1071,
        LATEST_CONFORMANCE_VERSION_PASSED = 0x1072
    }

    // device_fp_config - bitfield 
    [Flags]
    public enum CL_DEVICE_FP_CONFIG
    {
        DENORM = (1 << 0),
        INF_NAN = (1 << 1),
        ROUND_TO_NEAREST = (1 << 2),
        ROUND_TO_ZERO = (1 << 3),
        ROUND_TO_INF = (1 << 4),
        FMA = (1 << 5),
        SOFT_FLOAT = (1 << 6),
        CORRECTLY_ROUNDED_DIVIDE_SQRT = (1 << 7)
    }

    // device_mem_cache_type 
    public enum CL_DEVIVE_MEM_CACHE_TYPE
    {
        NONE = 0x0,
        READ_ONLY_CACHE = 0x1,
        READ_WRITE_CACHE = 0x2
    }

    // device_local_mem_type 
    public enum CL_DEVICE_LOCAL_MEM_TYPE
    {
        LOCAL = 0x1,
        GLOBAL = 0x2
    }

    // device_exec_capabilities - bitfield 
    [Flags]
    public enum CL_DEVICE_EXEC_CAPABILITIES
    {
        KERNEL = (1 << 0),
        NATIVE_KERNEL = (1 << 1)
    }

    // command_queue_properties - bitfield 
    [Flags]
    public enum CL_COMMAND_QUEUE_POPERTIES
    {
        OUT_OF_ORDER_EXEC_MODE_ENABLE = (1 << 0),
        PROFILING_ENABLE = (1 << 1),
        ON_DEVICE = (1 << 2),
        ON_DEVICE_DEFAULT = (1 << 3)
    }

    // context_info 
    public enum CL_CONTEXT_INFO
    {
        REFERENCE_COUNT = 0x1080,
        DEVICES = 0x1081,
        PROPERTIES = 0x1082,
        NUM_DEVICES = 0x1083
    }

    // context_properties 
    public enum CL_CONTEXT_PROPERTIES
    {
        PLATFORM = 0x1084,
        INTEROP_USER_SYNC = 0x1085
    }

    // device_partition_property 
    public enum CL_DEVICE_PARTITION_PROPERTY
    {
        EQUALLY = 0x1086,
        BY_COUNTS = 0x1087,
        BY_COUNTS_LIST_END = 0x0,
        BY_AFFINITY_DOMAIN = 0x1088
    }

    // device_affinity_domain 
    [Flags]
    public enum CL_DEVICE_AFFINITY_DOMAIN
    {
        NUMA = (1 << 0),
        L4_CACHE = (1 << 1),
        L3_CACHE = (1 << 2),
        L2_CACHE = (1 << 3),
        L1_CACHE = (1 << 4),
        NEXT_PARTITIONABLE = (1 << 5)
    }

    // device_svm_capabilities 
    [Flags]
    public enum CL_DEVICE_SVM_CAPABILITIES
    {
        COARSE_GRAIN_BUFFER = (1 << 0),
        FINE_GRAIN_BUFFER = (1 << 1),
        FINE_GRAIN_SYSTEM = (1 << 2),
        ATOMICS = (1 << 3)
    }

    // command_queue_info 
    public enum CL_COMMAND_QUEUE_INFO
    {
        CONTEXT = 0x1090,
        DEVICE = 0x1091,
        REFERENCE_COUNT = 0x1092,
        PROPERTIES = 0x1093,
        SIZE = 0x1094,
        DEVICE_DEFAULT = 0x1095,
        PROPERTIES_ARRAY = 0x1098
    }

    // mem_flags and svm_mem_flags - bitfield 
    [Flags]
    public enum CL_MEM_FLAGS
    {
        READ_WRITE = (1 << 0),
        WRITE_ONLY = (1 << 1),
        READ_ONLY = (1 << 2),
        USE_HOST_PTR = (1 << 3),
        ALLOC_HOST_PTR = (1 << 4),
        COPY_HOST_PTR = (1 << 5),
        // reserved = (1 << 6),   
        HOST_WRITE_ONLY = (1 << 7),
        HOST_READ_ONLY = (1 << 8),
        HOST_NO_ACCESS = (1 << 9),
        // used by svm_mem_flags only 
        SVM_FINE_GRAIN_BUFFER = (1 << 10),
        // used by svm_mem_flags only
        SVM_ATOMICS = (1 << 11),   
        KERNEL_READ_AND_WRITE = (1 << 12)
    }

    // mem_migration_flags - bitfield 
    [Flags]
    public enum CL_MEM_MIGRATION_FLAGS
    {
        OBJECT_HOST = (1 << 0),
        OBJECT_CONTENT_UNDEFINED = (1 << 1)
    }

    // channel_order 
    public enum CL_CHANNEL_ORDER
    {
        R = 0x10B0,
        A = 0x10B1,
        RG = 0x10B2,
        RA = 0x10B3,
        RGB = 0x10B4,
        RGBA = 0x10B5,
        BGRA = 0x10B6,
        ARGB = 0x10B7,
        INTENSITY = 0x10B8,
        LUMINANCE = 0x10B9,
        Rx = 0x10BA,
        RGx = 0x10BB,
        RGBx = 0x10BC,
        DEPTH = 0x10BD,
        DEPTH_STENCIL = 0x10BE,
        sRGB = 0x10BF,
        sRGBx = 0x10C0,
        sRGBA = 0x10C1,
        sBGRA = 0x10C2,
        ABGR = 0x10C3
    }

    // channel_type 
    public enum CL_CHANNEL_TYPE
    {
        SNORM_INT8 = 0x10D0,
        SNORM_INT16 = 0x10D1,
        UNORM_INT8 = 0x10D2,
        UNORM_INT16 = 0x10D3,
        UNORM_SHORT_565 = 0x10D4,
        UNORM_SHORT_555 = 0x10D5,
        UNORM_INT_101010 = 0x10D6,
        SIGNED_INT8 = 0x10D7,
        SIGNED_INT16 = 0x10D8,
        SIGNED_INT32 = 0x10D9,
        UNSIGNED_INT8 = 0x10DA,
        UNSIGNED_INT16 = 0x10DB,
        UNSIGNED_INT32 = 0x10DC,
        HALF_FLOAT = 0x10DD,
        FLOAT = 0x10DE,
        UNORM_INT24 = 0x10DF,
        UNORM_INT_101010_2 = 0x10E0
    }

    // mem_object_type 
    public enum CL_MEM_OBJECT_TYPE
    {
        BUFFER = 0x10F0,
        IMAGE2D = 0x10F1,
        IMAGE3D = 0x10F2,
        IMAGE2D_ARRAY = 0x10F3,
        IMAGE1D = 0x10F4,
        IMAGE1D_ARRAY = 0x10F5,
        IMAGE1D_BUFFER = 0x10F6,
        PIPE = 0x10F7
    }

    // mem_info 
    public enum CL_MEM_INFO
    {
        TYPE = 0x1100,
        FLAGS = 0x1101,
        SIZE = 0x1102,
        HOST_PTR = 0x1103,
        MAP_COUNT = 0x1104,
        REFERENCE_COUNT = 0x1105,
        CONTEXT = 0x1106,
        ASSOCIATED_MEMOBJECT = 0x1107,
        OFFSET = 0x1108,
        USES_SVM_POINTER = 0x1109,
        PROPERTIES = 0x110A
    }

    // image_info 
    public enum CL_IMAGE_INFO
    {
        FORMAT = 0x1110,
        ELEMENT_SIZE = 0x1111,
        ROW_PITCH = 0x1112,
        SLICE_PITCH = 0x1113,
        WIDTH = 0x1114,
        HEIGHT = 0x1115,
        DEPTH = 0x1116,
        ARRAY_SIZE = 0x1117,
        BUFFER = 0x1118,
        NUM_MIP_LEVELS = 0x1119,
        NUM_SAMPLES = 0x111A
    }


    // pipe_info 
    public enum CL_PIP_INFO
    {
        PACKET_SIZE = 0x1120,
        MAX_PACKETS = 0x1121,
        PROPERTIES = 0x1122
    }

    // addressing_mode 
    public enum ADDRESSING_MODE
    {
        NONE = 0x1130,
        CLAMP_TO_EDGE = 0x1131,
        CLAMP = 0x1132,
        REPEAT = 0x1133,
        MIRRORED_REPEAT = 0x1134
    }

    // filter_mode 
    public enum CL_FILTER_MODE
    {
        NEAREST = 0x1140,
        LINEAR = 0x1141
    }

    // sampler_info 
    public enum CL_SAMPLE_INFO
    {
        REFERENCE_COUNT = 0x1150,
        CONTEXT = 0x1151,
        NORMALIZED_COORDS = 0x1152,
        ADDRESSING_MODE = 0x1153,
        FILTER_MODE = 0x1154,
        // These enumerants are for the khr_mipmap_image extension.
        // They have since been added to ext.h with an appropriate
        // KHR suffix, but are left here for backwards compatibility. 
        MIP_FILTER_MODE = 0x1155,
        LOD_MIN = 0x1156,
        LOD_MAX = 0x1157,
        PROPERTIES = 0x1158
    }

    // map_flags - bitfield 
    [Flags]
    public enum CL_MAP_FLAGS
    {
        READ = (1 << 0),
        WRITE = (1 << 1),
        WRITE_INVALIDATE_REGION = (1 << 2)
    }

    // program_info 
    public enum CL_PROGRAM_INFO
    {
        REFERENCE_COUNT = 0x1160,
        CONTEXT = 0x1161,
        NUM_DEVICES = 0x1162,
        DEVICES = 0x1163,
        SOURCE = 0x1164,
        BINARY_SIZES = 0x1165,
        BINARIES = 0x1166,
        NUM_KERNELS = 0x1167,
        KERNEL_NAMES = 0x1168,
        IL = 0x1169,
        SCOPE_GLOBAL_CTORS_PRESENT = 0x116A,
        SCOPE_GLOBAL_DTORS_PRESENT = 0x116B
    }

    // program_build_info 
    public enum CL_PROGRAM_BUILD_INFO
    {
        STATUS = 0x1181,
        OPTIONS = 0x1182,
        LOG = 0x1183,
        BINARY_TYPE = 0x1184,
        GLOBAL_VARIABLE_TOTAL_SIZE = 0x1185
    }

    // program_binary_type 
    public enum CL_PROGRAM_BINARY_TYPE
    {
        NONE = 0x0,
        COMPILED_OBJECT = 0x1,
        LIBRARY = 0x2,
        EXECUTABLE = 0x4
    }

    // build_status 
    public enum CL_BUILD_STATUS
    {
        SUCCESS = 0,
        NONE = -1,
        ERROR = -2,
        IN_PROGRESS = -3
    }

    // kernel_info 
    public enum CL_KERNEL_INFO
    {
        FUNCTION_NAME = 0x1190,
        NUM_ARGS = 0x1191,
        REFERENCE_COUNT = 0x1192,
        CONTEXT = 0x1193,
        PROGRAM = 0x1194,
        ATTRIBUTES = 0x1195
    }

    // kernel_arg_info 
    public enum CL_KERNEL_ARG_INFO
    {
        ADDRESS_QUALIFIER = 0x1196,
        ACCESS_QUALIFIER = 0x1197,
        TYPE_NAME = 0x1198,
        TYPE_QUALIFIER = 0x1199,
        NAME = 0x119A
    }

    // kernel_arg_address_qualifier 
    public enum CL_KERNEL_ARG_ADDRESS_QUALIFIER
    {
        GLOBAL = 0x119B,
        LOCAL = 0x119C,
        CONSTANT = 0x119D,
        PRIVATE = 0x119E
    }

    // kernel_arg_access_qualifier 
    public enum CL_KERNEL_ARG_ACCESS_QUALIFIER
    {
        READ_ONLY = 0x11A0,
        WRITE_ONLY = 0x11A1,
        READ_WRITE = 0x11A2,
        NONE = 0x11A3
    }

    // kernel_arg_type_qualifier 
    [Flags]
    public enum CL_KERNEL_ARG_TYPE_QUALIFIER
    {
        NONE = 0,
        CONST = (1 << 0),
        RESTRICT = (1 << 1),
        VOLATILE = (1 << 2),
        PIPE = (1 << 3)
    }

    // kernel_work_group_info 
    public enum CL_KERNEL_WORK_GROUP_INFO
    {
        WORK_GROUP_SIZE = 0x11B0,
        COMPILE_WORK_GROUP_SIZE = 0x11B1,
        LOCAL_MEM_SIZE = 0x11B2,
        PREFERRED_WORK_GROUP_SIZE_MULTIPLE = 0x11B3,
        PRIVATE_MEM_SIZE = 0x11B4,
        GLOBAL_WORK_SIZE = 0x11B5
    }

    // kernel_sub_group_info 
    public enum CL_KERNEL_SUB_GROUP_INFO
    {
        MAX_SUB_GROUP_SIZE_FOR_NDRANGE = 0x2033,
        SUB_GROUP_COUNT_FOR_NDRANGE = 0x2034,
        LOCAL_SIZE_FOR_SUB_GROUP_COUNT = 0x11B8,
        MAX_NUM_SUB_GROUPS = 0x11B9,
        COMPILE_NUM_SUB_GROUPS = 0x11BA
    }

    // kernel_exec_info 
    public enum CL_KERNEL_EXEC_INFO
    {
        SVM_PTRS = 0x11B6,
        SVM_FINE_GRAIN_SYSTEM = 0x11B7
    }

    // event_info 
    public enum CL_EVENT_INFO
    {
        COMMAND_QUEUE = 0x11D0,
        COMMAND_TYPE = 0x11D1,
        REFERENCE_COUNT = 0x11D2,
        COMMAND_EXECUTION_STATUS = 0x11D3,
        CONTEXT = 0x11D4
    }

    // command_type 
    public enum CL_COMMAND_TYPE
    {
        NDRANGE_KERNEL = 0x11F0,
        TASK = 0x11F1,
        NATIVE_KERNEL = 0x11F2,
        READ_BUFFER = 0x11F3,
        WRITE_BUFFER = 0x11F4,
        COPY_BUFFER = 0x11F5,
        READ_IMAGE = 0x11F6,
        WRITE_IMAGE = 0x11F7,
        COPY_IMAGE = 0x11F8,
        COPY_IMAGE_TO_BUFFER = 0x11F9,
        COPY_BUFFER_TO_IMAGE = 0x11FA,
        MAP_BUFFER = 0x11FB,
        MAP_IMAGE = 0x11FC,
        UNMAP_MEM_OBJECT = 0x11FD,
        MARKER = 0x11FE,
        ACQUIRE_GL_OBJECTS = 0x11FF,
        RELEASE_GL_OBJECTS = 0x1200,
        READ_BUFFER_RECT = 0x1201,
        WRITE_BUFFER_RECT = 0x1202,
        COPY_BUFFER_RECT = 0x1203,
        USER = 0x1204,
        BARRIER = 0x1205,
        MIGRATE_MEM_OBJECTS = 0x1206,
        FILL_BUFFER = 0x1207,
        FILL_IMAGE = 0x1208,
        SVM_FREE = 0x1209,
        SVM_MEMCPY = 0x120A,
        SVM_MEMFILL = 0x120B,
        SVM_MAP = 0x120C,
        SVM_UNMAP = 0x120D,
        SVM_MIGRATE_MEM = 0x120E
    }

    // command execution status 
    public enum CL_COMMAND_EXECUTION_STATUS
    {
        COMPLETE = 0x0,
        RUNNING = 0x1,
        SUBMITTED = 0x2,
        QUEUED = 0x3
    }

    // buffer_create_type 
    public enum CL_BUFFER_CREATION_TYPE
    {
        REGION = 0x1220
    }

    // profiling_info 
    public enum CL_PROFILING_INFO
    {
        QUEUED = 0x1280,
        SUBMIT = 0x1281,
        START = 0x1282,
        END = 0x1283,
        COMPLETE = 0x1284
    }

    // device_atomic_capabilities - bitfield 
    [Flags]
    public enum CL_DEVICE_ATOMIC_CAPABILITIES
    {
        ORDER_RELAXED = (1 << 0),
        ORDER_ACQ_REL = (1 << 1),
        ORDER_SEQ_CST = (1 << 2),
        SCOPE_WORK_ITEM = (1 << 3),
        SCOPE_WORK_GROUP = (1 << 4),
        SCOPE_DEVICE = (1 << 5),
        SCOPE_ALL_DEVICES = (1 << 6)
    }

    //device_enqueue_capabilities - bitfield 
    [Flags]
    public enum CL_DEVICE_ENQUEUE_CAPABILITIES
    {
        SUPPORTED = (1 << 0),
        REPLACEABLE_DEFAULT = (1 << 1)
    }


}
