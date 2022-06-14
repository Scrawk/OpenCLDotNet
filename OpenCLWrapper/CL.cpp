#include "CL.h"

#include <iostream> 

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

cl_int CL_UnloadPlatformCompiler(cl_platform_id platform)
{
    return clUnloadPlatformCompiler(platform);
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

CL_WRAPPER_API cl_program CL_CreateProgramWithBinary(
    cl_context context,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const size_t* lengths,
    const unsigned char* binaries,
    cl_int* binary_status,
    cl_int* errcode_ret)
{
    unsigned char** _binaries = new unsigned char*[num_devices];

    int index = 0;
    for (auto i = 0; i < num_devices; i++)
    {
        _binaries[i] = new unsigned char[lengths[i]];

        for (int j = 0; j < lengths[i]; j++)
            _binaries[i][j] = binaries[index++];
    }

    auto const_bin = (const unsigned char**)_binaries;
        
    auto id = clCreateProgramWithBinary(context, num_devices, device_list, lengths, 
        const_bin, binary_status, errcode_ret);

    for (cl_uint i = 0; i < num_devices; i++)
        delete[] _binaries[i];

    delete[] _binaries;

    return id;
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

CL_WRAPPER_API cl_int CL_GetProgramBinaries(
    cl_program         program,
    int num_devices,
    size_t* sizes,
    unsigned char* binaries)
{
   
    unsigned char** _binaries = new unsigned char* [num_devices];
    for (int i = 0; i < num_devices; i++)
    {
        _binaries[i] = new unsigned char[sizes[i]];
    }

    cl_int err = clGetProgramInfo(program, CL_PROGRAM_BINARIES, 
        sizeof(unsigned char*) * num_devices, _binaries, NULL);

    if (err != CL_SUCCESS)
    {
        for (int i = 0; i < num_devices; i++)
            delete[] _binaries[i];

        delete[] _binaries;
        return err;
    }

    int index = 0;
    for (int i = 0; i < num_devices; i++)
    {
        for (int j = 0; j < sizes[i]; j++)
        {
            auto c = _binaries[i][j];
            binaries[index++] = c;
        }
    }

    for (int i = 0; i < num_devices; i++)
        delete[] _binaries[i];

    delete[] _binaries;

    return CL_SUCCESS;

}

cl_int CL_RetainProgram(cl_program program)
{
    return clRetainProgram(program);
}

cl_int CL_ReleaseProgram(cl_program program)
{
    return clReleaseProgram(program);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                KERNEL FUNCTIONS                                           ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_kernel CL_CreateKernel(
    cl_program program,
    const char* kernel_name,
    cl_int* errcode_ret)
{
    return clCreateKernel(program, kernel_name, errcode_ret);
}

cl_int CL_SetKernelArgMem(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_mem arg_value)
{
    size_t size = sizeof(cl_mem);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgSampler(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_sampler arg_value)
{
    size_t size = sizeof(cl_sampler);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgInt(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_int arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgFloat(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_float arg_value)
{
    size_t size = sizeof(cl_float);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_GetKernelInfoSize(
    cl_kernel kernel,
    cl_kernel_info param_name,
    size_t* param_value_size_ret)
{
    return clGetKernelInfo(kernel, param_name, 0, nullptr, param_value_size_ret);
}

cl_int CL_GetKernelInfo(
    cl_kernel kernel,
    cl_kernel_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetKernelInfo(kernel, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_GetKernelArgInfoSize(
    cl_kernel kernel,
    cl_uint arg_indx,
    cl_kernel_arg_info param_name,
    size_t* param_value_size_ret)
{
    return clGetKernelArgInfo(kernel, arg_indx, param_name, 0, nullptr, param_value_size_ret);
}

cl_int CL_GetKernelArgInfo(
    cl_kernel kernel,
    cl_uint arg_indx,
    cl_kernel_arg_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetKernelArgInfo(kernel, arg_indx, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_GetKernelWorkGroupInfoSize(
    cl_kernel kernel,
    cl_device_id device,
    cl_kernel_work_group_info  param_name,
    size_t* param_value_size_ret)
{
    return clGetKernelWorkGroupInfo(kernel, device, param_name, 
        0, nullptr, param_value_size_ret);
}

cl_int CL_GetKernelWorkGroupInfo(
    cl_kernel kernel,
    cl_device_id device,
    cl_kernel_work_group_info  param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetKernelWorkGroupInfo(kernel, device, param_name,
        param_value_size, param_value, nullptr);
}

/*
cl_int CL_GetKernelSubGroupInfoSize(
    cl_kernel kernel,
    cl_device_id device,
    cl_kernel_sub_group_info param_name,
    size_t input_value_size,
    const void* input_value,
    size_t* param_value_size_ret)
{
    return clGetKernelSubGroupInfo(kernel, device, param_name, input_value_size, input_value,
        0, nullptr, param_value_size_ret);
}

cl_int CL_GetKernelSubGroupInfo(
    cl_kernel kernel,
    cl_device_id device,
    cl_kernel_sub_group_info param_name,
    size_t input_value_size,
    const void* input_value,
    size_t param_value_size,
    void* param_value)
{
    return clGetKernelSubGroupInfo(kernel, device, param_name, input_value_size, input_value,
        param_value_size, param_value, nullptr);
}
*/

cl_int CL_RetainKernel(cl_kernel kernel)
{
    return clRetainKernel(kernel);
}

cl_int CL_ReleaseKernel(cl_kernel kernel)
{
    return clReleaseKernel(kernel);
}