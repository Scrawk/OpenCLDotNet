using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Programs
{
    public class CLProgram : CLObject
    {

        public CLProgram()
        {

        }

        public void Create(CLContext context, string filename)
        {
            var file = File.ReadAllText(filename, Encoding.UTF8);

            CL_ERROR error;
            var program = CL.CreateProgramWithSource(context.Id, file, out error);

            var devices = context.GetDeviceIds();

            var options = "";
            //options += "-D FAST_ALGORITM";
            //options += " -D MAX_ITERATIONS=20";
            //options += "-cl-single-precision-constant";

            error = CL.BuildProgram(program, (uint)devices.Length, devices, options);

            CL_BUILD_STATUS status;
            error = CL.GetProgramBuildInfo(program, devices[0], CL_PROGRAM_BUILD_INFO.STATUS, out status);

            Console.WriteLine("Build info = " + status);

            var name = CL_PROGRAM_BUILD_INFO.OPTIONS;
            uint size;
            error = CL.GetProgramBuildInfoSize(program, devices[0], name, out size);

            cl_char[] log = new cl_char[size];
            error = CL.GetProgramBuildInfo(program, devices[0], name, size, log);

            Console.WriteLine("Build log = " + log.ToText());
        }

        protected override void Release()
        {
            //CL.ReleaseProgram(Id);
        }
    }
}
