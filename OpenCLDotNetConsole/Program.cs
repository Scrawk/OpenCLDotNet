using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet;

namespace OpenCLDotNetConsole
{
    public class Program
    {

        static void Main(string[] args)
        {
			CL_ERROR errNum;
			uint numPlatforms;
			errNum = CL.GetPlatformIDs(out numPlatforms);

			if (errNum != CL_ERROR.CL_SUCCESS || numPlatforms <= 0)
			{
				Console.WriteLine("Failed to find any OpenCL platform.");
				return;
			}

			var platforms = new cl_platform_id[numPlatforms];
			errNum = CL.GetPlatformIDs(numPlatforms, platforms);

			if (errNum != CL_ERROR.CL_SUCCESS)
			{
				Console.WriteLine("Failed to find any OpenCL platform.");
				return;
			}

			foreach(var p in platforms)
			Console.WriteLine("platform " + p);


		}

    }
}
