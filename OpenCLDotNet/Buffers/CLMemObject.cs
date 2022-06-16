using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public abstract class CLMemObject : CLObject
    {

        public cl_mem Id { get; protected set; }

        public CL_MEM_FLAGS Flags { get; protected set; }

        public CL_MEM_OBJECT_TYPE MemType { get; protected set; }

        public bool CanReadWrite => CanRead && CanWrite;

        public bool CanRead
        {
            get
            {
                return Flags.HasFlag(CL_MEM_FLAGS.READ_ONLY) ||
                       Flags.HasFlag(CL_MEM_FLAGS.READ_WRITE);
            }
        }

        public bool CanWrite
        {
            get
            {
                return Flags.HasFlag(CL_MEM_FLAGS.WRITE_ONLY) ||
                       Flags.HasFlag(CL_MEM_FLAGS.READ_WRITE);
            }
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            builder.AppendLine("FLAGS: " + Flags);

            var values = Enum.GetValues<CL_MEM_INFO>();

            foreach (var e in values)
            {
                if (e == CL_MEM_INFO.FLAGS)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        public string GetInfo(CL_MEM_INFO info)
        {
            var type = CL.GetReturnType(info);

            string str = CL_INFO_RETURN_TYPE.UNKNOWN.ToString();

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

        private UInt64 GetInfoUInt64(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        private UIntPtr GetInfoUIntPtr(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UIntPtr info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        private bool GetInfoBool(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            UInt64 info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info > 0;
        }

        private cl_object GetInfoObject(CL_MEM_INFO name)
        {
            Core.CL.GetMemObjectInfoSize(Id, name, out uint size);

            cl_object info;
            Core.CL.GetMemObjectInfo(Id, name, size, out info);
            return info;
        }

        protected override void Release()
        {
            Core.CL.ReleaseMemObject(Id);
        }
    }
}
