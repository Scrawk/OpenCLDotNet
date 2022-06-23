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

            builder.AppendLine();
            builder.AppendLine("Event Info:");
            builder.AppendLine();

            var event_values = CL.GetValues<CL_EVENT_INFO>();

            foreach (var e in event_values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }

            builder.AppendLine();
            builder.AppendLine("Event Profiling Info:");
            builder.AppendLine();

            var profiling_values = CL.GetValues<CL_PROFILING_INFO>();

            foreach (var e in profiling_values)
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

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info);

                if (info == CL_EVENT_INFO.COMMAND_TYPE)
                    str = ((CL_COMMAND_TYPE)i).ToString();
                else if(info == CL_EVENT_INFO.COMMAND_EXECUTION_STATUS)
                    str = ((CL_COMMAND_EXECUTION_STATUS)i).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.UINT)
            {
                str = GetInfoUInt64(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.OBJECT)
            {
                str = GetInfoObject(info).ToString();
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_PROFILING_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.ULONG)
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
        private UInt64 GetInfoUInt64(CL_PROFILING_INFO name)
        {
            Core.CL.GetEventProfilingInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetEventProfilingInfo(Id, name, size, out info);
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

    }
}
