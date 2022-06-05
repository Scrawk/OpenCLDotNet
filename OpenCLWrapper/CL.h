#pragma once

#include "OpenCLWrapper.h"
#include <CL/cl.h>

extern "C"
{

    CL_WRAPPER_API int CL_VersionNumber();

    CL_WRAPPER_API cl_int CL_GetPlatformCount(
        cl_uint* num_platforms);

    CL_WRAPPER_API cl_int CL_GetPlatformIDs(
        cl_uint num_entries,
        cl_platform_id* platforms);

    CL_WRAPPER_API cl_int CL_GetPlatformInfo(
        cl_platform_id   platform,
        cl_platform_info param_name,
        size_t           param_value_size,
        void* param_value,
        size_t* param_value_size_ret);

    CL_WRAPPER_API cl_int CL_GetDeviceIDs(
        cl_platform_id   platform,
        cl_device_type   device_type,
        cl_uint          num_entries,
        cl_device_id* devices,
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

}