using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;

namespace OpenCLDotNet.Programs
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CLKernel : CLObject
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg_name"></param>
        /// <param name="value"></param>
        public void SetArgument(string arg_name, object value)
        {
            if(value == null)
                throw new ArgumentNullException("Argument value can not be null.");

            var arg = GetArgument(arg_name);
            if (arg == null)
                throw new ArgumentException($"Kernel does not contain a argument names {arg_name}.");

            SetArgument(arg, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg_index"></param>
        /// <param name="value"></param>
        public void SetArgument(uint arg_index, object value)
        {
            if (value == null)
                throw new ArgumentNullException("Argument value can not be null.");

            var arg = GetArgument(arg_index);
            SetArgument(arg, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="value"></param>
        private void SetArgument(CLKernelArg arg, object value)
        {
            uint index = arg.Index;

            if (value is CLBuffer buffer)
                SetBuffer(buffer, index);
            else if (value is CLSubBuffer sub_buffer)
                SetSubBuffer(sub_buffer, index);
            else if (value is CLImage image)
                SetImage(image, index);
            else if (value is CLSampler sampler)
                SetSampler(sampler, index);

            else if (value is byte b)
                SetByte(b, index);
            else if (value is sbyte sb)
                SetSByte(sb, index);
            else if (value is ushort us)
                SetUShort(us, index);
            else if (value is short s)
                SetShort(s, index);

            else if (value is uint ui)
                SetUInt(ui, index);
            else if (value is int i)
                SetInt(i, index);

            else if (value is ulong ul)
                SetULong(ul, index);
            else if (value is long l)
                SetLong(l, index);

            else if (value is Half h)
                SetHalf(h, index);
            else if (value is float f)
                SetFloat(f, index);
            else if (value is double d)
                SetDouble(d, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetBuffer(CLBuffer arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(CLBuffer).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLBuffer GetBuffer(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return arg.ArgObject as CLBuffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetSubBuffer(CLSubBuffer arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(CLSubBuffer).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLSubBuffer GetSubBuffer(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return arg.ArgObject as CLSubBuffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetImage(CLImage arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(CLImage2D).Name;

            cl_mem arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLImage GetImage(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return arg.ArgObject as CLImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetSampler(CLSampler arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(CLSampler).Name;

            cl_sampler arg_id = arg.Id;
            Error = CL.SetKernelArg(Id, index, arg_id).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLSampler GetSampler(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return arg.ArgObject as CLSampler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetDouble(double arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(double).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetDouble(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (double)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetFloat(float arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(float).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float GetFloat(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (float)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetHalf(Half arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(Half).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Half GetHalf(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (Half)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetLong(long arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(long).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public long GetLong(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (long)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetULong(ulong arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(ulong).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ulong GetULong(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (ulong)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetInt(int arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(int).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (int)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetUInt(uint arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(uint).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint GetUInt(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (uint)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetShort(short arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(short).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (short)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetUShort(ushort arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(ushort).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ushort GetUShort(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (ushort)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetSByte(sbyte arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(sbyte).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public sbyte GetSByte(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (sbyte)arg.ArgObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetByte(byte arg, uint index)
        {
            CheckIndex(index);

            var kernel_arg = GetArgument(index);
            kernel_arg.ArgObject = arg;
            kernel_arg.ArgType = typeof(byte).Name;

            Error = CL.SetKernelArg(Id, index, arg).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte GetByte(uint index)
        {
            CheckIndex(index);

            var arg = GetArgument(index);
            return (byte)arg.ArgObject;
        }
    }
}
