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
        /// <param name="cmd"></param>
        /// <param name="region"></param>
        /// <param name="dst"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Read(CLCommandQueue cmd, CLRegion3t region, Array dst, bool blocking)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Image is not valid.");

            CheckCommand(cmd);
            CheckData(this, dst, region);

            cl_event e;
            var error = CL.EnqueueReadImage(cmd.Id, this, blocking, region, dst, 0, null, out e);
            Error = Error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="region"></param>
        /// <param name="src"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Write(CLCommandQueue cmd, CLRegion3t region, Array src, bool blocking)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Image is not valid.");

            CheckCommand(cmd);
            CheckData(this, src, region);

            cl_event e;
            var error = CL.EnqueueWriteImage(cmd.Id, this, blocking, region, src, 0, null, out e);
            Error = Error.ToString();
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
