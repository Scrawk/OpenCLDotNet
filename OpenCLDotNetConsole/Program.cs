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

		private const uint WIDTH = 10;

		private const uint HEIGHT = 10;

		private const uint CHANNELS = 4;

		private const uint SIZE = WIDTH * HEIGHT * CHANNELS;

		static void Main(string[] args)
		{

			var context = new CLContext();
			//context.Print();

			//var filename = "F:/Projects/Visual Studio Projects/OpenCLDotNet/Programs/Convolution.cl";

			var program_text1 =
			@"__kernel void Kernel1(__global const float* a, __global const float* b, __global float* result)
			{
				int gid = get_global_id(0);
				result[gid] = a[gid] + b[gid];
			}
			";

			var program_text2 =

			@"__kernel void gaussian_filter(__read_only image2d_t srcImg,
							  __write_only image2d_t dstImg,
							  sampler_t sampler,
							  int width, int height)
			{
				// Gaussian Kernel is:
				// 1  2  1
				// 2  4  2
				// 1  2  1
				float kernelWeights[9] = { 1.0f, 2.0f, 1.0f,
							   2.0f, 4.0f, 2.0f,
							   1.0f, 2.0f, 1.0f };

				int2 startImageCoord = (int2)(get_global_id(0) - 1, get_global_id(1) - 1);
				int2 endImageCoord = (int2)(get_global_id(0) + 1, get_global_id(1) + 1);
				int2 outImageCoord = (int2)(get_global_id(0), get_global_id(1));

				if (outImageCoord.x < width && outImageCoord.y < height)
				{
					int weight = 0;
					float4 outColor = (float4)(0.0f, 0.0f, 0.0f, 0.0f);
					for (int y = startImageCoord.y; y <= endImageCoord.y; y++)
					{
						for (int x = startImageCoord.x; x <= endImageCoord.x; x++)
						{
							outColor += (read_imagef(srcImg, sampler, (int2)(x, y)) * (kernelWeights[weight] / 16.0f));
							weight += 1;
						}
					}

					// Write the output value to image
					write_imagef(dstImg, outImageCoord, outColor);
				}
			}
			";

			var program_text3 =

			@"__kernel void read_write_test(__read_only image2d_t srcImg,
											__write_only image2d_t dstImg,
											sampler_t sampler,
											int width, int height)
			{

				int2 imageCoord = (int2)(get_global_id(0), get_global_id(1));

				if (imageCoord.x < width && imageCoord.y < height)
				{
					//float4 outColor = (float4)(0.0f, 0.0f, 0.0f, 0.0f);
		
					float4 outColor = read_imagef(srcImg, sampler, imageCoord);
				
					write_imagef(dstImg, imageCoord, outColor);
				}
			}
			";

			var options = CLProgram.DefaultOptions;

			var program = new CLProgram(context, program_text3, options);
			if(program.HasError)
            {
				Console.WriteLine("Program has error");
				Console.WriteLine(program.Error);
				Console.WriteLine("");
				Console.WriteLine(program.GetBuildLog(0));

				return;
            }

			string kernel_name = "read_write_test";
			
			var data = new byte[SIZE];
			for (uint i = 0; i < SIZE; i++)
			{
				data[i] = (byte)i;
			}

			var param = CLImageParameters2D.ByteImage(WIDTH, HEIGHT, 4);
			param.Source = data;

			var read_image = program.CreateReadImage2D(kernel_name, 0, param);
			var write_image = program.CreateWriteImage2D(kernel_name, 1, param);

			if(read_image.HasError)
            {
				Console.WriteLine("Read image has error : " + read_image.Error);
				return;
            }

			if (write_image.HasError)
			{
				Console.WriteLine("Write image has error : " + write_image.Error);
				return;
			}

			program.CreateSamplerIndex(kernel_name, 2);
			program.SetInt(kernel_name, (int)WIDTH, 3);
			program.SetInt(kernel_name, (int)HEIGHT, 4);
			//program.Print();

			var global_offset = new CLPoint2t(0);
			var global_size = new CLPoint2t(WIDTH, HEIGHT);
			var local_size = new CLPoint2t(16, 16);

			//program.Run(kernel_name, global_offset, global_size, local_size);
			//Console.WriteLine("Program error " + program.Error);

			var color = new float[]
			{
				0.25f, 0.5f, 0.75f, 1.0f
			};

			write_image.Fill(color, CL_DATA_TYPE.FLOAT);

			var copy_image = write_image.Copy();

			var result = new byte[SIZE];
			copy_image.Read(result);

			//for (int i = 0; i < 100; i++)
			//	Console.WriteLine(result[i]);

			context.Print();

		}

	}
}

