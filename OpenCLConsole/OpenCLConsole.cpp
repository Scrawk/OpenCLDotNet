#pragma once

//#define CL_TARGET_OPENCL_VERSION 100
//#define CL_TARGET_OPENCL_VERSION 110
//#define CL_TARGET_OPENCL_VERSION 120
//#define CL_TARGET_OPENCL_VERSION 200
//#define CL_TARGET_OPENCL_VERSION 210
#define CL_TARGET_OPENCL_VERSION 220
//#define CL_TARGET_OPENCL_VERSION 300


#include "OpenCL_Programming_Guide/Chapter_8/ImageFilter2D.h"

int main()
{
	//OpenCL_Programming_Guide::Chapter_3::Convolution();

	OpenCL_Programming_Guide::Chapter_8::Run();
}
