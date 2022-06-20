using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public class CLEvent : CLObject
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLEvent(CLContext context)
        {
            Create(context);
        }

        /// <summary>
        /// 
        /// </summary>
        public CLContext Context { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLEvent: Id={0}, ContextId={1}, Error={2}]",
                Id, Context.Id, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void Create(CLContext context)
        {
            ResetErrorCode();
            Context = context;

            CL_ERROR error;
            Id = CL.CreateUserEvent(context.Id, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid)
                return;

            var values = Enum.GetValues<CL_EVENT_INFO>();

            builder.AppendLine();
            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_EVENT_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.UINT)
            {
                str = GetInfoUInt64(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
            {
                str = GetInfoObject(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
            {
                str = GetInfoObjectArray(info).ToString();
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_EVENT_INFO name)
        {
            Core.CL.GetEventInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetEventInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_EVENT_INFO name)
        {
            CL.GetEventInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetEventInfo(Id, name, size, out info);

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_EVENT_INFO name)
        {
            int size_of = sizeof(cl_object);

            CL.GetEventInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetEventInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

    }
}
