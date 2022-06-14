using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Utility;

[assembly: InternalsVisibleTo("OpenGLDotNetConsole")]
[assembly: InternalsVisibleTo("OpenGLDotNetTest")]

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {
        /// <summary>
        /// 
        /// </summary>
        private const string DLL_NAME = "OpenCLWrapper.dll";

        /// <summary>
        /// 
        /// </summary>
        private const CallingConvention CDECL = CallingConvention.Cdecl;

        /// <summary>
        /// 
        /// </summary>
        public static string Version
        {
            get
            {
                return CL_VersionNumber().ToString();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern int CL_VersionNumber();

    }
}
