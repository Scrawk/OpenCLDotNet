using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNetConsole
{
    public class Program
    {

        static void Main(string[] args)
        {
			
			var ids = new List<cl_platform_id>();
			CL.GetPlatformIDs(ids);

			var platform = new CLPlatform(ids[0]);
			//platform.Print();

			uint numDevices;
			var errNum = CL.GetDeviceIDs(
				platform.Id,
				CL_DEVICE_TYPE.ALL,
				out numDevices);

			if (errNum != CL_ERROR.SUCCESS)
			{
				Console.WriteLine("Failed to find OpenCL devices.");
				return;
			}
            else
            {
				Console.WriteLine("devices = " + numDevices);
			}

			var devices = new cl_device_id[numDevices];

			errNum = CL.GetDeviceIDs(
				platform.Id,
				CL_DEVICE_TYPE.ALL,
				numDevices,
				devices);

			if (errNum != CL_ERROR.SUCCESS)
			{
				Console.WriteLine("Failed to find OpenCL devices.");
				return;
			}
			else
			{
				foreach (var device in devices)
				{
					cl_device_id id = device;
					CL_DEVICE_INFO name = CL_DEVICE_INFO.TYPE;
					uint paramValueSize;

					var err = CL.GetDeviceInfo(
						id,
						name,
						out paramValueSize);

					if (err != CL_ERROR.SUCCESS)
					{
						Console.WriteLine("Failed to find OpenCL device info ");
						return;
					}
					else
					{
						Console.WriteLine("paramValueSize = " + paramValueSize);
					}
				}
					
			}

		}

    }
}
