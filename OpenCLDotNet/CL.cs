using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("OpenGLDotNetConsole")]
[assembly: InternalsVisibleTo("OpenGLDotNetTest")]

namespace OpenCLDotNet
{
    public static class CL
    {

        private const string DLL_NAME = "OpenCLWrapper.dll";

        private const CallingConvention CDECL = CallingConvention.Cdecl;

        public static string Version
        {
            get
            {
                return CL_VersionNumber().ToString();
            }
            
        }

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern int CL_VersionNumber();

    }
}
