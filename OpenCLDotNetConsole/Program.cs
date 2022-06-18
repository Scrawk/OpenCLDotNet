using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet.Utility;
using OpenCLDotNet.Core;
using OpenCLDotNet.Programs;
using OpenCLDotNet.Buffers;

namespace OpenCLDotNetConsole
{
	public class Program
	{

		static void Main(string[] args)
		{

			var context = new CLContext(CL_DEVICE_TYPE.GPU);
			//context.Print();

			var filename = "F:/Projects/Visual Studio Projects/OpenCLDotNet/Programs/Convolution.cl";

			var options = "-cl-kernel-arg-info";
			var program = new CLProgram(context, filename, options);
			//program.Print();

			//var kernel = new CLKernel(program, "convolve");
			//kernel.SetIntArg(10, 3);
			//kernel.Print();

			/*
			var buffer_data = new CLBufferData();
			buffer_data.Flags = CL_MEM_FLAGS.READ_WRITE | CL_MEM_FLAGS.USE_HOST_PTR;
			buffer_data.Source = new float[100];

			var buffer = new CLBuffer(context, buffer_data);
			buffer.Print();

			var region = new CLBufferRegion(0, 10);
			var sub_buffer = new CLSubBuffer(buffer, region);
			sub_buffer.Print();

			var image_data = new CLImageData2D();
			image_data.Width = 10;
			image_data.Height = 10;
			image_data.ChannelOrder = CL_CHANNEL_ORDER.R;
			image_data.ChannelType = CL_CHANNEL_TYPE.FLOAT;
			image_data.Flags = CL_MEM_FLAGS.READ_WRITE | CL_MEM_FLAGS.USE_HOST_PTR;
			image_data.Source = new float[100];

			var image = new CLImage2D(context, image_data);
			image.Print();
			*/

		}

	}
}

