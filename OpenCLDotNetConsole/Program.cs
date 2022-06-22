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
			@"__kernel void Kernel1(__global const float* a, __global const float* b, __global float* result)
			{
				int gid = get_global_id(0);
				result[gid] = a[gid] + b[gid];
			}

			__kernel void Kernel2(__global const float* a, __global const float* b, __global float* result)
			{
				int gid = get_global_id(0);
				result[gid] = a[gid] + b[gid];
			}

			__kernel void Kernel3(__global const float* a, __global const float* b, __global float* result)
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

			var buffer0 = CLBuffer.CreateReadBuffer(context, data0);
			//buffer0.Print();

			var buffer1 = CLBuffer.CreateReadBuffer(context, data0);
			//buffer1.Print();

			var buffer2 = CLBuffer.CreateWriteBuffer(context, CL_MEM_DATA_TYPE.FLOAT, ARRAY_SIZE);
			//buffer2.Print();

			program.SetBuffer("Kernel1", buffer0, 0);
			program.SetBuffer("Kernel1", buffer1, 1);
			program.SetBuffer("Kernel1", buffer2, 2);

			//program.Print();

			return;

			program.Run("Kernel1", 0, 100);
			Console.WriteLine("Program error " + program.Error);

			var result = new float[ARRAY_SIZE];
			var cmd = new CLCommandQueue(context);
			buffer2.ReadBuffer(cmd, result, true);

			Console.WriteLine("Buffer error " + buffer2.Error);

			//for (int i = 0; i < result.Length; i++)
			//	Console.WriteLine(result[i]);

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

