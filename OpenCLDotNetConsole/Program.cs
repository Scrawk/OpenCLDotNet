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
					CL_DEVICE_INFO name = CL_DEVICE_INFO.MAX_WORK_ITEM_SIZES;
					uint paramValueSize;

					var err = CL.GetDeviceInfoSize(
						id,
						name,
						out paramValueSize);

					if (err != CL_ERROR.SUCCESS)
					{
						Console.WriteLine("Failed to find OpenCL device info ");
						return;
					}
				
					Console.WriteLine("paramValueSize = " + paramValueSize);

                    unsafe
					{
						var info = new size_t[paramValueSize / sizeof(size_t)];

						errNum = CL.GetDeviceInfo(
							id,
							name,
							paramValueSize,
							info);

						if (errNum != CL_ERROR.SUCCESS)
						{
							Console.WriteLine("Failed to find OpenCL device info ");
							return;
						}

						foreach (var s in info)
							Console.WriteLine(s);
					}

				}
					
			}

		}

    }
}
