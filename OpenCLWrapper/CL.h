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

    CL_WRAPPER_API cl_int CL_RetainProgram(cl_program program);

    CL_WRAPPER_API cl_int CL_ReleaseProgram(cl_program program);

}