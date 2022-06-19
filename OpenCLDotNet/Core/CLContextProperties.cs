using System;
using System.Collections.Generic;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public struct CLContextProperties
    {

        public cl_platform_id Platform;

        //public cl_bool InteropUserSync;

        public CLContextProperties()
        {
            Platform = UIntPtr.Zero;
            //InteropUserSync = false;    
        }

        public override string ToString()
        {
            return String.Format("[CLContextProperties: Platform={0}]", Platform);
        }

    }
}
