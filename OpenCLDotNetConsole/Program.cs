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

			var data = new float[100];

			var buffer = new CLBuffer(context, data);
			buffer.Print();

			var region = new CLBufferRegion(0, 10);
			var sub_buffer = new CLSubBuffer(buffer, region);
			sub_buffer.Print();
		}

	}
}
