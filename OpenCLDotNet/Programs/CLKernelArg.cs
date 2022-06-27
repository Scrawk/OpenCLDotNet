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
        public uint Index { get; set; }

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
        public object ArgObject { get; set; }

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
        /// <param name="argIndex"></param>
        /// <param name="address"></param>
        /// <param name="access"></param>
        /// <param name="arg"></param>
        public CLKernelArg(string name, string argType, uint argIndex, string address, string access, object arg)
        {
            Name = name;
            ArgType = argType;
            Index = argIndex;
            AddressQualifier = address;  
            AccessQualifier = access;
            ArgObject = arg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string arg_or_null = ArgObject == null ? "Null" : ArgObject.ToString();

            return String.Format("[CLKernelArg: Name={0}, Address={1}, Access={2}, ArgType={3}, Arg={4}]",
                Name, AddressQualifier, AccessQualifier, ArgType, arg_or_null);
        }

    }
}
