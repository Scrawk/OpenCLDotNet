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
    ContextProperties properties,
    cl_uint num_devices,
    const cl_device_id* devices)
{
    cl_context_properties props[] =
    {
      CL_CONTEXT_PLATFORM,
      properties.platform,
      //CL_CONTEXT_INTEROP_USER_SYNC,
      //properties.interopUserSync,
      0
    };

    return clCreateContext(props, num_devices, devices, nullptr, nullptr, nullptr);
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
    cl_int* error)
{
    return clCreateProgramWithSource(context, count, &strings, &length, error);
}

 cl_program CL_CreateProgramWithBinary(
    cl_context context,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const size_t* lengths,
    const unsigned char* binaries,
    cl_int* binary_status,
    cl_int* error)
{
    unsigned char** _binaries = new unsigned char*[num_devices];

    int index = 0;
    for (cl_uint i = 0; i < num_devices; i++)
    {
        _binaries[i] = new unsigned char[lengths[i]];

        for (int j = 0; j < lengths[i]; j++)
            _binaries[i][j] = binaries[index++];
    }

    auto const_bin = (const unsigned char**)_binaries;
        
    auto id = clCreateProgramWithBinary(context, num_devices, device_list, lengths, 
        const_bin, binary_status, error);

    for (cl_uint i = 0; i < num_devices; i++)
        delete[] _binaries[i];

    delete[] _binaries;

    return id;
}

 cl_program CL_CreateProgramWithBuiltInKernels(
     cl_context context,
     cl_uint num_devices,
     const cl_device_id* device_list,
     const char* kernel_names,
     cl_int* error)
 {
     return clCreateProgramWithBuiltInKernels(context, num_devices, device_list, kernel_names, error);
}

cl_program CL_CreateProgramWithIL(
     cl_context context,
     const void* il,
     size_t length,
     cl_int* error)
 {
    return clCreateProgramWithIL(context, il, length, error);
 }

cl_int CL_BuildProgram(
    cl_program program,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const char* options)
{
    return clBuildProgram(program, num_devices, device_list, options, nullptr, nullptr);
}

cl_int CL_CompileProgram(
    cl_program program,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const char* options,
    cl_uint num_input_headers,
    const cl_program* input_headers,
    const char** header_include_names)
{
    return clCompileProgram(program, num_devices, device_list, options, 
        num_input_headers, input_headers, header_include_names, 
        nullptr, nullptr);
}

