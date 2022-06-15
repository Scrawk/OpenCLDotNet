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

			var data1 = new float[100];

			var buffer = new CLBuffer(context, data1);
			//buffer.Print();

			var region = new CLBufferRegion(0, 10);
			var sub_buffer = new CLSubBuffer(buffer, region);
			//sub_buffer.Print();

			var image = new CLImage2D(context, 10, 10, data1);
			//image.Print();

			var flags = CL_MEM_FLAGS.READ_WRITE;
			var type = CL_MEM_OBJECT_TYPE.IMAGE2D;

			var error = CL.GetSupportedImageFormatsSize(context.Id, flags, type, out uint size);
			
			Console.WriteLine(error);
			Console.WriteLine(size);

			var formats = new CLImageFormat[size];
			error = CL.GetSupportedImageFormats(context.Id, flags, type, formats);

			Console.WriteLine(error);

			for(int i = 0; i < formats.Length; i++)	
            {
				Console.WriteLine(i + " " + formats[i]);	
            }
		}

	}
}
