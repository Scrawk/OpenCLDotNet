#include "CL.h"

int CL_VersionNumber()
{
	return CL_TARGET_OPENCL_VERSION;
}

cl_int CL_GetPlatformCount(
    cl_uint* num_platforms)
{
    return clGetPlatformIDs(0, nullptr, num_platforms);
}

cl_int CL_GetPlatformIDs(
    cl_uint num_entries,
    cl_platform_id* platforms)
{
    return clGetPlatformIDs(num_entries, platforms, nullptr);
}

cl_int CL_GetPlatformInfoSize(
    cl_platform_id   platform,
    cl_platform_info param_name,
    size_t* param_value_size_ret)
{
    return clGetPlatformInfo(platform, param_name, 0, 
        nullptr, param_value_size_ret);
}

cl_int CL_GetPlatformInfo(
    cl_platform_id   platform,
    cl_platform_info param_name,
    size_t           param_value_size,
    void* param_value)
{
    return clGetPlatformInfo(platform, param_name, param_value_size,
        param_value, nullptr);
}

cl_int CL_GetDeviceIDs(
    cl_platform_id   platform,
    cl_device_type   device_type,
    cl_uint          num_entries,
    cl_device_id* devices,
    cl_uint* num_devices)
{
    return clGetDeviceIDs(platform, device_type, num_entries, 
        devices, num_devices);
}

cl_int CL_GetDeviceInfoSize(
    cl_device_id    device,
    cl_device_info  param_name,
    size_t* param_value_size_ret)
{
    return clGetDeviceInfo(device, param_name, 0,
        nullptr, param_value_size_ret);
}

cl_int CL_GetDeviceInfo(
    cl_device_id    device,
    cl_device_info  param_name,
    size_t          param_value_size,
    void* param_value)
{
    return clGetDeviceInfo(device, param_name, param_value_size,
        param_value, nullptr);
}