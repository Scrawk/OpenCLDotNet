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

		private const uint CHANNELS = 1;

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

			@"__kernel void gaussian_filter(__read_only image2d_t srcImg,
											__write_only image2d_t dstImg,
											sampler_t sampler,
											int width, int height)
			{

				int2 imageCoord = (int2)(get_global_id(0), get_global_id(1));

				if (oimageCoord.x < width && imageCoord.y < height)
				{
					//float4 outColor = (float4)(0.0f, 0.0f, 0.0f, 0.0f);
		
					float4 outColor = read_imagef(srcImg, sampler, imageCoord);
				
					write_imagef(dstImg, imageCoord, outColor);
				}
			}
			";

			var options = CLProgram.DefaultOptions;

			var program = new CLProgram(context, program_text2, options);
			//program.Print();

			uint WIDTH = 100;
			uint HEIGHT = 100;
			uint ARRAY_SIZE = WIDTH * HEIGHT;

			
			var data0 = new float[ARRAY_SIZE];
			//var data1 = new float[ARRAY_SIZE];
			//var data2 = new float[ARRAY_SIZE];

			for (uint i = 0; i < ARRAY_SIZE; i++)
			{
				data0[i] = i;
				//data1[i] = i;
			}
			

			var image_params = new CLImageParameters2D();
			image_params.Width = WIDTH;
			image_params.Height = HEIGHT;
			image_params.ChannelOrder = CL_CHANNEL_ORDER.R;
			image_params.ChannelType = CL_CHANNEL_TYPE.FLOAT;
			image_params.DataType = CL_MEM_DATA_TYPE.FLOAT;
			image_params.DataLength = ARRAY_SIZE;
			image_params.Source = data0;

			var image = CLImage2D.CreateReadImage2D(context, image_params);
			//image.Print();

			//program.CreateReadBuffer("Kernel1", 0, data0);
			//program.CreateReadBuffer("Kernel1", 1, data1);
			//program.CreateWriteBuffer("Kernel1", 2, CL_MEM_DATA_TYPE.FLOAT, ARRAY_SIZE);

			program.CreateReadImage2D("gaussian_filter", 0, image_params);
			program.CreateWriteImage2D("gaussian_filter", 1, image_params);
			program.CreateSamplerIndex("gaussian_filter", 2);
			program.SetInt("gaussian_filter", (int)WIDTH, 3);
			program.SetInt("gaussian_filter", (int)HEIGHT, 4);

			//program.Print();

			var offset = new CLPoint3t(0);
			var size = new CLPoint3t(WIDTH, HEIGHT, 0);

			program.Run("gaussian_filter", offset, size);
			Console.WriteLine("Program error " + program.Error);

			var result = new float[ARRAY_SIZE];
			program.ReadImage("gaussian_filter", true, 1, result);

			for (int i = 0; i < 100; i++)
				Console.WriteLine(result[i]);

		}

	}
}

