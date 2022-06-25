//
// Book:      OpenCL(R) Programming Guide
// Authors:   Aaftab Munshi, Benedict Gaster, Timothy Mattson, James Fung, Dan Ginsburg
// ISBN-10:   0-321-74964-2
// ISBN-13:   978-0-321-74964-2
// Publisher: Addison-Wesley Professional
// URLs:      http://safari.informit.com/9780132488006/
//            http://www.openclprogrammingguide.com
//

// ImageFilter2D.cpp
//
//    This example demonstrates performing gaussian filtering on a 2D image using
//    OpenCL
//
//    Requires FreeImage library for image I/O:
//      http://freeimage.sourceforge.net/

#include <iostream>
#include <fstream>
#include <sstream>
#include <string.h>

#ifdef __APPLE__
#include <OpenCL/cl.h>
#else
#include <CL/cl.h>
#endif

//#include "FreeImage.h"

namespace OpenCL_Programming_Guide
{

    namespace Chapter_8
    {

        ///
        //  Create an OpenCL context on the first available platform using
        //  either a GPU or CPU depending on what is available.
        //
        cl_context CreateContext()
        {
            cl_int errNum;
            cl_uint numPlatforms;
            cl_platform_id firstPlatformId;
            cl_context context = NULL;

            // First, select an OpenCL platform to run on.  For this example, we
            // simply choose the first available platform.  Normally, you would
            // query for all available platforms and select the most appropriate one.
            errNum = clGetPlatformIDs(1, &firstPlatformId, &numPlatforms);
            if (errNum != CL_SUCCESS || numPlatforms <= 0)
            {
                std::cerr << "Failed to find any OpenCL platforms." << std::endl;
                return NULL;
            }

            // Next, create an OpenCL context on the platform.  Attempt to
            // create a GPU-based context, and if that fails, try to create
            // a CPU-based context.
            cl_context_properties contextProperties[] =
            {
                CL_CONTEXT_PLATFORM,
                (cl_context_properties)firstPlatformId,
                0
            };
            context = clCreateContextFromType(contextProperties, CL_DEVICE_TYPE_GPU,
                NULL, NULL, &errNum);
            if (errNum != CL_SUCCESS)
            {
                std::cout << "Could not create GPU context, trying CPU..." << std::endl;
                context = clCreateContextFromType(contextProperties, CL_DEVICE_TYPE_CPU,
                    NULL, NULL, &errNum);
                if (errNum != CL_SUCCESS)
                {
                    std::cerr << "Failed to create an OpenCL GPU or CPU context." << std::endl;
                    return NULL;
                }
            }

            return context;
        }

        ///
        //  Create a command queue on the first device available on the
        //  context
        //
        cl_command_queue CreateCommandQueue(cl_context context, cl_device_id* device)
        {
            cl_int errNum;
            cl_device_id* devices;
            cl_command_queue commandQueue = NULL;
            size_t deviceBufferSize = -1;

            // First get the size of the devices buffer
            errNum = clGetContextInfo(context, CL_CONTEXT_DEVICES, 0, NULL, &deviceBufferSize);
            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Failed call to clGetContextInfo(...,GL_CONTEXT_DEVICES,...)";
                return NULL;
            }

            if (deviceBufferSize <= 0)
            {
                std::cerr << "No devices available.";
                return NULL;
            }

            // Allocate memory for the devices buffer
            devices = new cl_device_id[deviceBufferSize / sizeof(cl_device_id)];
            errNum = clGetContextInfo(context, CL_CONTEXT_DEVICES, deviceBufferSize, devices, NULL);
            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Failed to get device IDs";
                return NULL;
            }

            // In this example, we just choose the first available device.  In a
            // real program, you would likely use all available devices or choose
            // the highest performance device based on OpenCL device queries

            //commandQueue = clCreateCommandQueue(context, devices[0], 0, NULL);

