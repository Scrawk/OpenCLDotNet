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

			foreach(var id in ids)
            {
				var platform = new CLPlatform(id);
				platform.Print();
			}
			
		}

    }
}
