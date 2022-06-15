using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public abstract class CLImage : CLMemObject
    {
        public CLImage(CLContext context)
        {
            Context = context;
        }

        public CLContext Context { get; protected set; }

        public override void Print(StringBuilder builder)
        {
            base.Print(builder);

            var values = Enum.GetValues<CL_IMAGE_INFO>();

            foreach (var e in values)
            {
                if (e == CL_IMAGE_INFO.WIDTH ||
                    e == CL_IMAGE_INFO.HEIGHT ||
                    e == CL_IMAGE_INFO.DEPTH)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        public string GetInfo(CL_IMAGE_INFO info)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = CL_INFO_RETURN_TYPE.UNKNOWN.ToString();

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

        private UInt64 GetInfoUInt64(CL_IMAGE_INFO name)
        {
            CL.GetImageInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetImageInfo(Id, name, size, out info);
            return info;
        }

        private string GetInfoFormat(CL_IMAGE_INFO name)
        {
            CL.GetImageInfoSize(Id, name, out uint size);

            CLImageFormat format;
            CL.GetImageInfo(Id, name, size, out format);

            string str = "{" + format.Order + ", " + format.Type + "}"; 

            return str;
        }
    }
}
