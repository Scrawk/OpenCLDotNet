#include "CL.h"

int CL_VersionNumber()
{
	return CL_TARGET_OPENCL_VERSION;
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                PLATFORM FUNCTIONS                                         ///
///////////////////////////////////////////////////////////////////////////////////////////////

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

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                DEVICE FUNCTIONS                                           ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_int CL_GetDeviceIDs(
    cl_platform_id   platform,
    cl_device_type   device_type,
    cl_uint          num_entries,
    cl_device_id* devices)
{
    return clGetDeviceIDs(platform, device_type, num_entries, 
        devices, nullptr);
}

cl_int CL_GetDeviceCount(
    cl_platform_id   platform,
    cl_device_type   device_type,
    cl_uint* num_devices)
{
    return clGetDeviceIDs(platform, device_type, 0,
        nullptr, num_devices);
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

cl_int CL_RetainDevice(cl_device_id device)
{
    return clRetainDevice(device);
}

cl_int CL_ReleaseDevice(cl_device_id device)
{
    return clReleaseDevice(device);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                CONTEXT FUNCTIONS                                          ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_context CL_CreateContext(
    const cl_context_properties* properties,
    cl_uint num_devices,
    const cl_device_id* devices)
{
    return clCreateContext(properties, num_devices, devices, nullptr, nullptr, nullptr);
}

cl_int CL_GetContextInfoSize(
    cl_context context,
    cl_context_info param_name,
    size_t* param_value_size_ret)
{
    return clGetContextInfo(context, param_name, 0, nullptr, param_value_size_ret);
}

cl_int CL_GetContextInfo(
    cl_context context,
    cl_context_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetContextInfo(context, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_RetainContext(cl_context context)
{
    return clRetainContext(context);
}

cl_int CL_ReleaseContext(cl_context context)
{
    return clReleaseContext(context);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                PROGRAM FUNCTIONS                                          ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_program CL_CreateProgramWithSource(
    cl_context context,
    cl_uint count,
    const char* strings,
    size_t length,
    cl_int* errcode_ret)
{
    return clCreateProgramWithSource(context, count, &strings, &length, errcode_ret);
}

cl_int CL_BuildProgram(
    cl_program program,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const char* options)
{
    return clBuildProgram(program, num_devices, device_list, options, nullptr, nullptr);
}

CL_WRAPPER_API cl_int CL_GetProgramBuildInfoSize(
    cl_program program,
    cl_device_id device,
    cl_program_build_info param_name,
    size_t* param_value_size_ret)
{
    return clGetProgramBuildInfo(program, device, param_name, 0,
        nullptr, param_value_size_ret);
}

cl_int CL_GetProgramBuildInfo(
    cl_program program,
    cl_device_id device,
    cl_program_build_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetProgramBuildInfo(program, device, param_name, param_value_size, 
                                 param_value, nullptr);
}

CL_WRAPPER_API cl_int CL_GetProgramInfoSize(
    cl_program         program,
    cl_program_info    param_name,
    size_t* param_value_size_ret)
{
    return clGetProgramInfo(program, param_name, 0, nullptr, param_value_size_ret);
}

CL_WRAPPER_API cl_int CL_GetProgramInfo(
    cl_program         program,
    cl_program_info    param_name,
    size_t             param_value_size,
    void* param_value)
{
    return clGetProgramInfo(program, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_RetainProgram(cl_program program)
{
    return clRetainProgram(program);
}

cl_int CL_ReleaseProgram(cl_program program)
{
    return clReleaseProgram(program);
}