
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenCLDotNet.Core
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
    public readonly record struct cl_device_partition_property(UIntPtr Value);
    public readonly record struct cl_device_affinity_domain(UInt64 Value);
    public readonly record struct cl_context_properties(UIntPtr Value);
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
    public readonly record struct cl_pipe_properties(UIntPtr Value);
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


    public readonly record struct cl_object(UIntPtr Value)
    {
        public static implicit operator cl_object(UIntPtr v) => new cl_object(v);
        public static implicit operator UIntPtr(cl_object v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_object: Id={0}]", Value);
        }
    }

    public readonly record struct cl_platform_id(UIntPtr Value)
    {
        public static implicit operator cl_platform_id(UIntPtr v) => new cl_platform_id(v);
        public static implicit operator UIntPtr(cl_platform_id v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_platform_id: Id={0}]", Value);
        }
    }

    public readonly record struct cl_device_id(UIntPtr Value)
    {
        public static implicit operator cl_device_id(UIntPtr v) => new cl_device_id(v);
        public static implicit operator UIntPtr(cl_device_id v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_device_id: Id={0}]", Value);
        }
    }

    public readonly record struct cl_context(UIntPtr Value)
    {
        public static implicit operator cl_context(UIntPtr v) => new cl_context(v);
        public static implicit operator UIntPtr(cl_context v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_context: Id={0}]", Value);
        }
    }

    public readonly record struct cl_command_queue(UIntPtr Value)
    {
        public static implicit operator cl_command_queue(UIntPtr v) => new cl_command_queue(v);
        public static implicit operator UIntPtr(cl_command_queue v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_command_queue: Id={0}]", Value);
        }
    }

    public readonly record struct cl_mem(UIntPtr Value)
    {
        public static implicit operator cl_mem(UIntPtr v) => new cl_mem(v);
        public static implicit operator UIntPtr(cl_mem v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_mem: Id={0}]", Value);
        }
    }

    public readonly record struct cl_program(UIntPtr Value)
    {
        public static implicit operator cl_program(UIntPtr v) => new cl_program(v);
        public static implicit operator UIntPtr(cl_program v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_program: Id={0}]", Value);
        }
    }

    public readonly record struct cl_kernel(UIntPtr Value)
    {
        public static implicit operator cl_kernel(UIntPtr v) => new cl_kernel(v);
        public static implicit operator UIntPtr(cl_kernel v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_kernel: Id={0}]", Value);
        }
    }

    public readonly record struct cl_event(UIntPtr Value)
    {
        public static implicit operator cl_event(UIntPtr v) => new cl_event(v);
        public static implicit operator UIntPtr(cl_event v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_event: Id={0}]", Value);
        }
    }

    public readonly record struct cl_sampler(UIntPtr Value)
    {
        public static implicit operator cl_sampler(UIntPtr v) => new cl_sampler(v);
        public static implicit operator UIntPtr(cl_sampler v) => v.Value;

        public override string ToString()
        {
            return String.Format("[cl_sampler: Id={0}]", Value);
        }
    }


    public readonly record struct size_t(UInt64 Value)
    {
        public static implicit operator size_t(UInt16 v) => new size_t(v);
        public static implicit operator size_t(UInt32 v) => new size_t(v);
        public static implicit operator size_t(UInt64 v) => new size_t(v);
        public static implicit operator UInt64(size_t v) => (UInt64)v.Value;
  
        public static bool operator > (size_t v1, UInt64 v2) => v1.Value > v2;
        public static bool operator < (size_t v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(size_t v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(size_t v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_char(SByte Value)
    {
        public static implicit operator cl_char(SByte v) => new cl_char(v);
        public static implicit operator SByte(cl_char v) => (SByte)v.Value;
        public static implicit operator Char(cl_char v) => (Char)v.Value;
        public static explicit operator cl_char(Char v) => new cl_char((SByte)v);

        public static bool operator >(cl_char v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_char v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_char v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_char v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => ((Char)Value).ToString();
    }

    public readonly record struct cl_uchar(Byte Value)
    {
        public static implicit operator cl_uchar(Byte v) => new cl_uchar(v);
        public static implicit operator Byte(cl_uchar v) => (Byte)v.Value;
        public static explicit operator cl_uchar(Char v) => new cl_uchar((Byte)v);

        public static bool operator >(cl_uchar v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_uchar v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_uchar v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_uchar v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => ((Char)Value).ToString();
    }

    public readonly record struct cl_short(Int16 Value)
    {
        public static implicit operator cl_short(Int16 v) => new cl_short(v);
        public static implicit operator Int16(cl_short v) => (Int16)v.Value;
        public static implicit operator Int32(cl_short v) => (Int32)v.Value;
        public static implicit operator Int64(cl_short v) => (Int64)v.Value;

        public static bool operator >(cl_short v1, Int64 v2) => v1.Value > v2;
        public static bool operator <(cl_short v1, Int64 v2) => v1.Value < v2;
        public static bool operator >=(cl_short v1, Int64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_short v1, Int64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_ushort(UInt16 Value)
    {
        public static implicit operator cl_ushort(UInt16 v) => new cl_ushort(v);
        public static implicit operator UInt16(cl_ushort v) => (UInt16)v.Value;
        public static implicit operator UInt32(cl_ushort v) => (UInt32)v.Value;
        public static implicit operator UInt64(cl_ushort v) => (UInt64)v.Value;

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
        public static implicit operator Int32(cl_int v) => (Int32)v.Value;
        public static implicit operator Int64(cl_int v) => (Int64)v.Value;

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
        public static implicit operator UInt32(cl_uint v) => (UInt32)v.Value;
        public static implicit operator UInt64(cl_uint v) => (UInt64)v.Value;

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
        public static implicit operator Int64(cl_long v) => (Int64)v.Value;

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
        public static implicit operator UInt64(cl_ulong v) => (UInt64)v.Value;

        public static bool operator >(cl_ulong v1, UInt64 v2) => v1.Value > v2;
        public static bool operator <(cl_ulong v1, UInt64 v2) => v1.Value < v2;
        public static bool operator >=(cl_ulong v1, UInt64 v2) => v1.Value >= v2;
        public static bool operator <=(cl_ulong v1, UInt64 v2) => v1.Value <= v2;
        public override string ToString() => Value.ToString();
    }

    public readonly record struct cl_half(UInt16 Value)
    {
        public static implicit operator cl_half(UInt16 v) => new cl_half(v);
        public static implicit operator UInt16(cl_half v) => (UInt16)v.Value;

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
        public static implicit operator float(cl_float v) => (float)v.Value;
        public static implicit operator double(cl_float v) => (double)v.Value;

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
        public static implicit operator float(cl_double v) => (float)v.Value;
        public static implicit operator double(cl_double v) => (double)v.Value;

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
        public static cl_bool True => true;
        public static cl_bool False => false;

        public static implicit operator cl_bool(bool v) => new cl_bool(v ? 1u : 0u);
        public static implicit operator cl_bool(UInt32 v) => new cl_bool(v);
        public static implicit operator bool(cl_bool v) => v.Value != 0;
        public static implicit operator UInt64(cl_bool v) => v ? 1u : 0u;

        public static bool operator false(cl_bool b) => b.Value == 0;
        public static bool operator true(cl_bool b) => b.Value != 0;

        public override string ToString() => Value.ToString();
    }


}

