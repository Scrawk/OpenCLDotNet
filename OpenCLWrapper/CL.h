#pragma once

//#define CL_TARGET_OPENCL_VERSION 100
//#define CL_TARGET_OPENCL_VERSION 110
//#define CL_TARGET_OPENCL_VERSION 120
//#define CL_TARGET_OPENCL_VERSION 200
//#define CL_TARGET_OPENCL_VERSION 210
#define CL_TARGET_OPENCL_VERSION 220
//#define CL_TARGET_OPENCL_VERSION 300

/*
cl_khr_fp64	//Double precision floating - point
cl_khr_select_fprounding_mode	//Specify rounding mode
cl_khr_global_int32_base_atomics	//32 - bit global integer base atomic operations
cl_khr_global_int32_extended_atomics	//32 - bit global integer extended atomic operations
cl_khr_local_int32_base_atomics	//32 - bit local integer base atomic operations
cl_khr_local_int32_extended_atomics	//32 - bit local integer extended atomic operations
cl_khr_int64_base_atomics	//64 - bit integer base atomic operations
cl_khr_int64_extended_atomics	//64 - bit integer extended atomic operations
cl_khr_3d_image_writes	//Writes to 3D image objects
cl_khr_byte_addressable_store	//Allow byte addressible stores
cl_khr_fp16	//Half precision floating - point
CL_APPLE_gl_sharing	//MacOS X OpenGL sharing
CL_KHR_gl_sharing //OpenGL sharing
*/

#pragma OPENCL EXTENSION cl_khr_fp16 : enable
#pragma OPENCL EXTENSION cl_khr_fp64 : enable
#pragma OPENCL EXTENSION cles_khr_int64 : enable
#pragma OPENCL EXTENSION cl_khr_3d_image_writes : enable
//#pragma OPENCL EXTENSION cl_amd_printf : enable

#include "OpenCLWrapper.h"
#include <CL/cl.h>

struct SamplerProperties
{
    cl_bool normalizedCoords;
    int addressingMode;
    int filterMode;
    //int mipFilterMode;
    //cl_float LODMin;
    //cl_float LODMax;
};

struct ContextProperties
{
    cl_long platform;
    //cl_bool interopUserSync;
};

struct CommandQueueProperties
{
    cl_command_queue_properties properties;
    cl_uint queueSize;
};

