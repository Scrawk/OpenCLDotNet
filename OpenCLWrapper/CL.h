#pragma once

#include "OpenCLWrapper.h"
#include <CL/cl.h>

extern "C"
{

	CL_WRAPPER_API int CL_VersionNumber();

    CL_WRAPPER_API cl_int CL_GetPlatformIDs(
        cl_uint num_entries,
        cl_platform_id* platforms,
        cl_uint* num_platforms);

    CL_WRAPPER_API cl_int CL_GetPlatformInfo(
        cl_platform_id   platform,
        cl_platform_info param_name,
        size_t           param_value_size,
        void* param_value,
        size_t* param_value_size_ret);

}