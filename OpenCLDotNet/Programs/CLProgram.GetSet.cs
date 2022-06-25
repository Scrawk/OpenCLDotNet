using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Programs
{
    public partial class CLProgram : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public void SetBuffer(string kernel_name, CLBuffer buffer, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetBuffer(buffer, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLBuffer GetBuffer(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetBuffer(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="image"></param>
        /// <param name="index"></param>
        public void SetImage(string kernel_name, CLImage image, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetImage(image, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public CLImage GetImage(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetImage(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="sampler"></param>
        /// <param name="index"></param>
        public void SetSampler(string kernel_name, CLSampler sampler, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetSampler(sampler, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        public CLSampler GetSampler(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetSampler(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetDouble(string kernel_name, double arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetDouble(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetDouble(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetDouble(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetFloat(string kernel_name, float arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetFloat(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public float GetFloat(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetFloat(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetHalf(string kernel_name, Half arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetHalf(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Half GetHalf(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetHalf(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetLong(string kernel_name, long arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetLong(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public long GetLong(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetLong(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetULong(string kernel_name, ulong arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetULong(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ulong GetULong(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetULong(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetInt(string kernel_name, int arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetInt(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetInt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetUInt(string kernel_name, uint arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetUInt(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public uint GetUInt(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetUInt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetShort(string kernel_name, short arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetShort(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetShort(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetUShort(string kernel_name, ushort arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetUShort(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ushort GetUShort(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetUShort(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetSByte(string kernel_name, sbyte arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetSByte(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public sbyte GetSByte(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetSByte(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="arg"></param>
        /// <param name="index"></param>
        public void SetByte(string kernel_name, byte arg, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            kernel.SetByte(arg, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel_name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte GetByte(string kernel_name, uint index)
        {
            var kernel = FindKernel(kernel_name);
            CheckKernel(kernel, kernel_name, false);

            return kernel.GetByte(index);
        }
    }
}
