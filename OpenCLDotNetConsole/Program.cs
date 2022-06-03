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
			cl_uint numEntries = new cl_uint();
			cl_uint[] numPlatforms = new cl_uint[1];
			//cl_platform_id[] platformIds = new cl_platform_id[1];
			//cl_context context;

			// First, query the total number of platforms
			errNum = CL.GetPlatformIDs(numEntries, null, numPlatforms);

			if (errNum != CL_ERROR.CL_SUCCESS || numPlatforms[0].Value <= 0)
			{
				Console.WriteLine("Failed to find any OpenCL platform.");
				return;
			}
			


		}

    }
}
