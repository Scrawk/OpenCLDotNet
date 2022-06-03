
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCLDotNet
{

    public readonly record struct size_t(UInt64 Value);
    public readonly record struct cl_char(SByte Value);
    public readonly record struct cl_uchar(Byte Value);
    public readonly record struct cl_short(Int16 Value);
    public readonly record struct cl_ushort(UInt16 Value);
    public readonly record struct cl_int(Int32 Value);
    public readonly record struct cl_uint(UInt32 Value);
    public readonly record struct cl_long(Int64 Value);
    public readonly record struct cl_ulong(UInt64 Value);
    public readonly record struct cl_half(UInt16 Value);
    public readonly record struct cl_float(float Value);
    public readonly record struct cl_double(double Value);


    public readonly record struct cl_bool(UInt32 Value);
    public readonly record struct cl_bitfield(UInt64 Value);
    public readonly record struct cl_properties(UInt64 Value);
    public readonly record struct cl_device_type(UInt64 Value);
    public readonly record struct cl_platform_info(UInt32 Value);
    public readonly record struct cl_device_info(UInt32 Value);
    public readonly record struct cl_device_fp_config(UInt64 Value);
    public readonly record struct cl_device_mem_cache_type(UInt32 Value);
    public readonly record struct cl_device_local_mem_type(UInt32 Value);
    public readonly record struct cl_device_exec_capabilities(UInt64 Value);
    public readonly record struct cl_device_svm_capabilities(UInt64 Value);
    public readonly record struct cl_command_queue_properties(UInt64 Value);
    public readonly record struct cl_device_partition_property(IntPtr Value);
    public readonly record struct cl_device_affinity_domain(UInt64 Value);
    public readonly record struct cl_context_properties(IntPtr Value);
    public readonly record struct cl_context_info(UInt32 Value);
    public readonly record struct cl_queue_properties(UInt64 Value);
    public readonly record struct cl_command_queue_info(UInt32 Value);
    public readonly record struct cl_channel_order(UInt32 Value);
    public readonly record struct cl_channel_type(UInt32 Value);
    public readonly record struct cl_mem_flags(UInt64 Value);
    public readonly record struct cl_svm_mem_flags(UInt64 Value);
    public readonly record struct cl_mem_object_type(UInt32 Value);
    public readonly record struct cl_mem_info(UInt32 Value);
    public readonly record struct cl_mem_migration_flags(UInt64 Value);
    public readonly record struct cl_image_info(UInt32 Value);
    public readonly record struct cl_buffer_create_type(UInt32 Value);
    public readonly record struct cl_addressing_mode(UInt32 Value);
    public readonly record struct cl_filter_mode(UInt32 Value);
    public readonly record struct cl_sampler_info(UInt32 Value);
    public readonly record struct cl_map_flags(UInt64 Value);
    public readonly record struct cl_pipe_properties(IntPtr Value);
    public readonly record struct cl_pipe_info(UInt32 Value);
    public readonly record struct cl_program_info(UInt32 Value);
    public readonly record struct cl_program_build_info(UInt32 Value);
    public readonly record struct cl_program_binary_type(UInt32 Value);
    public readonly record struct cl_build_status(UInt32 Value);
    public readonly record struct cl_kernel_info(UInt32 Value);
    public readonly record struct cl_kernel_arg_info(UInt32 Value);
    public readonly record struct cl_kernel_arg_address_qualifier(UInt32 Value);
    public readonly record struct cl_kernel_arg_access_qualifier(UInt32 Value);
    public readonly record struct cl_kernel_arg_type_qualifier(UInt64 Value);
    public readonly record struct cl_kernel_work_group_info(UInt32 Value);
    public readonly record struct cl_kernel_sub_group_info(UInt32 Value);
    public readonly record struct cl_event_info(UInt32 Value);
    public readonly record struct cl_command_type(UInt32 Value);
    public readonly record struct cl_profiling_info(UInt32 Value);
    public readonly record struct cl_sampler_properties(UInt64 Value);
    public readonly record struct cl_kernel_exec_info(UInt32 Value);
    public readonly record struct cl_device_atomic_capabilities(UInt64 Value);
    public readonly record struct cl_device_device_enqueue_capabilities(UInt64 Value);
    public readonly record struct cl_khronos_vendor_id(UInt32 Value);
    public readonly record struct cl_mem_properties(UInt64 Value);
    public readonly record struct cl_version(UInt32 Value);


    public readonly record struct cl_platform_id(IntPtr Value);
    public readonly record struct cl_device_id(IntPtr Value);
    public readonly record struct cl_context(IntPtr Value);
    public readonly record struct cl_command_queue(IntPtr Value);
    public readonly record struct cl_mem(IntPtr Value);
    public readonly record struct cl_program(IntPtr Value);
    public readonly record struct cl_kernel(IntPtr Value);
    public readonly record struct cl_event(IntPtr Value);
    public readonly record struct cl_sampler(IntPtr Value);


}

