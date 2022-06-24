using System;
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
    public abstract class CLMemObject : CLObject
    {

        protected static readonly string ERROR_SOURCE_DATA_IS_NULL = "SOURCE_DATA_IS_NULL";

        protected static readonly string ERROR_INVALID_SOURCE_SIZE = "INVALID_SOURCE_SIZE";

        protected static readonly string ERROR_INVALID_CHANNEL_ORDER_TYPE = "INVALID_CHANNEL_ORDER_TYPE";

        protected static readonly string ERROR_INVALID_DATA_TYPE = "INVALID_DATA_TYPE";

        protected static readonly string ERROR_CHANNEL_FORMAT_NOT_SUPPORTED = "CHANNEL_FORMAT_NOT_SUPPORTED";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLMemObject(CLContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        public CL_MEM_DATA_TYPE DataType { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public uint DataSize { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Length { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        internal abstract uint RowPitch { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint ByteSize => DataSize * Length;

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
        public bool IsReadOnly => CanRead && !CanWrite;

        /// <summary>
        /// 
        /// </summary>
        public bool IsWriteOnly => CanWrite && !CanRead;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rw"></param>
        /// <returns></returns>
        protected CL_MEM_FLAGS CreateFlags(CL_READ_WRITE rw)
        {
            CL_MEM_FLAGS flag = 0;

            switch (rw)
            {
                case CL_READ_WRITE.WRITE:
                    flag = CL_MEM_FLAGS.WRITE_ONLY;
                    //flag |= CL_MEM_FLAGS.HOST_WRITE_ONLY;
                    flag |= CL_MEM_FLAGS.ALLOC_HOST_PTR;
                    break;

                case CL_READ_WRITE.READ:
                    flag = CL_MEM_FLAGS.READ_ONLY;
                    flag |= CL_MEM_FLAGS.HOST_READ_ONLY;
                    flag |= CL_MEM_FLAGS.COPY_HOST_PTR;
                    break;
            }

            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid) return;

            builder.AppendLine("");
            builder.AppendLine("Flags: " + Flags);
            builder.AppendLine("MemType: " + MemType);
            builder.AppendLine("DataType: " + DataType);
            builder.AppendLine("DataSize: " + DataSize);
            builder.AppendLine("Length: " + Length);
            builder.AppendLine("ByteSize: " + ByteSize);
            builder.AppendLine("");

            var values = CL.GetValues<CL_MEM_INFO>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidObjectExeception"></exception>
        protected static void CheckCommand(CLCommandQueue cmd)
        {
            if (cmd == null)
                throw new NullReferenceException("Command is null.");

            if (!cmd.IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        /// <exception cref="InvalidDataTypeExeception"></exception>
        protected static void CheckData(CLMemObject obj, Array data, uint offset)
        {
            if (data == null)
                throw new NullReferenceException("Data is null.");

            if ((offset + data.Length) > obj.Length)
                throw new InvalidDataSizeExeception($"Data offset + length was {offset + data.Length} when offset + length <= {obj.Length} was expected.");

            if (obj.DataType != CL.TypeOf(data))
                throw new InvalidDataTypeExeception($"Data type is {CL.TypeOf(data)} when type {obj.DataType} was expected."); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        protected static void CheckOffset(CLMemObject obj, uint offset, uint size)
        {
            if ((offset + size) > obj.Length)
                throw new InvalidDataSizeExeception($"Offset + size was {offset + size} when offset + size <= {obj.Length} was expected.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="region"></param>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        protected static void CheckRegion(CLImage2D obj, CLRegion2t region)
        {
            if ((region.Origion.x + region.Size.x) > obj.Width)
                throw new InvalidDataSizeExeception($"Region origin.x + size.x was {region.Origion.x + region.Size.x} when origin.x + size.x <= {obj.Width} was expected.");

            if ((region.Origion.y + region.Size.y) > obj.Height)
                throw new InvalidDataSizeExeception($"Region origin.y + size.y was {region.Origion.y + region.Size.y} when origin.y + size.y <= {obj.Height} was expected.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="region"></param>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        protected static void CheckRegion(CLImage2D obj, CLRegion3t region)
        {
            if ((region.Origion.x + region.Size.x) > obj.Width)
                throw new InvalidDataSizeExeception($"Region origin.x + size.x was {region.Origion.x + region.Size.x} when origin.x + size.x <= {obj.Width} was expected.");

            if ((region.Origion.y + region.Size.y) > obj.Height)
                throw new InvalidDataSizeExeception($"Region origin.y + size.y was {region.Origion.y + region.Size.y} when origin.y + size.y <= {obj.Height} was expected.");

            //if ((region.Origion.z + region.Size.z) > obj.Depth)
            //    throw new InvalidDataSizeExeception($"Region origin.y + size.y was {region.Origion.z + region.Size.z} when origin.z + size.z <= {obj.Depth} was expected.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="data"></param>
        /// <param name="region"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        /// <exception cref="InvalidDataTypeExeception"></exception>
        protected static void CheckData(CLMemObject obj, Array data, CLRegion3t region)
        {
            if (data == null)
                throw new NullReferenceException("Data is null.");

            if ((region.Origion.x + region.Size.x) > (ulong)data.Length)
                throw new InvalidDataSizeExeception($"Data region.Origion.x + region.Size.x was {region.Origion.x + region.Size.x} when length <= obj.{obj.Length} was expected.");

            if ((region.Origion.y + region.Size.y) > (ulong)data.Length)
                throw new InvalidDataSizeExeception($"Data region.Origion.y + region.Size.y was {region.Origion.y + region.Size.y} when length <= {obj.Length} was expected.");

            if ((region.Origion.z + region.Size.z) > (ulong)data.Length)
                throw new InvalidDataSizeExeception($"Data region.Origion.z + region.Size.z was {region.Origion.z + region.Size.z} when length <= {obj.Length} was expected.");

            if (obj.DataType != CL.TypeOf(data))
                throw new InvalidDataTypeExeception($"Data type is {CL.TypeOf(data)} when type {obj.DataType} was expected."); ;
        }
    }
}