extern "C"
{

    CL_WRAPPER_API int CL_VersionNumber();

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                PLATFORM FUNCTIONS                                         ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_int CL_GetPlatformCount(
        cl_uint* num_platforms);

    CL_WRAPPER_API cl_int CL_GetPlatformIDs(
        cl_uint num_entries,
        cl_platform_id* platforms);

    CL_WRAPPER_API cl_int CL_GetPlatformInfoSize(
        cl_platform_id   platform,
        cl_platform_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetPlatformInfo(
        cl_platform_id   platform,
        cl_platform_info param_name,
        size_t           param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_UnloadPlatformCompiler(cl_platform_id platform);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                DEVICE FUNCTIONS                                           ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_int CL_GetDeviceIDs(
        cl_platform_id   platform,
        cl_device_type   device_type,
        cl_uint          num_entries,
        cl_device_id* devices);

    CL_WRAPPER_API cl_int CL_GetDeviceCount(
        cl_platform_id   platform,
        cl_device_type   device_type,
        cl_uint* num_devices);

    CL_WRAPPER_API cl_int CL_GetDeviceInfoSize(
        cl_device_id    device,
        cl_device_info  param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetDeviceInfo(
        cl_device_id    device,
        cl_device_info  param_name,
        size_t          param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_RetainDevice(cl_device_id device);

    CL_WRAPPER_API cl_int CL_ReleaseDevice(cl_device_id device);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                CONTEXT FUNCTIONS                                          ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_context CL_CreateContext(
        ContextProperties properties,
        cl_uint num_devices,
        const cl_device_id* devices);

    CL_WRAPPER_API cl_int CL_GetContextInfoSize(
        cl_context context,
        cl_context_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetContextInfo(
        cl_context context,
        cl_context_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_RetainContext(cl_context context);

    CL_WRAPPER_API cl_int CL_ReleaseContext(cl_context context);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                PROGRAM FUNCTIONS                                          ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_program CL_CreateProgramWithSource(
        cl_context context,
        cl_uint count,
        const char* strings,
        size_t length,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_program CL_CreateProgramWithBinary(
        cl_context context,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const size_t* lengths,
        const unsigned char* binaries,
        cl_int* binary_status,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_program CL_CreateProgramWithBuiltInKernels(
        cl_context context,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const char* kernel_names,
        cl_int* error);

    CL_WRAPPER_API cl_program CL_CreateProgramWithIL(
        cl_context context,
        const void* il,
        size_t length,
        cl_int* error);

    CL_WRAPPER_API cl_int CL_BuildProgram(
        cl_program program,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const char* options);

    CL_WRAPPER_API cl_int CL_CompileProgram(
        cl_program program,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const char* options,
        cl_uint num_input_headers,
        const cl_program* input_headers,
        const char** header_include_names);

    CL_WRAPPER_API cl_program CL_LinkProgram(
        cl_context context,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const char* options,
        cl_uint num_input_programs,
        const cl_program* input_programs,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_int CL_GetProgramBuildInfoSize(
        cl_program program,
        cl_device_id device,
        cl_program_build_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetProgramBuildInfo(
        cl_program program,
        cl_device_id device,
        cl_program_build_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_GetProgramInfoSize(
        cl_program         program,
        cl_program_info    param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetProgramInfo(
        cl_program         program,
        cl_program_info    param_name,
        size_t             param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_GetProgramBinaries(
        cl_program         program,
        int num_devices,
        size_t* sizes,
        unsigned char* binaries);

    CL_WRAPPER_API cl_int CL_RetainProgram(cl_program program);

    CL_WRAPPER_API cl_int CL_ReleaseProgram(cl_program program);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                KERNEL FUNCTIONS                                           ///
    ///////////////////////////////////////////////////////////////////////////////////////////////


    CL_WRAPPER_API cl_kernel CL_CreateKernel(
        cl_program program,
        const char* kernel_name,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_kernel CL_CloneKernel(
        cl_kernel source_kernel,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_int CL_SetKernelArgMem(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_mem arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgSampler(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_sampler arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgDouble(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_double arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgFloat(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_float arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgLong(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_long arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgULong(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_ulong arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgInt(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_int arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgUInt(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_uint arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgShort(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_short arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgUShort(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_ushort arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgSByte(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_char arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgByte(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_uchar arg_value);

    CL_WRAPPER_API cl_int CL_GetKernelInfoSize(
        cl_kernel kernel,
        cl_kernel_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetKernelInfo(
        cl_kernel kernel,
        cl_kernel_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_GetKernelArgInfoSize(
        cl_kernel kernel,
        cl_uint arg_indx,
        cl_kernel_arg_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetKernelArgInfo(
        cl_kernel kernel,
        cl_uint arg_indx,
        cl_kernel_arg_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_GetKernelWorkGroupInfoSize(
        cl_kernel kernel,
        cl_device_id device,
        cl_kernel_work_group_info  param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetKernelWorkGroupInfo(
        cl_kernel kernel,
        cl_device_id device,
        cl_kernel_work_group_info  param_name,
        size_t param_value_size,
        void* param_value);

    /*
    CL_WRAPPER_API cl_int CL_GetKernelSubGroupInfoSize(
        cl_kernel kernel,
        cl_device_id device,
        cl_kernel_sub_group_info param_name,
        size_t input_value_size,
        const void* input_value,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetKernelSubGroupInfo(
        cl_kernel kernel,
        cl_device_id device,
        cl_kernel_sub_group_info param_name,
        size_t input_value_size,
        const void* input_value,
        size_t param_value_size,
        void* param_value);
        */

    CL_WRAPPER_API cl_int CL_RetainKernel(cl_kernel kernel);

    CL_WRAPPER_API cl_int CL_ReleaseKernel(cl_kernel kernel);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                BUFFERS FUNCTIONS                                           ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_mem CL_CreateBuffer(
        cl_context   context,
        cl_mem_flags flags,
        size_t       size,
        void* data,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_mem CL_CreateEmptyBuffer(
        cl_context   context,
        cl_mem_flags flags,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_mem CL_CreateSubBuffer(
        cl_mem  buffer,
        cl_mem_flags flags,
        cl_buffer_create_type buffer_create_type,
        const void* buffer_create_info,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_int CL_GetMemObjectInfoSize(
        cl_mem memobj,
        cl_mem_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetMemObjectInfo(
        cl_mem memobj,
        cl_mem_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_RetainMemObject(cl_mem mem);

    CL_WRAPPER_API cl_int CL_ReleaseMemObject(cl_mem mem);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                IMAGE FUNCTIONS                                            ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_mem CL_CreateImage(
        cl_context context,
        cl_mem_flags flags,
        cl_image_format image_format,
        cl_image_desc image_desc,
        void* host_ptr,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_int CL_GetSupportedImageFormatsSize(
        cl_context context,
        cl_mem_flags flags,
        cl_mem_object_type image_type,
        cl_uint num_entries,
        cl_uint* num_image_formats);

    CL_WRAPPER_API cl_int CL_GetSupportedImageFormats(
        cl_context context,
        cl_mem_flags flags,
        cl_mem_object_type image_type,
        cl_uint num_entries,
        cl_image_format* image_formats);

    CL_WRAPPER_API cl_int CL_GetImageInfoSize(
        cl_mem image,
        cl_image_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetImageInfo(
        cl_mem image,
        cl_image_info param_name,
        size_t param_value_size,
        void* param_value);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                SAMPLER FUNCTIONS                                          ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_sampler CL_CreateSamplerWithProperties(
        cl_context context,
        SamplerProperties sampler_properties,
        cl_int* errcode_ret);

    /*
    CL_WRAPPER_API cl_sampler CL_CreateSampler(
        cl_context context,
        cl_bool normalize_coords,
        cl_addressing_mode addressing_mode,
        cl_filter_mode filter_mode,
        cl_int* errcode_ret);
        */

    CL_WRAPPER_API cl_int CL_GetSamplerInfoSize(
        cl_sampler sampler,
        cl_sampler_info param_name,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetSamplerInfo(
        cl_sampler sampler,
        cl_sampler_info param_name,
        size_t param_value_size,
        void* param_value);

    CL_WRAPPER_API cl_int CL_RetainSampler(cl_sampler sampler);

    CL_WRAPPER_API cl_int CL_ReleaseSampler(cl_sampler sampler);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                Command FUNCTIONS                                          ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_command_queue CL_CreateCommandQueue(
        cl_context context,
        cl_device_id device,
        cl_int* error);

    CL_WRAPPER_API cl_command_queue CL_CreateCommandQueueWithProperties(
        cl_context context,
        cl_device_id device,
        CommandQueueProperties properties,
        cl_int* error);

    CL_WRAPPER_API cl_int CL_GetCommandQueueInfoSize(
        cl_command_queue command,
        cl_command_queue_info name,
        size_t* size);

    CL_WRAPPER_API cl_int CL_GetCommandQueueInfo(
        cl_command_queue command,
        cl_command_queue_info name,
        size_t size,
        void* info);

    CL_WRAPPER_API cl_int CL_RetainCommandQueue(cl_command_queue command);

    CL_WRAPPER_API cl_int CL_ReleaseCommandQueue(cl_command_queue command);

    CL_WRAPPER_API cl_int CL_Flush(cl_command_queue command);

    CL_WRAPPER_API cl_int CL_Finish(cl_command_queue command);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                EVENT FUNCTIONS                                            ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_int CL_WaitForEvents(
        cl_uint num_events,
        const cl_event* event_list);

    CL_WRAPPER_API cl_event CL_CreateUserEvent(
        cl_context context,
        cl_int* error);

    CL_WRAPPER_API cl_int CL_GetEventInfoSize(
        cl_event event,
        cl_event_info name,
        size_t* size);

    CL_WRAPPER_API cl_int CL_GetEventInfo(
        cl_event event,
        cl_event_info name,
        size_t size,
        void* value);

    CL_WRAPPER_API cl_int CL_GetEventProfilingInfoSize(
        cl_event event,
        cl_profiling_info name,
        size_t* size);

    CL_WRAPPER_API cl_int CL_GetEventProfilingInfo(
        cl_event event,
        cl_profiling_info name,
        size_t size,
        void* value);

    CL_WRAPPER_API cl_int CL_RetainEvent(cl_event event);

    CL_WRAPPER_API cl_int CL_ReleaseEvent(cl_event event);

    CL_WRAPPER_API cl_int CL_SetUserEventStatus(
        cl_event event,
        cl_int status);

    /////////////////////////////////////////////////////////////////////////////////////////////////
    //                                ENQUEUE FUNCTIONS                                          ///
    ///////////////////////////////////////////////////////////////////////////////////////////////

    CL_WRAPPER_API cl_int CL_EnqueueReadBuffer(
        cl_command_queue command_queue,
        cl_mem buffer,
        cl_bool blocking_read,
        size_t offset,
        size_t size,
        void* ptr,
        cl_uint num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueReadBufferRect(
        cl_command_queue command_queue,
        cl_mem buffer,
        cl_bool blocking_read,
        const size_t* buffer_origin,
        const size_t* host_origin,
        const size_t* region,
        size_t buffer_row_pitch,
        size_t buffer_slice_pitch,
        size_t host_row_pitch,
        size_t host_slice_pitch,
        void* ptr,
        cl_uint             num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueWriteBuffer(
        cl_command_queue command_queue,
        cl_mem buffer,
        cl_bool blocking_write,
        size_t offset,
        size_t size,
        const void* ptr,
        cl_uint num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueWriteBufferRect(
        cl_command_queue command_queue,
        cl_mem              buffer,
        cl_bool             blocking_write,
        const size_t* buffer_origin,
        const size_t* host_origin,
        const size_t* region,
        size_t              buffer_row_pitch,
        size_t              buffer_slice_pitch,
        size_t              host_row_pitch,
        size_t              host_slice_pitch,
        const void* ptr,
        cl_uint             num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueFillBuffer(
        cl_command_queue command_queue,
        cl_mem             buffer,
        const void* pattern,
        size_t             pattern_size,
        size_t             offset,
        size_t             size,
        cl_uint            num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueCopyBuffer(
        cl_command_queue command_queue,
        cl_mem              src_buffer,
        cl_mem              dst_buffer,
        size_t              src_offset,
        size_t              dst_offset,
        size_t              size,
        cl_uint             num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueCopyBufferRect(
        cl_command_queue command_queue,
        cl_mem              src_buffer,
        cl_mem              dst_buffer,
        const size_t* src_origin,
        const size_t* dst_origin,
        const size_t* region,
        size_t              src_row_pitch,
        size_t              src_slice_pitch,
        size_t              dst_row_pitch,
        size_t              dst_slice_pitch,
        cl_uint             num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueReadImage(
        cl_command_queue  command_queue,
        cl_mem image,
        cl_bool blocking_read,
        const size_t* origin,
        const size_t* region,
        size_t row_pitch,
        size_t slice_pitch,
        void* ptr,
        cl_uint num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueWriteImage(
        cl_command_queue command_queue,
        cl_mem              image,
        cl_bool             blocking_write,
        const size_t* origin,
        const size_t* region,
        size_t              input_row_pitch,
        size_t              input_slice_pitch,
        const void* ptr,
        cl_uint             num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueFillImage(
        cl_command_queue command_queue,
        cl_mem             image,
        const void* fill_color,
        const size_t* origin,
        const size_t* region,
        cl_uint            num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueCopyImage(
        cl_command_queue command_queue,
        cl_mem               src_image,
        cl_mem               dst_image,
        const size_t* src_origin,
        const size_t* dst_origin,
        const size_t* region,
        cl_uint              num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueCopyImageToBuffer(
        cl_command_queue command_queue,
        cl_mem           src_image,
        cl_mem           dst_buffer,
        const size_t* src_origin,
        const size_t* region,
        size_t           dst_offset,
        cl_uint          num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueCopyBufferToImage(
        cl_command_queue command_queue,
        cl_mem           src_buffer,
        cl_mem           dst_image,
        size_t           src_offset,
        const size_t* dst_origin,
        const size_t* region,
        cl_uint          num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API void* CL_EnqueueMapBuffer(
        cl_command_queue command_queue,
        cl_mem           buffer,
        cl_bool          blocking_map,
        cl_map_flags     map_flags,
        size_t           offset,
        size_t           size,
        cl_uint          num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event,
        cl_int* errcode_ret);

    CL_WRAPPER_API void* CL_EnqueueMapImage(
        cl_command_queue  command_queue,
        cl_mem            image,
        cl_bool           blocking_map,
        cl_map_flags      map_flags,
        const size_t* origin,
        const size_t* region,
        size_t* image_row_pitch,
        size_t* image_slice_pitch,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event,
        cl_int* errcode_ret);

    CL_WRAPPER_API cl_int CL_EnqueueUnmapMemObject(
        cl_command_queue command_queue,
        cl_mem           memobj,
        void* mapped_ptr,
        cl_uint          num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueMigrateMemObjects(
        cl_command_queue command_queue,
        cl_uint                num_mem_objects,
        const cl_mem* mem_objects,
        cl_mem_migration_flags flags,
        cl_uint                num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueNDRangeKernel(
        cl_command_queue command_queue,
        cl_kernel        kernel,
        cl_uint          work_dim,
        const size_t* global_work_offset,
        const size_t* global_work_size,
        const size_t* local_work_size,
        cl_uint          num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    /*
    CL_WRAPPER_API cl_int CL_EnqueueNativeKernel(
        cl_command_queue  command_queue,
        void (CL_CALLBACK* user_func)(void*),
        void* args,
        size_t            cb_args,
        cl_uint           num_mem_objects,
        const cl_mem* mem_list,
        const void** args_mem_loc,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);
        */

    CL_WRAPPER_API cl_int CL_EnqueueMarkerWithWaitList(
        cl_command_queue command_queue,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueBarrierWithWaitList(
        cl_command_queue command_queue,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    /*
    CL_WRAPPER_API cl_int CL_EnqueueSVMFree(
        cl_command_queue command_queue,
        cl_uint           num_svm_pointers,
        void* svm_pointers[],
        void (CL_CALLBACK* pfn_free_func)(cl_command_queue queue,
            cl_uint          num_svm_pointers,
            void* svm_pointers[],
            void* user_data),
        void* user_data,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);
        */

    CL_WRAPPER_API cl_int CL_EnqueueSVMMemcpy(
        cl_command_queue command_queue,
        cl_bool           blocking_copy,
        void* dst_ptr,
        const void* src_ptr,
        size_t            size,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueSVMMemFill(
        cl_command_queue command_queue,
        void* svm_ptr,
        const void* pattern,
        size_t            pattern_size,
        size_t            size,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueSVMMap(
        cl_command_queue command_queue,
        cl_bool           blocking_map,
        cl_map_flags      flags,
        void* svm_ptr,
        size_t            size,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueSVMUnmap(
        cl_command_queue  command_queue,
        void* svm_ptr,
        cl_uint           num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

    CL_WRAPPER_API cl_int CL_EnqueueSVMMigrateMem(
        cl_command_queue  command_queue,
        cl_uint                  num_svm_pointers,
        const void** svm_pointers,
        const size_t* sizes,
        cl_mem_migration_flags   flags,
        cl_uint                  num_events_in_wait_list,
        const cl_event* event_wait_list,
        cl_event* event);

}