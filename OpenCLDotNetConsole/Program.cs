using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet.Utility;
using OpenCLDotNet.Core;
using OpenCLDotNet.Programs;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;

namespace OpenCLDotNetConsole
{
	public class Program
	{

		static void Main(string[] args)
		{


			var context = new CLContext();
			context.Print();

			//var filename = "F:/Projects/Visual Studio Projects/OpenCLDotNet/Programs/Convolution.cl";

			var program_text =
			@"__kernel void Kernel(__global int* result)
			{
				int gid = get_global_id(0);
				result[gid] = gid;
			}
			";

			var options = CLProgram.OPTION_KERNEL_ARG_INFO;

			var program = new CLProgram(context, program_text, options);
			program.Print();

			var buffer_data = new CLBufferData();
			buffer_data.Flags = CL_MEM_FLAGS.READ_WRITE;
			buffer_data.SetSource(new int[100]);

			var buffer = new CLBuffer(context, buffer_data);
			buffer.Print();

			var kernel = new CLKernel(program, "Kernel");
			
			kernel.SetBufferArg(buffer, 0);
			kernel.Print();

			//var region = new CLBufferRegion(0, 10);
			//var sub_buffer = new CLSubBuffer(buffer, region);
			//sub_buffer.Print();


			/*
			var image_data = new CLImageData2D();
			image_data.Width = 10;
			image_data.Height = 10;
			image_data.ChannelOrder = CL_CHANNEL_ORDER.R;
			image_data.ChannelType = CL_CHANNEL_TYPE.FLOAT;
			image_data.Flags = CL_MEM_FLAGS.READ_WRITE;
			image_data.SetSource(new float[100]);

			var image = new CLImage2D(context, image_data);
			//image.Print();
			*/

			var cmd = new CLCommandQueue(context);
			//cmd.Print();	

			/*
			var sampler_props = new CLSamplerProperties();
			sampler_props.NormalizedCoords = false;
			sampler_props.AddressingMode = CL_SAMPLER_ADDRESSING_MODE.REPEAT;
			sampler_props.FilterMode = CL_SAMPLER_FILTER_MODE.NEAREST;

			var sampler = new CLSampler(context, sampler_props);
			sampler.Print();
			*/

			//var cmd = new CLCommandQueue(context);
			//cmd.Print();

			//var _event = new CLEvent(context);
			//_event.Print();

		}

	}
}

