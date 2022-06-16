using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class CLUnmanagedResourcesReleasedExeception : Exception
    {
        public CLUnmanagedResourcesReleasedExeception()
        {
        }

        public CLUnmanagedResourcesReleasedExeception(string message)
            : base(message)
        {
        }

        public CLUnmanagedResourcesReleasedExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CLUnmanagedResourcesNotReleasedExeception : Exception
    {
        public CLUnmanagedResourcesNotReleasedExeception()
        {
        }

        public CLUnmanagedResourcesNotReleasedExeception(string message)
            : base(message)
        {
        }

        public CLUnmanagedResourcesNotReleasedExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvalidArrayExeception : Exception
    {
        public InvalidArrayExeception()
        {
        }

        public InvalidArrayExeception(string message)
            : base(message)
        {
        }

        public InvalidArrayExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
