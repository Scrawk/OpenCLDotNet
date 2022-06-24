﻿using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public class CLImageParameters2D
    {

        /// <summary>
        /// 
        /// </summary>
        public uint Width;

        /// <summary>
        /// 
        /// </summary>
        public uint Height;

        /// <summary>
        /// 
        /// </summary>
        internal uint Channels => CL.GetNumChannels(ChannelOrder);

        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_ORDER ChannelOrder;

        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_TYPE ChannelType;

        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_DATA_TYPE DataType;

        /// <summary>
        /// 
        /// </summary>
        public uint DataLength;

        /// <summary>
        /// 
        /// </summary>
        public uint SourceLength
        {
            get
            {
                if (Source != null)
                    return (uint)Source.Length;
                else
                    return DataLength;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Array Source;

        /// <summary>
        /// 
        /// </summary>
        public CLImageParameters2D()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public CLImageParameters2D(CLImage2D image)
        {
            Width = image.Width;
            Height = image.Height;
            ChannelOrder = image.ChannelOrder;
            ChannelType = image.ChannelType;
            DataType = image.DataType;
            DataLength = image.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLImageParameters2D: Width={0}, Height={1}, ChannelOrder={2}, ChannelType={3}, DataType={4}, Length={5}]",
                Width, Height, ChannelOrder, ChannelType, DataType, SourceLength);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public List<CLImageFormat> GetSupportedImageFormats(cl_context context, CL_MEM_FLAGS flags)
        {
            var formats = new List<CLImageFormat>();
            var key = new CLImageFormatKey(context, flags, CL_MEM_OBJECT_TYPE.IMAGE2D);

            CL.GetSupportedImageFormats(key, formats);
            return formats;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValidSize()
        {
            return Width * Height * Channels  == SourceLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValidChannel()
        {
            return CL.IsValidChannelType(ChannelOrder, ChannelType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool IsValidArrayData()
        {
            if (Source == null)
                throw new NullReferenceException("Source is null");

            return CL.IsValidArrayData(ChannelType, Source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValidDataType()
        {
            return CL.IsValidDataType(ChannelType, DataType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="flags"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ImageFormatIsSupported(cl_context context, CLImageFormat format, CL_MEM_FLAGS flags, out CL_ERROR error)
        {
            var key = new CLImageFormatKey(context, flags, CL_MEM_OBJECT_TYPE.IMAGE2D);
            return CL.ImageFormatIsSupported(key, format, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CLImageFormat CreateImageFormat()
        {
            var format = new CLImageFormat();
            format.ChannelOrder = ChannelOrder;
            format.ChannelType = ChannelType;

            return format;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CLImageDescription CreateImageDescription()
        {

            var type = CL.TypeOf(Source);
            uint elementSize = CL.SizeOf(type);
            uint width = Width;
            uint height = Height;
            uint row_pitch = (uint)(Channels * elementSize * Width);
            uint slice_pitch = 0; // row_pitch * Height;

            var des = new CLImageDescription();
            des.MemType = CL_MEM_OBJECT_TYPE.IMAGE2D;
            des.Width = width;
            des.Height = height;
            des.Depth = 0;
            des.ArraySize = 0;
            des.RowPitch = row_pitch;
            des.SlicePitch = slice_pitch;
            des.NumMipLevels = 0;
            des.NumSamples = 0;
            //des.Buffer = 0;

            return des;
        }

    }
}
