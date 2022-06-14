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

			var program = new CLProgram(context, filename);
			//program.Print();
		}

	}
}
