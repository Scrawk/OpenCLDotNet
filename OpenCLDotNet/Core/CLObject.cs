using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// Base class for objects that referrence a CL object.
    /// </summary>
    public abstract class CLObject : IDisposable
    {
        protected const string DLL_NAME = "OpenCLWrapper.dll";

        protected const CallingConvention CDECL = CallingConvention.Cdecl;

        /// <summary>
        /// Default constructor.
        /// </summary>
        internal CLObject()
        {
            
        }

        /// <summary>
        /// The destuctor.
        /// </summary>
        ~CLObject()
        {
            ReleaseInternal();
        }

        /// <summary>
        /// Has the object been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// Dispose the CL object.
        /// </summary>
        public void Dispose()
        {
            ReleaseInternal();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Resets the error code to the value used when 
        /// nothing has been ran the generates a error.
        /// </summary>
        public void ResetErrorCode()
        {
            Error = CL_ERROR.NONE.ToString();   
        }

        /// <summary>
        /// If error code not default value or success.
        /// </summary>
        public bool HasError => Error != CL_ERROR.NONE.ToString() || 
                                Error != CL_ERROR.SUCCESS.ToString();

        /// <summary>
        /// Print some information about the object.
        /// </summary>
        /// <returns>Print some information about the object.</returns>
        public override string ToString()
        {
            return String.Format("[CLObject: IsDiposed={0}, Error={1}]", 
                IsDisposed, Error);
        }

        /// <summary>
        /// Print the object into the console.
        /// </summary>
        public void Print()
        {
            var buider = new StringBuilder();
            Print(buider);
            Console.WriteLine(buider.ToString());
        }

        /// <summary>
        /// Print the object into a string builder.
        /// </summary>
        /// <param name="builder">The builder to print into.</param>
        public virtual void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());
        }

        /// <summary>
        /// Release the CGAL object.
        /// </summary>
        private void ReleaseInternal()
        {
            if (!IsDisposed)
            {
                Release();
                IsDisposed = true;
            }
        }

        /// <summary>
        /// Allow derived class to release the unmanaged memory.
        /// </summary>
        protected virtual void Release()
        {

        }
    }
}
