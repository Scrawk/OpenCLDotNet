﻿using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Events;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CLImage : CLMemObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLImage(CLContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public uint Channels { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_ORDER ChannelOrder { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_CHANNEL_TYPE ChannelType { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public CLRegion3t Region { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="blocking"></param>
        public void Read(CLCommand cmd, Array dst, bool blocking = true)
        {
            Read(cmd, dst, Region, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="dst"></param>
        /// <param name="blocking"></param>
        public void Read(CLCommand cmd, Array dst, CLRegion3t region, bool blocking)
        {
            CheckCommand(cmd);
            CheckImage(this);
            CheckRegion(this, dst, region);

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueReadImage(cmd.Id, this, blocking, region, dst, ByteSize,
                wait_list_size, wait_list, out e);
                
            Error = error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="blocking"></param>
        public void Write(CLCommand cmd, Array src, bool blocking = true)
        {
            Write(cmd, src, Region, blocking);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="src"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Write(CLCommand cmd, Array src, CLRegion3t region, bool blocking)
        {
            CheckCommand(cmd);
            CheckImage(this);
            CheckRegion(this, src, region);

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueWriteImage(cmd.Id, this, blocking, region, src, ByteSize, 
                wait_list_size, wait_list, out e);

            Error = error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void Fill(CLCommand cmd, CLColorRGBA color)
        {
            Fill(cmd, color, Region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="region"></param>
        public void Fill(CLCommand cmd, CLColorRGBA color, CLRegion3t region)
        {
            CheckCommand(cmd);
            CheckImage(this);
            CheckRegion(this, region);

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueFillImage(cmd.Id, Id, color, region, 
                wait_list_size, wait_list, out e);

            Error = error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="type"></param>
        public void Fill(CLCommand cmd, Array color, CL_DATA_TYPE type)
        {
            Fill(cmd, color, type, Region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="type"></param>
        /// <param name="region"></param>
        public void Fill(CLCommand cmd, Array color, CL_DATA_TYPE type, CLRegion3t region)
        {
            CheckCommand(cmd);
            CheckImage(this);
            CheckRegion(this, region);

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueFillImage(cmd.Id, Id, color, type, region, 
                wait_list_size, wait_list, out e);

            Error = error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            base.Print(builder);

            if (!IsValid) return;

            var values = CL.GetValues<CL_IMAGE_INFO>();

            foreach (var e in values)
            {
                if (e == CL_IMAGE_INFO.WIDTH ||
                    e == CL_IMAGE_INFO.HEIGHT ||
                    e == CL_IMAGE_INFO.DEPTH)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_IMAGE_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.STRUCT)
            {
                if(info == CL_IMAGE_INFO.FORMAT)
                {
                    str = GetInfoFormat(info);
                }

            }
            else if (type == CL_INFO_RETURN_TYPE.UINT ||
                     type == CL_INFO_RETURN_TYPE.ULONG ||
                     type == CL_INFO_RETURN_TYPE.SIZET)
            {
                str = GetInfoUInt64(info).ToString();
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_IMAGE_INFO name)
        {
            Core.CL.GetImageInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetImageInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetInfoFormat(CL_IMAGE_INFO name)
        {
            Core.CL.GetImageInfoSize(Id, name, out uint size);

            CLImageFormat format;
            CL.GetImageInfo(Id, name, size, out format);

            string str = "{" + format.ChannelOrder + ", " + format.ChannelType + "}"; 

            return str;
        }
    }
}
