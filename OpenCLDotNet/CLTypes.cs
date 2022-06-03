
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCLDotNet
{

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


    public readonly record struct size_t(UInt64 Value)
    {
        public static implicit operator size_t(UInt16 v) => new size_t(v);
        public static implicit operator size_t(UInt32 v) => new size_t(v);
        public static implicit operator size_t(UInt64 v) => new size_t(v);
        public static bool operator > (size_t v1, UInt64 v2) => v1.Value > v2;
        public static bool operator < (size_t v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(size_t v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(size_t v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_char(SByte Value)
    {
        public static implicit operator cl_char(SByte v) => new cl_char(v);
        public static bool operator >(cl_char v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_char v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_char v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_char v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_uchar(Byte Value)
    {
        public static implicit operator cl_uchar(Byte v) => new cl_uchar(v);
        public static bool operator >(cl_uchar v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_uchar v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_uchar v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_uchar v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_short(Int16 Value)
    {
        public static implicit operator cl_short(Int16 v) => new cl_short(v);
        public static bool operator >(cl_short v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_short v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_short v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_short v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_ushort(UInt16 Value)
    {
        public static implicit operator cl_ushort(UInt16 v) => new cl_ushort(v);
        public static bool operator >(cl_ushort v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_ushort v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_ushort v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_ushort v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_int(Int32 Value)
    {
        public static implicit operator cl_int(Int16 v) => new cl_int(v);
        public static implicit operator cl_int(Int32 v) => new cl_int(v);
        public static bool operator >(cl_int v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_int v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_int v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_int v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_uint(UInt32 Value)
    {
        public static implicit operator cl_uint(UInt16 v) => new cl_uint(v);
        public static implicit operator cl_uint(UInt32 v) => new cl_uint(v);
        public static bool operator >(cl_uint v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_uint v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_uint v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_uint v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_long(Int64 Value)
    {
        public static implicit operator cl_long(Int16 v) => new cl_long(v);
        public static implicit operator cl_long(Int32 v) => new cl_long(v);
        public static implicit operator cl_long(Int64 v) => new cl_long(v);
        public static bool operator >(cl_long v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_long v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_long v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_long v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_ulong(UInt64 Value)
    {
        public static implicit operator cl_ulong(UInt16 v) => new cl_ulong(v);
        public static implicit operator cl_ulong(UInt32 v) => new cl_ulong(v);
        public static implicit operator cl_ulong(UInt64 v) => new cl_ulong(v);
        public static bool operator >(cl_ulong v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_ulong v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_ulong v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_ulong v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_half(UInt16 Value)
    {
        public static implicit operator cl_half(UInt16 v) => new cl_half(v);
        public static bool operator >(cl_half v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_half v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_half v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_half v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_float(float Value)
    {
        public static implicit operator cl_float(Int16 v) => new cl_float(v);
        public static implicit operator cl_float(Int32 v) => new cl_float(v);
        public static implicit operator cl_float(Int64 v) => new cl_float(v);
        public static implicit operator cl_float(UInt16 v) => new cl_float(v);
        public static implicit operator cl_float(UInt32 v) => new cl_float(v);
        public static implicit operator cl_float(UInt64 v) => new cl_float(v);
        public static implicit operator cl_float(float v) => new cl_float(v);
        public static explicit operator cl_float(double v) => new cl_float((float)v);
        public static explicit operator cl_float(cl_double v) => new cl_float((float)v.Value);
        public static bool operator >(cl_float v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_float v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_float v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_float v1, Int64 v2) => v1.Value <= v2;
        public static bool operator >(cl_float v1, double v2) => v1.Value > v2;
        public static bool operator <(cl_float v1, double v2) => v1.Value < v2;
        public static bool operator >=(cl_float v1, double v2) => v1.Value >= v2;
        public static bool operator <=(cl_float v1, double v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_double(double Value)
    {
        public static implicit operator cl_double(Int16 v) => new cl_double(v);
        public static implicit operator cl_double(Int32 v) => new cl_double(v);
        public static implicit operator cl_double(Int64 v) => new cl_double(v);
        public static implicit operator cl_double(UInt16 v) => new cl_double(v);
        public static implicit operator cl_double(UInt32 v) => new cl_double(v);
        public static implicit operator cl_double(UInt64 v) => new cl_double(v);
        public static implicit operator cl_double(float v) => new cl_double(v);
        public static implicit operator cl_double(double v) => new cl_double(v);
        public static bool operator >(cl_double v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_double v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_double v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_double v1, Int64 v2) => v1.Value <= v2;
        public static bool operator >(cl_double v1, double v2) => v1.Value > v2;
        public static bool operator <(cl_double v1, double v2) => v1.Value < v2;
        public static bool operator >=(cl_double v1, double v2) => v1.Value >= v2;
        public static bool operator <=(cl_double v1, double v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_bool(UInt32 Value)
    {
        public static implicit operator cl_bool(bool v) => new cl_bool(v ? 1u : 0u);
        public static implicit operator cl_bool(UInt32 v) => new cl_bool(v);
        public override string ToString() => Value.ToString();
    }


}

