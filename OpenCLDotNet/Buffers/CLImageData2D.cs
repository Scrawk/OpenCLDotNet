using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public class CLImageData2D
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
        internal uint Channels;

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
        public CL_MEM_FLAGS Flags;

        /// <summary>
        /// 
        /// </summary>
        internal Array Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string source_or_null = Source == null ? "NULL" : Source.ToString();

            return String.Format("[CLImageData: Width={0}, Height={1}, Order={2}, Type={3}, Source={4}]",
                Width, Height, ChannelOrder, ChannelType, source_or_null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<CLImageFormat> GetSupportedImageFormats(cl_context context)
        {
            var formats = new List<CLImageFormat>();
            var key = new CLImageFormatKey(context, Flags, CL_MEM_OBJECT_TYPE.IMAGE2D);

            CL.GetSupportedImageFormats(key, formats);
            return formats;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValidSize()
        {
            if(Source == null)
                return false;
            else
                return Width * Height * Channels == Source.Length;
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
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ImageFormatIsSupported(cl_context context, CLImageFormat format, out CL_ERROR error)
        {
            var key = new CLImageFormatKey(context, Flags, CL_MEM_OBJECT_TYPE.IMAGE2D);
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
            throw new NotImplementedException("fix row pitch");

            Channels = CL.GetNumChannels(ChannelOrder);

            uint width = Width;
            uint height = Height;
            uint row_pitch = (uint)(Channels * 4 * Source.Length);
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
