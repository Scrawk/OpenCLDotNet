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