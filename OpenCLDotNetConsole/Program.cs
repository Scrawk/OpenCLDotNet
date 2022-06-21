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
			//context.Print();

			//var filename = "F:/Projects/Visual Studio Projects/OpenCLDotNet/Programs/Convolution.cl";

			var program_text =
			@"__kernel void Kernel(__global const float* a, __global const float* b, __global float* result)
			{
				int gid = get_global_id(0);
				result[gid] = a[gid] + b[gid];
			}
			";

			var options = CLProgram.OPTION_KERNEL_ARG_INFO;

			var program = new CLProgram(context, program_text, options);
			//program.Print();

			uint ARRAY_SIZE = 100;

			var data0 = new float[ARRAY_SIZE];
			var data1 = new float[ARRAY_SIZE];
			var data2 = new float[ARRAY_SIZE];

			for (uint i = 0; i < ARRAY_SIZE; i++)
			{
				data0[i] = i;
				data1[i] = i;
			}

			var buffer0 = new CLBuffer(context, CL_READ_WRITE.READ, data0);
			buffer0.Print();

			var buffer1 = new CLBuffer(context, CL_READ_WRITE.READ, data1);
			buffer1.Print();

			var buffer2 = new CLBuffer(context, CL_READ_WRITE.WRITE, CL_MEM_DATA_TYPE.FLOAT, ARRAY_SIZE);
			buffer2.Print();

			var kernel = new CLKernel(program, "Kernel");

			kernel.SetBufferArg(buffer0, 0);
			kernel.SetBufferArg(buffer1, 1);
			kernel.SetBufferArg(buffer2, 2);
			kernel.Print();

			var cmd = new CLCommandQueue(context);
			cmd.Print();


			size_t[] globalWorkSize = { ARRAY_SIZE };
			size_t[] localWorkSize = { 1 };
			cl_event e;

			var error = CL.EnqueueNDRangeKernel(cmd.Id, kernel.Id, 1, null, globalWorkSize, localWorkSize, 0, null, out e);

			Console.WriteLine("EnqueueNDRangeKernel: " + error);
			if (error != CL_ERROR.SUCCESS)
				return;

			var result = new float[ARRAY_SIZE];

			error = CL.EnqueueReadBuffer(cmd.Id, buffer2.Id, true, 0, buffer2.ByteSize, result, 0, null, out e);
			Console.WriteLine("EnqueueReadBuffer: " + error);
			if (error != CL_ERROR.SUCCESS)
				return;

			for (int i = 0; i < result.Length; i++)
				Console.WriteLine(result[i]);

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

