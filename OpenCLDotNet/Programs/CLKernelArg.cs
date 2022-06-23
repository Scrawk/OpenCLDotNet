using System;
using System.Collections.Generic;

namespace OpenCLDotNet.Programs
{
    /// <summary>
    /// 
    /// </summary>
    public class CLKernelArg
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArgType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressQualifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AccessQualifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Arg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CLKernelArg()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public CLKernelArg(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="argType"></param>
        /// <param name="address"></param>
        /// <param name="access"></param>
        /// <param name="arg"></param>
        public CLKernelArg(string name, string argType, string address, string access, object arg)
        {
            Name = name;
            ArgType = argType;
            AddressQualifier = address;  
            AccessQualifier = access;
            Arg = arg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string arg_or_null = Arg == null ? "Null" : Arg.ToString();

            return String.Format("[CLKernelArg: Name={0}, Address={1}, Access={2}, ArgType={3}, Arg={4}]",
                Name, AddressQualifier, AccessQualifier, ArgType, arg_or_null);
        }

    }
}