            commandQueue = clCreateCommandQueueWithProperties(context, devices[0], 0, NULL);

            if (commandQueue == NULL)
            {
                std::cerr << "Failed to create commandQueue for device 0";
                return NULL;
            }

            *device = devices[0];
            delete[] devices;
            return commandQueue;
        }

        ///
        //  Create an OpenCL program from the kernel source file
        //
        cl_program CreateProgram(cl_context context, cl_device_id device, const char* fileName)
        {
            cl_int errNum;
            cl_program program;

            std::ifstream kernelFile(fileName, std::ios::in);
            if (!kernelFile.is_open())
            {
                std::cerr << "Failed to open file for reading: " << fileName << std::endl;
                return NULL;
            }

            std::ostringstream oss;
            oss << kernelFile.rdbuf();

            std::string srcStdStr = oss.str();
            const char* srcStr = srcStdStr.c_str();
            program = clCreateProgramWithSource(context, 1,
                (const char**)&srcStr,
                NULL, NULL);
            if (program == NULL)
            {
                std::cerr << "Failed to create CL program from source." << std::endl;
                return NULL;
            }

            errNum = clBuildProgram(program, 0, NULL, NULL, NULL, NULL);
            if (errNum != CL_SUCCESS)
            {
                // Determine the reason for the error
                char buildLog[16384];
                clGetProgramBuildInfo(program, device, CL_PROGRAM_BUILD_LOG,
                    sizeof(buildLog), buildLog, NULL);

                std::cerr << "Error in kernel: " << std::endl;
                std::cerr << buildLog;
                clReleaseProgram(program);
                return NULL;
            }

            return program;
        }


        ///
        //  Cleanup any created OpenCL resources
        //
        void Cleanup(cl_context context, cl_command_queue commandQueue,
            cl_program program, cl_kernel kernel, cl_mem imageObjects[2],
            cl_sampler sampler)
        {
            for (int i = 0; i < 2; i++)
            {
                if (imageObjects[i] != 0)
                    clReleaseMemObject(imageObjects[i]);
            }
            if (commandQueue != 0)
                clReleaseCommandQueue(commandQueue);

            if (kernel != 0)
                clReleaseKernel(kernel);

            if (program != 0)
                clReleaseProgram(program);

            if (sampler != 0)
                clReleaseSampler(sampler);

            if (context != 0)
                clReleaseContext(context);

        }

        ///
        //  Load an image using the FreeImage library and create an OpenCL
        //  image out of it
        //
        cl_mem LoadImage(cl_context context, char *fileName, int &width, int &height)
        {
            //FREE_IMAGE_FORMAT format = FreeImage_GetFileType(fileName, 0);
            //FIBITMAP* image = FreeImage_Load(format, fileName);

            // Convert to 32-bit image
            //FIBITMAP* temp = image;
            //image = FreeImage_ConvertTo32Bits(image);
            //FreeImage_Unload(temp);

            //width = FreeImage_GetWidth(image);
            //height = FreeImage_GetHeight(image);

            width = 32;
            height = 32;

            char *buffer = new char[width * height * 4];
            for (int i = 0; i < width * height * 4; i++)
            {
                buffer[i] = i & 255;
                //std::cout << "Buffer " << i << " " << buffer[i] << std::endl;
            }
                
            //memcpy(buffer, FreeImage_GetBits(image), width * height * 4);

            //FreeImage_Unload(image);

            // Create OpenCL image
            cl_image_format clImageFormat;
            clImageFormat.image_channel_order = CL_RGBA;
            clImageFormat.image_channel_data_type = CL_UNORM_INT8;


            /*
            cl_mem_object_type      image_type;
            size_t                  image_width;
            size_t                  image_height;
            size_t                  image_depth;
            size_t                  image_array_size;
            size_t                  image_row_pitch;
            size_t                  image_slice_pitch;
            cl_uint                 num_mip_levels;
            cl_uint                 num_samples;
            */

            cl_image_desc desc;
            desc.image_type = CL_MEM_OBJECT_IMAGE2D;
            desc.image_width = width;
            desc.image_height = height;
            desc.image_depth = 1;
            desc.image_array_size = 0;
            desc.image_row_pitch = 4 * width;
            desc.image_slice_pitch = 0;
            desc.num_mip_levels = 0;
            desc.num_samples = 0;

            /*
            cl_mem CL_CreateImage(
                cl_context context,
                cl_mem_flags flags,
                cl_image_format format,
                cl_image_desc desc,
                void* host_ptr,
                cl_int * error)
            {
                return clCreateImage(context, flags, &format,
                    &desc, host_ptr, error);
            }
            */

            cl_int errNum;
            cl_mem clImage;
            clImage = clCreateImage(context,
                                    CL_MEM_READ_ONLY | CL_MEM_COPY_HOST_PTR,
                                    &clImageFormat,
                                    &desc,
                                    buffer,
                                    &errNum);

            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error creating CL image object " << errNum << std::endl;
                return 0;
            }

            return clImage;
        }


        ///
        //  Save an image using the FreeImage library
        //
        bool SaveImage(char *fileName, char *buffer, int width, int height)
        {

            //FREE_IMAGE_FORMAT format = FreeImage_GetFIFFromFilename(fileName);
            //FIBITMAP *image = FreeImage_ConvertFromRawBits((BYTE*)buffer, width,
            //                    height, width * 4, 32,
            //                    0xFF000000, 0x00FF0000, 0x0000FF00);
            //return (FreeImage_Save(format, image, fileName) == TRUE) ? true : false;

            return true;
        }

        ///
        //  Round up to the nearest multiple of the group size
        //
        size_t RoundUp(int groupSize, int globalSize)
        {
            int r = globalSize % groupSize;
            if (r == 0)
            {
                return globalSize;
            }
            else
            {
                return globalSize + groupSize - r;
            }
        }

  
        int Run()
        {
            cl_context context = 0;
            cl_command_queue commandQueue = 0;
            cl_program program = 0;
            cl_device_id device = 0;
            cl_kernel kernel = 0;
            cl_mem imageObjects[2] = { 0, 0 };
            cl_sampler sampler = 0;
            cl_int errNum;

            int argc;
            char** arg;

            //if (argc != 3)
            //{
            //    std::cerr << "USAGE: " << argv[0] << " <inputImageFile> <outputImageFiles>" << std::endl;
            //    return 1;
            //}

            // Create an OpenCL context on first available platform
            context = CreateContext();
            if (context == NULL)
            {
                std::cerr << "Failed to create OpenCL context." << std::endl;
                return 1;
            }

            // Create a command-queue on the first device available
            // on the created context
            commandQueue = CreateCommandQueue(context, &device);
            if (commandQueue == NULL)
            {
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Make sure the device supports images, otherwise exit
            cl_bool imageSupport = CL_FALSE;
            clGetDeviceInfo(device, CL_DEVICE_IMAGE_SUPPORT, sizeof(cl_bool),
                &imageSupport, NULL);
            if (imageSupport != CL_TRUE)
            {
                std::cerr << "OpenCL device does not support images." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Load input image from file and load it into
            // an OpenCL image object
            int width, height;
            imageObjects[0] = LoadImage(context, NULL, width, height);
            if (imageObjects[0] == 0)
            {
                //std::cerr << "Error loading: " << std::string(NULL) << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Create ouput image object
            cl_image_format clImageFormat;
            clImageFormat.image_channel_order = CL_RGBA;
            clImageFormat.image_channel_data_type = CL_UNORM_INT8;

            cl_image_desc desc;
            desc.image_type = CL_MEM_OBJECT_IMAGE2D;
            desc.image_width = width;
            desc.image_height = height;
            desc.image_depth = 1;
            desc.image_array_size = 0;
            desc.image_row_pitch = 4 * width;
            desc.image_slice_pitch = 0;
            desc.num_mip_levels = 0;
            desc.num_samples = 0;

            imageObjects[1] = clCreateImage(context,
                CL_MEM_WRITE_ONLY,
                &clImageFormat,
                &desc,
                0,
                &errNum);

            /*
            imageObjects[1] = clCreateImage2D(context,
                CL_MEM_WRITE_ONLY,
                &clImageFormat,
                width,
                height,
                0,
                NULL,
                &errNum);
                */

            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error creating CL output image object." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            /*
            // Create sampler for sampling image object
            sampler = clCreateSampler(context,
                CL_FALSE, // Non-normalized coordinates
                CL_ADDRESS_CLAMP_TO_EDGE,
                CL_FILTER_NEAREST,
                &errNum);
                */

            cl_sampler_properties props[] =
            {
              CL_SAMPLER_NORMALIZED_COORDS,
              CL_FALSE,
              CL_SAMPLER_ADDRESSING_MODE,
              CL_ADDRESS_CLAMP_TO_EDGE,
              CL_SAMPLER_FILTER_MODE,
              CL_FILTER_NEAREST,
              0
            };

            sampler = clCreateSamplerWithProperties(context, props, &errNum);

            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error creating CL sampler object." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Create OpenCL program
            program = CreateProgram(context, device, "OpenCL_Programming_Guide/Chapter_8/ImageFilter2D.cl");
            if (program == NULL)
            {
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Create OpenCL kernel
            kernel = clCreateKernel(program, "gaussian_filter", NULL);
            if (kernel == NULL)
            {
                std::cerr << "Failed to create kernel" << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Set the kernel arguments
            errNum = clSetKernelArg(kernel, 0, sizeof(cl_mem), &imageObjects[0]);
            errNum |= clSetKernelArg(kernel, 1, sizeof(cl_mem), &imageObjects[1]);
            errNum |= clSetKernelArg(kernel, 2, sizeof(cl_sampler), &sampler);
            errNum |= clSetKernelArg(kernel, 3, sizeof(cl_int), &width);
            errNum |= clSetKernelArg(kernel, 4, sizeof(cl_int), &height);
            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error setting kernel arguments." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            size_t localWorkSize[2] = { 16, 16 };
            size_t globalWorkSize[2] = { RoundUp(localWorkSize[0], width),
                                          RoundUp(localWorkSize[1], height) };

            // Queue the kernel up for execution
            errNum = clEnqueueNDRangeKernel(commandQueue, kernel, 2, NULL,
                globalWorkSize, localWorkSize,
                0, NULL, NULL);

            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error queuing kernel for execution." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            // Read the output buffer back to the Host
            char* buffer = new char[width * height * 4];
            size_t origin[3] = { 0, 0, 0 };
            size_t region[3] = { width, height, 1 };
            errNum = clEnqueueReadImage(commandQueue, imageObjects[1], CL_TRUE,
                origin, region, 0, 0, buffer,
                0, NULL, NULL);

            for (int i = 0; i < width * height * 4; i++)
                std::cout << "Buffer = " << (int)buffer[i] << std::endl;

            if (errNum != CL_SUCCESS)
            {
                std::cerr << "Error reading result buffer." << std::endl;
                Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
                return 1;
            }

            std::cout << std::endl;
            std::cout << "Executed program succesfully." << std::endl;

            memset(buffer, 0xff, width * height * 4);

            //Save the image out to disk
            if (!SaveImage(NULL, buffer, width, height))
            {
                //std::cerr << "Error writing output image: " << argv[2] << std::endl;
               //Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
               //delete [] buffer;
               //return 1;
           }

            delete[] buffer;
            Cleanup(context, commandQueue, program, kernel, imageObjects, sampler);
            return 0;
        }

    }

}