cl_program CL_LinkProgram(
    cl_context context,
    cl_uint num_devices,
    const cl_device_id* device_list,
    const char* options,
    cl_uint num_input_programs,
    const cl_program* input_programs,
    cl_int* error)
{
    return clLinkProgram(context, num_devices, device_list, options,
        num_input_programs, input_programs, nullptr, nullptr, error);
}

 cl_int CL_GetProgramBuildInfoSize(
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

 cl_int CL_GetProgramInfoSize(
    cl_program         program,
    cl_program_info    param_name,
    size_t* param_value_size_ret)
{
    return clGetProgramInfo(program, param_name, 0, nullptr, param_value_size_ret);
}

 cl_int CL_GetProgramInfo(
    cl_program         program,
    cl_program_info    param_name,
    size_t             param_value_size,
    void* param_value)
{
    return clGetProgramInfo(program, param_name, param_value_size, param_value, nullptr);
}

 cl_int CL_GetProgramBinaries(
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
    cl_int* error)
{
    return clCreateKernel(program, kernel_name, error);
}

cl_kernel CL_CloneKernel(
    cl_kernel source_kernel,
    cl_int* error)
{
    return clCloneKernel(source_kernel, error);
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

cl_int CL_SetKernelArgDouble(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_double arg_value)
{
    size_t size = sizeof(cl_float);
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

cl_int CL_SetKernelArgLong(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_long arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgULong(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_ulong arg_value)
{
    size_t size = sizeof(cl_int);
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

cl_int CL_SetKernelArgUInt(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_uint arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgShort(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_short arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgUShort(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_ushort arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgSByte(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_char arg_value)
{
    size_t size = sizeof(cl_int);
    cl_int err = clSetKernelArg(kernel, arg_index, size, &arg_value);
    return err;
}

cl_int CL_SetKernelArgByte(
    cl_kernel kernel,
    cl_uint arg_index,
    cl_uchar arg_value)
{
    size_t size = sizeof(cl_int);
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

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                BUFFERS FUNCTIONS                                          ///
///////////////////////////////////////////////////////////////////////////////////////////////


cl_mem CL_CreateEmptyBuffer(
    cl_context   context,
    cl_mem_flags flags,
    cl_int* error)
{
    return clCreateBuffer(context, flags, 0, nullptr, error);
}

 cl_mem CL_CreateBuffer(
    cl_context context,
    cl_mem_flags flags,
    size_t size,
    void* data,
    cl_int* error)
{
    return clCreateBuffer(context, flags, size, data, error);
}

 cl_mem CL_CreateSubBuffer(
    cl_mem  buffer,
    cl_mem_flags flags,
    cl_buffer_create_type buffer_create_type,
    const void* buffer_create_info,
    cl_int* error)
{
    return clCreateSubBuffer(buffer, flags, buffer_create_type, buffer_create_info, error);
}

 cl_int CL_GetMemObjectInfoSize(
    cl_mem memobj,
    cl_mem_info param_name,
    size_t* param_value_size_ret)
{
    return clGetMemObjectInfo(memobj, param_name, 0, nullptr, param_value_size_ret);
}

 cl_int CL_GetMemObjectInfo(
    cl_mem memobj,
    cl_mem_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetMemObjectInfo(memobj, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_RetainMemObject(cl_mem mem)
{
    return clRetainMemObject(mem);
}

cl_int CL_ReleaseMemObject(cl_mem mem)
{
    return clReleaseMemObject(mem);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                IMAGE FUNCTIONS                                            ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_mem CL_CreateImage(
    cl_context context,
    cl_mem_flags flags,
    cl_image_format format,
    cl_image_desc desc,
    void* host_ptr,
    cl_int* error)
{
    return clCreateImage(context, flags, &format, 
                        &desc, host_ptr, error);
}

cl_int CL_GetSupportedImageFormatsSize(
    cl_context context,
    cl_mem_flags flags,
    cl_mem_object_type image_type,
    cl_uint num_entries,
    cl_uint* num_image_formats)
{
    return clGetSupportedImageFormats(context, flags, image_type,
        num_entries, nullptr, num_image_formats);
}

cl_int CL_GetSupportedImageFormats(
    cl_context context,
    cl_mem_flags flags,
    cl_mem_object_type image_type,
    cl_uint num_entries,
    cl_image_format* image_formats)
{
    return clGetSupportedImageFormats(context, flags, image_type, 
        num_entries, image_formats, nullptr);
}

cl_int CL_GetImageInfoSize(
    cl_mem image,
    cl_image_info param_name,
    size_t* param_value_size_ret)
{
    return clGetImageInfo(image, param_name, 0, nullptr, 
        param_value_size_ret);
}

cl_int CL_GetImageInfo(
    cl_mem image,
    cl_image_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetImageInfo(image, param_name, param_value_size, param_value,
        nullptr);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
///                                SAMPLER FUNCTIONS                                         ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_sampler CL_CreateSamplerWithProperties(
    cl_context context,
    SamplerProperties properties,
    cl_int* error)
{

    cl_sampler_properties props[] = 
    {
      CL_SAMPLER_NORMALIZED_COORDS,
      properties.normalizedCoords,
      CL_SAMPLER_ADDRESSING_MODE, 
      properties.addressingMode,
      CL_SAMPLER_FILTER_MODE, 
      properties.filterMode,
      //CL_SAMPLER_MIP_FILTER_MODE,
      //properties.mipFilterMode,
      //CL_SAMPLER_LOD_MIN,
      //properties.LODMin,
      //CL_SAMPLER_LOD_MAX,
      //properties.LODMax,
      0
    };

    return clCreateSamplerWithProperties(context, props, error);
}

/*
cl_sampler CL_CreateSampler(
    cl_context context,
    cl_bool normalize_coords,
    cl_addressing_mode addressing_mode,
    cl_filter_mode filter_mode,
    cl_int* error)
{
    return clCreateSampler(context, normalize_coords, addressing_mode, filter_mode, error);
}
*/

cl_int CL_GetSamplerInfoSize(
    cl_sampler sampler,
    cl_sampler_info param_name,
    size_t* param_value_size_ret)
{
    return clGetSamplerInfo(sampler, param_name, 0, nullptr, param_value_size_ret);
}

cl_int CL_GetSamplerInfo(
    cl_sampler sampler,
    cl_sampler_info param_name,
    size_t param_value_size,
    void* param_value)
{
    return clGetSamplerInfo(sampler, param_name, param_value_size, param_value, nullptr);
}

cl_int CL_RetainSampler(cl_sampler sampler)
{
    return clRetainSampler(sampler);
}

cl_int CL_ReleaseSampler(cl_sampler sampler)
{
    return clReleaseSampler(sampler);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                Command FUNCTIONS                                          ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_command_queue CL_CreateCommandQueue(
    cl_context context,
    cl_device_id device,
    cl_int* error)
{
    return clCreateCommandQueueWithProperties(context, device, 0, error);
}

cl_command_queue CL_CreateCommandQueueWithProperties(
    cl_context context,
    cl_device_id device,
    CommandQueueProperties properties,
    cl_int* error)
{
    cl_queue_properties props[] =
    {
        CL_QUEUE_PROPERTIES,
        properties.properties,
        CL_QUEUE_SIZE,
        properties.queueSize,
        0
    };

    return clCreateCommandQueueWithProperties(context, device, props, error);
}

cl_int CL_GetCommandQueueInfoSize(
    cl_command_queue command,
    cl_command_queue_info name,
    size_t* size)
{
    return clGetCommandQueueInfo(command, name, 0, nullptr, size);
}

cl_int CL_GetCommandQueueInfo(
    cl_command_queue command,
    cl_command_queue_info name,
    size_t size,
    void* info)
{
    return clGetCommandQueueInfo(command, name, size, info, nullptr);
}

cl_int CL_RetainCommandQueue(cl_command_queue command)
{
    return clRetainCommandQueue(command);
}

cl_int CL_ReleaseCommandQueue(cl_command_queue command)
{
    return clReleaseCommandQueue(command);
}

cl_int CL_Flush(cl_command_queue command)
{
    return clFlush(command);
}

cl_int CL_Finish(cl_command_queue command)
{
    return clFinish(command);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                Event FUNCTIONS                                            ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_int CL_WaitForEvents(
    cl_uint num_events,
    const cl_event* event_list)
{
    return clWaitForEvents(num_events, event_list);
}

cl_event CL_CreateUserEvent(
    cl_context context,
    cl_int* error)
{
    return clCreateUserEvent(context, error);
}

cl_int CL_GetEventInfoSize(
    cl_event event,
    cl_event_info name,
    size_t* size)
{
    return clGetEventInfo(event, name, 0, nullptr, size);
}

cl_int CL_GetEventInfo(
    cl_event event,
    cl_event_info name,
    size_t size,
    void* value)
{
    return clGetEventInfo(event, name, size, value, nullptr);
}

cl_int CL_GetEventProfilingInfoSize(
    cl_event event,
    cl_profiling_info name,
    size_t* size)
{
    return clGetEventProfilingInfo(event, name, 0, nullptr, size);
}

cl_int CL_GetEventProfilingInfo(
    cl_event event,
    cl_profiling_info name,
    size_t size,
    void* value)
{
    return clGetEventProfilingInfo(event, name, size, value, nullptr);
}

cl_int CL_RetainEvent(cl_event event)
{
    return clRetainEvent(event);
}

cl_int CL_ReleaseEvent(cl_event event)
{
    return clReleaseEvent(event);
}

cl_int CL_SetUserEventStatus(
    cl_event event,
    cl_int status)
{
    return clSetUserEventStatus(event, status);
}

/////////////////////////////////////////////////////////////////////////////////////////////////
//                                ENQUEUE FUNCTIONS                                          ///
///////////////////////////////////////////////////////////////////////////////////////////////

cl_int CL_EnqueueReadBuffer(
    cl_command_queue command,
    cl_mem buffer,
    cl_bool blocking_read,
    size_t offset,
    size_t size,
    void* ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueReadBuffer(command, buffer, blocking_read, offset, size, ptr, 
                                wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueReadBufferRect(
    cl_command_queue command,
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
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueReadBufferRect(command, buffer, blocking_read, buffer_origin, host_origin, region,
        buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
        ptr, wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueWriteBuffer(
    cl_command_queue command,
    cl_mem buffer,
    cl_bool blocking_write,
    size_t offset,
    size_t size,
    const void* ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueWriteBuffer(command, buffer, blocking_write, offset, size, ptr,
        wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueWriteBufferRect(
    cl_command_queue command,
    cl_mem buffer,
    cl_bool blocking_write,
    const size_t* buffer_origin,
    const size_t* host_origin,
    const size_t* region,
    size_t buffer_row_pitch,
    size_t buffer_slice_pitch,
    size_t host_row_pitch,
    size_t  host_slice_pitch,
    const void* ptr,
    cl_uint  wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueWriteBufferRect(command, buffer, blocking_write, buffer_origin, host_origin, region,
        buffer_row_pitch, buffer_slice_pitch, host_row_pitch, host_slice_pitch,
        ptr, wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueFillBuffer(
    cl_command_queue command,
    cl_mem buffer,
    const void* pattern,
    size_t pattern_size,
    size_t offset,
    size_t size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueFillBuffer(command, buffer, pattern, pattern_size, offset, size, 
        wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueCopyBuffer(
    cl_command_queue command,
    cl_mem src_buffer,
    cl_mem dst_buffer,
    size_t src_offset,
    size_t dst_offset,
    size_t size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueCopyBuffer(command, src_buffer, dst_buffer, src_offset, dst_offset, size,
        wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueCopyBufferRect(
    cl_command_queue command,
    cl_mem src_buffer,
    cl_mem dst_buffer,
    const size_t* src_origin,
    const size_t* dst_origin,
    const size_t* region,
    size_t src_row_pitch,
    size_t src_slice_pitch,
    size_t dst_row_pitch,
    size_t dst_slice_pitch,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueCopyBufferRect(command, src_buffer, dst_buffer, src_origin, dst_origin, region, 
        src_row_pitch, src_slice_pitch, dst_row_pitch, dst_slice_pitch, 
        wait_list_size, wait_list, event);
 }

cl_int CL_EnqueueReadImage(
    cl_command_queue command,
    cl_mem image,
    cl_bool blocking_read,
    const size_t* origin,
    const size_t* region,
    size_t row_pitch,
    size_t slice_pitch,
    void* ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    std::cout << "ReadImage " << std::endl;

    auto error = clEnqueueReadImage(command, image, blocking_read, origin, region, 
        row_pitch, slice_pitch, ptr, wait_list_size, wait_list, event);

    auto buffer = (cl_float*)ptr;

    for (int i = 0; i < 10; i++)
    {
        auto c = buffer[i];
        std::cout << c << std::endl;
    }

    return error;
}

cl_int CL_EnqueueWriteImage(
    cl_command_queue command,
    cl_mem  image,
    cl_bool blocking_write,
    const size_t* origin,
    const size_t* region,
    size_t input_row_pitch,
    size_t  input_slice_pitch,
    const void* ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    std::cout << "WriteImage " << std::endl;

    auto buffer = (cl_float*)ptr;

    for (int i = 0; i < 10; i++)
    {
        auto c = buffer[i];
        std::cout << c << std::endl;
    }

    return clEnqueueWriteImage(command, image, blocking_write, origin, region, 
        input_row_pitch, input_slice_pitch, ptr,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueFillImage(
    cl_command_queue command,
    cl_mem image,
    const void* fill_color,
    const size_t* origin,
    const size_t* region,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueFillImage(command, image, fill_color, origin, region, 
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueCopyImage(
    cl_command_queue command,
    cl_mem src_image,
    cl_mem dst_image,
    const size_t* src_origin,
    const size_t* dst_origin,
    const size_t* region,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueCopyImage(command, src_image, dst_image, src_origin, dst_origin, region,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueCopyImageToBuffer(
    cl_command_queue command,
    cl_mem src_image,
    cl_mem dst_buffer,
    const size_t* src_origin,
    const size_t* region,
    size_t dst_offset,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueCopyImageToBuffer(command, src_image, dst_buffer, src_origin, region, dst_offset,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueCopyBufferToImage(
    cl_command_queue command,
    cl_mem src_buffer,
    cl_mem dst_image,
    size_t src_offset,
    const size_t* dst_origin,
    const size_t* region,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueCopyBufferToImage(command, src_buffer, dst_image, src_offset, dst_origin, region,
        wait_list_size, wait_list, event);
  }

void* CL_EnqueueMapBuffer(
    cl_command_queue command,
    cl_mem buffer,
    cl_bool blocking_map,
    cl_map_flags map_flags,
    size_t offset,
    size_t size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event,
    cl_int* error)
{
    return clEnqueueMapBuffer(command, buffer, blocking_map, map_flags, offset, size,
        wait_list_size, wait_list, event, error);
  }

void* CL_EnqueueMapImage(
    cl_command_queue  command,
    cl_mem image,
    cl_bool blocking_map,
    cl_map_flags map_flags,
    const size_t* origin,
    const size_t* region,
    size_t* image_row_pitch,
    size_t* image_slice_pitch,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event,
    cl_int* error)
{
    return clEnqueueMapImage(command, image, blocking_map, map_flags, origin, 
        region, image_row_pitch, image_slice_pitch,
        wait_list_size, wait_list, event, error);
  }

cl_int CL_EnqueueUnmapMemObject(
    cl_command_queue command,
    cl_mem memobj,
    void* mapped_ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueUnmapMemObject(command, memobj, mapped_ptr, 
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueMigrateMemObjects(
    cl_command_queue command,
    cl_uint num_mem_objects,
    const cl_mem* mem_objects,
    cl_mem_migration_flags flags,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueMigrateMemObjects(command, num_mem_objects, mem_objects, flags,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueNDRangeKernel(
    cl_command_queue command,
    cl_kernel kernel,
    cl_uint work_dim,
    const size_t* global_work_offset,
    const size_t* global_work_size,
    const size_t* local_work_size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueNDRangeKernel(command, kernel, work_dim, 
        global_work_offset, global_work_size, local_work_size,
        wait_list_size, wait_list, event);
  }

/*
cl_int CL_EnqueueNativeKernel(
    cl_command_queue  command,
    void (CL_CALLBACK* user_func)(void*),
    void* args,
    size_t cb_args,
    cl_uint num_mem_objects,
    const cl_mem* mem_list,
    const void** args_mem_loc,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueNativeKernel(wait_list_size, wait_list, event);
  }
  */

cl_int CL_EnqueueMarkerWithWaitList(
    cl_command_queue command,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueMarkerWithWaitList(command, wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueBarrierWithWaitList(
    cl_command_queue command,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueBarrierWithWaitList(command, wait_list_size, wait_list, event);
  }

/*
cl_int CL_EnqueueSVMFree(
    cl_command_queue command,
    cl_uint           num_svm_pointers,
    void* svm_pointers[],
    void (CL_CALLBACK* pfn_free_func)(cl_command_queue queue,
        cl_uint          num_svm_pointers,
        void* svm_pointers[],
        void* user_data),
    void* user_data,
    cl_uint           wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMFree(wait_list_size, wait_list, event);
  }
  */

cl_int CL_EnqueueSVMMemcpy(
    cl_command_queue command,
    cl_bool blocking_copy,
    void* dst_ptr,
    const void* src_ptr,
    size_t size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMMemcpy(command, blocking_copy, dst_ptr, src_ptr, size,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueSVMMemFill(
    cl_command_queue command,
    void* svm_ptr,
    const void* pattern,
    size_t pattern_size,
    size_t   size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMMemFill(command, svm_ptr, pattern, pattern_size, size,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueSVMMap(
    cl_command_queue command,
    cl_bool blocking_map,
    cl_map_flags flags,
    void* svm_ptr,
    size_t size,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMMap(command, blocking_map, flags, svm_ptr, size,
        wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueSVMUnmap(
    cl_command_queue command,
    void* svm_ptr,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMUnmap(command, svm_ptr, wait_list_size, wait_list, event);
  }

cl_int CL_EnqueueSVMMigrateMem(
    cl_command_queue command,
    cl_uint num_svm_pointers,
    const void** svm_pointers,
    const size_t* sizes,
    cl_mem_migration_flags flags,
    cl_uint wait_list_size,
    const cl_event* wait_list,
    cl_event* event)
{
    return clEnqueueSVMMigrateMem(command, num_svm_pointers, svm_pointers, sizes, flags, 
        wait_list_size, wait_list, event);
  }
