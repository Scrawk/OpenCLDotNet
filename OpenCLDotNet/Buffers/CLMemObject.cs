using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CLMemObject : CLObject
    {

        protected static readonly string ERROR_SOURCE_DATA_IS_NULL = "CLDOTNET_SOURCE_DATA_IS_NULL";

        protected static readonly string ERROR_INVALID_SOURCE_SIZE = "CLDOTNET_INVALID_SOURCE_SIZE";

        protected static readonly string ERROR_INVALID_CHANNEL_ORDER_TYPE = "CLDOTNET_INVALID_CHANNEL_ORDER_TYPE";

        protected static readonly string ERROR_INVALID_DATA_TYPE = "CLDOTNET_INVALID_DATA_TYPE";

        protected static readonly string ERROR_CHANNEL_FORMAT_NOT_SUPPORTED = "CLDOTNET_CHANNEL_FORMAT_NOT_SUPPORTED";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        public CLMemObject(CLContext context, CLMemData source)
        {
            Context = context;
            Source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        internal CLMemData Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CLContext Context { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_FLAGS Flags { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_OBJECT_TYPE MemType { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool CanReadWrite => CanRead && CanWrite;

        /// <summary>
        /// 
        /// </summary>
        public bool CanRead
        {
            get
            {
                return Flags.HasFlag(CL_MEM_FLAGS.READ_ONLY) ||
                       Flags.HasFlag(CL_MEM_FLAGS.READ_WRITE);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return Flags.HasFlag(CL_MEM_FLAGS.WRITE_ONLY) ||
                       Flags.HasFlag(CL_MEM_FLAGS.READ_WRITE);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid) return;

            builder.AppendLine("FLAGS: " + Flags);

            var values = Enum.GetValues<CL_MEM_INFO>();

            foreach (var e in values)
            {
                if (e == CL_MEM_INFO.FLAGS)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_MEM_INFO info)
        {
            if (!IsValid)
                return ERROR_UNKNOWN_TYPE;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info);

                if (info == CL_MEM_INFO.TYPE)
                    str = ((CL_MEM_OBJECT_TYPE)i).ToString();
                else if (info == CL_MEM_INFO.FLAGS)
                    str = ((CL_MEM_FLAGS)i).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.UINT ||
                     type == CL_INFO_RETURN_TYPE.ULONG ||
                     type == CL_INFO_RETURN_TYPE.SIZET)
            {
                str = GetInfoUInt64(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.BOOL)
            {
                str = GetInfoBool(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
            {
                str = GetInfoObject(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.VOID_PTR)
            {
                str = GetInfoUIntPtr(info).ToString();
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UIntPtr GetInfoUIntPtr(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UIntPtr info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool GetInfoBool(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            cl_object info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Release()
        {
            Core.CL.ReleaseMemObject(Id);
        }
    }
}
