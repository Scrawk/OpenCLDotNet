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

    /// <summary>
    /// 
    /// </summary>
    public class InvalidObjectExeception : Exception
    {
        public InvalidObjectExeception()
        {
        }

        public InvalidObjectExeception(string message)
            : base(message)
        {
        }

        public InvalidObjectExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvalidDataSizeExeception : Exception
    {
        public InvalidDataSizeExeception()
        {
        }

        public InvalidDataSizeExeception(string message)
            : base(message)
        {
        }

        public InvalidDataSizeExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvalidDataTypeExeception : Exception
    {
        public InvalidDataTypeExeception()
        {
        }

        public InvalidDataTypeExeception(string message)
            : base(message)
        {
        }

        public InvalidDataTypeExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ReadWriteExeception : Exception
    {
        public ReadWriteExeception()
        {
        }

        public ReadWriteExeception(string message)
            : base(message)
        {
        }

        public ReadWriteExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CyclicGraphExeception : Exception
    {
        public CyclicGraphExeception()
        {
        }

        public CyclicGraphExeception(string message)
            : base(message)
        {
        }

        public CyclicGraphExeception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
