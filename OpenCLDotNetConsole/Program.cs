using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet.Utility;
using OpenCLDotNet.Core;
using OpenCLDotNet.Programs;

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
			program.Print();

			var binary = program.GetBinary();
			var program2 = new CLProgram(context, binary, options);

			var kernel = new CLKernel(program2, "convolve");

			kernel.SetIntArg(10, 3);

			//kernel.Print();
		}

	}
}
