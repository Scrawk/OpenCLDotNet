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

			var write_data = new float[SIZE];
			var read_data = new float[SIZE];

			for (int i = 0; i < write_data.Length; i++)
				read_data[i] = i;

			var graph = new CLCommandGraph();

			var image = CLImage2D.CreateFloatImage2D(graph.Context, WIDTH, HEIGHT, CHANNELS);

			graph.AddNode(new CLWriteImageCommand(image, read_data));
			graph.AddNode(new CLReadImageCommand(image, write_data));
			graph.AddNode(new CLWriteImageCommand(image, read_data));
			graph.AddNode(new CLReadImageCommand(image, write_data));

			//graph.Run();

			//for (int i = 0; i < 10; i++)
			//	Console.WriteLine(write_data[i]);

			/*
			var program_text =
			@"__kernel void read_write_test(__read_only image2d_t srcImg,
											__write_only image2d_t dstImg,
											sampler_t sampler,
											int width, int height)
			{

				int2 imageCoord = (int2)(get_global_id(0), get_global_id(1));

				if (imageCoord.x < width && imageCoord.y < height)
				{
					float4 outColor = read_imagef(srcImg, sampler, imageCoord);
					write_imagef(dstImg, imageCoord, outColor);
				}
			}
			";

			var program = new CLProgram(program_text);

			var data = new float[SIZE];
			for (int i = 0; i < data.Length; i++)
				data[i] = i;	

			var read_image_param = CLImageParameters2D.FloatImage(WIDTH, HEIGHT, CHANNELS);
			read_image_param.Source = data;

			var write_image_param = CLImageParameters2D.FloatImage(WIDTH, HEIGHT, CHANNELS);

			var kernel_params = new CLKernelParameter()
			{
				Name = "read_write_test",
				Dimension = 2,
				GlobalSize = new CLPoint3t(WIDTH, HEIGHT, 1),
				LocalSize = new CLPoint3t(16, 16, 1),

				Args = new()
				{
					new CLKernelArgParameter(0, program.CreateReadImage2D(read_image_param)),
					new CLKernelArgParameter(1, program.CreateWriteImage2D(write_image_param)),
					new CLKernelArgParameter(2, program.CreateSamplerIndex()),
					new CLKernelArgParameter(3, WIDTH),
					new CLKernelArgParameter(4, HEIGHT)
				}

			};

			program.Run(kernel_params);
			Console.WriteLine(program.Error);

			program.Command.Finish();

			var image = program.GetImage("read_write_test", 1);

			var result = new float[SIZE];
			image.Read(program.Command, result);

			for (int i = 0; i < 100; i++)
				Console.WriteLine(result[i]);
			*/

		}

	}
}

