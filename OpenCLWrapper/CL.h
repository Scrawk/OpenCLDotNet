#pragma once

#include "OpenCLWrapper.h"
#include <CL/cl.h>

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
        const cl_context_properties* properties,
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

    CL_WRAPPER_API cl_int CL_BuildProgram(
        cl_program program,
        cl_uint num_devices,
        const cl_device_id* device_list,
        const char* options);

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

    CL_WRAPPER_API cl_int CL_SetKernelArgMem(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_mem arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgSampler(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_sampler arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgInt(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_int arg_value);

    CL_WRAPPER_API cl_int CL_SetKernelArgFloat(
        cl_kernel kernel,
        cl_uint arg_index,
        cl_float arg_value);

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
        const cl_sampler_properties* sampler_properties,
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



}