using System;
using System.Text;
using System.Collections.Generic;

using OpenCLDotNet.Utility;
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

			var devices = new List<cl_device_id>();
			CL.GetDeviceIDs(platform.Id, CL_DEVICE_TYPE.ALL, devices);

			foreach (var id in devices)
			{
				var device = new CLDevice(id);
				device.Print();
	
			}

		}

	}
}
