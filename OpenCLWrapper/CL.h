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

}