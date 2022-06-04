#include "CL.h"

int CL_VersionNumber()
{
	return CL_TARGET_OPENCL_VERSION;
}

cl_int CL_GetPlatformIDs(
    cl_uint num_entries,
    cl_platform_id* platforms,
    cl_uint* num_platforms)
{
    return clGetPlatformIDs(num_entries, platforms, num_platforms);
}

cl_int CL_GetPlatformInfo(
    cl_platform_id   platform,
    cl_platform_info param_name,
    size_t           param_value_size,
    void* param_value,
    size_t* param_value_size_ret)
{
    return clGetPlatformInfo(platform, param_name, param_value_size, param_value, param_value_size_ret);
}