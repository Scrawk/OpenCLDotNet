
global using cl_char = System.SByte; // int8_t
global using cl_uchar = System.Byte; // uint8_t
global using cl_short = System.Int16; // int16_t
global using cl_ushort = System.UInt16; // uint16_t
global using cl_int = System.Int32; // int32_t
global using cl_uint = System.UInt32; // uint32_t
global using cl_long = System.Int64; // int64_t
global using cl_ulong = System.UInt64; // uint64_t
global using cl_half = System.UInt16; //uint16_t
global using cl_float = System.Single; //float
global using cl_double = System.Double; //double

// WARNING!
// Unlike cl_ types in cl_platform.h,
// cl_bool is not guaranteed to be the same
// size as the bool in kernels.

global using cl_bool = System.UInt32; //cl_uint    
global using cl_bitfield = System.UInt64; //cl_ulong
global using cl_properties = System.UInt64; //cl_ulong 
global using cl_device_type = System.UInt64; //cl_bitfield
global using cl_platform_info = System.UInt32; //cl_uint
global using cl_device_info = System.UInt32; //cl_uint
global using cl_device_fp_config = System.UInt64;//cl_bitfield
global using cl_device_mem_cache_type = System.UInt32;//cl_uint
global using cl_device_local_mem_type = System.UInt32;//cl_uint
global using cl_device_exec_capabilities = System.UInt64;//cl_bitfield
global using cl_device_svm_capabilities = System.UInt64;//cl_bitfield
global using cl_command_queue_properties = System.UInt64;//cl_bitfield
global using cl_device_partition_property = System.IntPtr;//intptr_t
global using cl_device_affinity_domain = System.UInt64;//cl_bitfield
global using cl_context_properties = System.IntPtr;//intptr_t
global using cl_context_info = System.UInt32;//cl_uint
global using cl_queue_properties = System.UInt64;//cl_properties
global using cl_command_queue_info = System.UInt32;//cl_uint
global using cl_channel_order = System.UInt32;//cl_uint
global using cl_channel_type = System.UInt32;//cl_uint
global using cl_mem_flags = System.UInt64;//cl_bitfield
global using cl_svm_mem_flags = System.UInt64;//cl_bitfield
global using cl_mem_object_type = System.UInt32;//cl_uint
global using cl_mem_info = System.UInt32;//cl_uint
global using cl_mem_migration_flags = System.UInt64;//cl_bitfield
global using cl_image_info = System.UInt32;//cl_uint
global using cl_buffer_create_type = System.UInt32;//cl_uint
global using cl_addressing_mode = System.UInt32;//cl_uint
global using cl_filter_mode = System.UInt32;//cl_uint
global using cl_sampler_info = System.UInt32;//cl_uint
global using cl_map_flags = System.UInt64;//cl_bitfield
global using cl_pipe_properties = System.IntPtr;//intptr_t
global using cl_pipe_info = System.UInt32;//cl_uint
global using cl_program_info = System.UInt32;//cl_uint
global using cl_program_build_info = System.UInt32;//cl_uint
global using cl_program_binary_type = System.UInt32;//cl_uint
global using cl_build_status = System.UInt32;//cl_int
global using cl_kernel_info = System.UInt32;//cl_uint
global using cl_kernel_arg_info = System.UInt32;//cl_uint
global using cl_kernel_arg_address_qualifier = System.UInt32;//cl_uint
global using cl_kernel_arg_access_qualifier = System.UInt32;//cl_uint
global using cl_kernel_arg_type_qualifier = System.UInt64;//cl_bitfield
global using cl_kernel_work_group_info = System.UInt32;//cl_uint
global using cl_kernel_sub_group_info = System.UInt32;//cl_uint
global using cl_event_info = System.UInt32;//cl_uint
global using cl_command_type = System.UInt32;//cl_uint
global using cl_profiling_info = System.UInt32;//cl_uint
global using cl_sampler_properties = System.UInt64;//cl_properties
global using cl_kernel_exec_info = System.UInt32;//cl_uint
global using cl_device_atomic_capabilities = System.UInt64;//cl_bitfield
global using cl_device_device_enqueue_capabilities = System.UInt64;//cl_bitfield
global using cl_khronos_vendor_id = System.UInt32;//cl_uint
global using cl_mem_properties = System.UInt64;//cl_properties
global using cl_version = System.UInt32; //cl_uint
