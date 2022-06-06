using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public class CLDevice : CLObject
    {
        public CLDevice(cl_device_id id)
        {
            Id = id;
        }

        public cl_device_id Id { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLDevice: Id={0}]", Id.Value);
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            var values = Enum.GetValues<CL_DEVICE_INFO>();

            foreach(var e in values)
            {
                var type = EnumUtil.GetReturnType(e);

                if(type == CL_DEVICE_INFO_RETURN_TYPE.SIZET_ARRAY)
                    builder.AppendLine(e + ": " + GetInfoSizetArray(e));

                else if (type == CL_DEVICE_INFO_RETURN_TYPE.UINT ||
                    type == CL_DEVICE_INFO_RETURN_TYPE.ULONG ||
                    type == CL_DEVICE_INFO_RETURN_TYPE.SIZET)
                    builder.AppendLine(e + ": " + GetInfoUInt64(e));

                else if (type == CL_DEVICE_INFO_RETURN_TYPE.BOOL)
                    builder.AppendLine(e + ": " + GetInfoBool(e));

                else if (type == CL_DEVICE_INFO_RETURN_TYPE.OBJECT)
                    builder.AppendLine(e + ": " + GetInfoObject(e));

                else if (type == CL_DEVICE_INFO_RETURN_TYPE.CHAR_ARRAY)
                    builder.AppendLine(e + ": " + GetInfoString(e));
            }

        }

        private string GetInfoString(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            char[] info = new char[size];
            CL.GetDeviceInfo(Id, name, size, info);
            return new string(info);
        }

        private cl_object GetInfoObject(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info;
        }

        private bool GetInfoBool(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info > 0;
        }

        private UInt64 GetInfoUInt64(CL_DEVICE_INFO name)
        {
            CL.GetDeviceInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetDeviceInfo(Id, name, size, out info);
            return info;
        }

        private string GetInfoSizetArray(CL_DEVICE_INFO name)
        {
            int size_of = 0;
            unsafe
            {
                size_of = sizeof(size_t);
            }

            CL.GetDeviceInfoSize(Id, name, out uint size);

            var info = new size_t[size / size_of];
            CL.GetDeviceInfo(Id, name, size, info);

            string str = "{";

            for(int i = 0; i < info.Length; i++)
            {
                str += info[i];
                if (i < info.Length - 1)
                    str += ", ";
            }

            str += "}";
            return str;
        }

    }
}
