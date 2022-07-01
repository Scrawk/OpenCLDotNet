using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{

    public enum CL_EVENT_TIMESPAN
    {
        NANOSECONDS,
        MICROSECONDS,
        MILLISECONDS,
        SECONDS
    }

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
        /// <param name="context"></param>
        /// <param name="id"></param>
        public CLEvent(CLContext context, cl_event id)
        {
            Create(context, id);
        }

        /// <summary>
        /// 
        /// </summary>
        public CL_COMMAND_TYPE CmdType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private CLContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLEvent: Id={0}, ContextId={1}, CmdType={2}, Error={3}]",
                Id, Context.Id, CmdType, Error);
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

            var cmd_type = GetInfoUInt64(CL_EVENT_INFO.COMMAND_TYPE);
            CmdType = (CL_COMMAND_TYPE)cmd_type;

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        private void Create(CLContext context, cl_event id)
        {
            ResetErrorCode();
            Id = id;
            Context = context;

            if (Id == UIntPtr.Zero)
            {
                Error = ERROR_INVALID_ID;
                return;
            }

            var cmd_type = GetInfoUInt64(CL_EVENT_INFO.COMMAND_TYPE);
            CmdType = (CL_COMMAND_TYPE)cmd_type;

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CL_COMMAND_STATUS GetStatus()
        {
            var cmd_status = GetInfoUInt64(CL_EVENT_INFO.COMMAND_EXECUTION_STATUS);
            return (CL_COMMAND_STATUS)cmd_status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time_span"></param>
        /// <returns></returns>
        public double GetRunTime(CL_EVENT_TIMESPAN time_span)
        {
            var start = GetInfoUInt64(CL_PROFILING_INFO.START);
            var end = GetInfoUInt64(CL_PROFILING_INFO.END);
            double nanoseconds = end - start;

            return ToTimeSpan(time_span, nanoseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time_span"></param>
        /// <returns></returns>
        public double GetQueuedTime(CL_EVENT_TIMESPAN time_span)
        {
            var start = GetInfoUInt64(CL_PROFILING_INFO.QUEUED);
            var end = GetInfoUInt64(CL_PROFILING_INFO.START);
            double nanoseconds = end - start;

            return ToTimeSpan(time_span, nanoseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time_span"></param>
        /// <param name="nanoseconds"></param>
        /// <returns></returns>
        private double ToTimeSpan(CL_EVENT_TIMESPAN time_span, double nanoseconds)
        {
            switch (time_span)
            {
                case CL_EVENT_TIMESPAN.NANOSECONDS:
                    return nanoseconds;

                case CL_EVENT_TIMESPAN.MICROSECONDS:
                    return nanoseconds * 1e-3;

                case CL_EVENT_TIMESPAN.MILLISECONDS:
                    return nanoseconds * 1e-6;

                case CL_EVENT_TIMESPAN.SECONDS:
                    return nanoseconds * 1e-9;

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void PrintProfile(StringBuilder builder)
        {
            builder.AppendLine(ToString());
            builder.AppendLine("Status: " + GetStatus());

            var e = GetInfoUInt64(CL_EVENT_INFO.COMMAND_TYPE);
            var str = ((CL_COMMAND_TYPE)e).ToString();
            var span = CL_EVENT_TIMESPAN.MILLISECONDS;
            var format = "F4";

            builder.AppendLine("Type: " + str);
            builder.AppendLine("Queued Time: " + GetQueuedTime(span).ToString(format) + "ms.");
            builder.AppendLine("Run Time: " + GetRunTime(span).ToString(format) + "ms.");
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

            /*
            builder.AppendLine();
            builder.AppendLine("Event Profiling Info:");
            builder.AppendLine();

            var profiling_values = CL.GetValues<CL_PROFILING_INFO>();

            foreach (var e in profiling_values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }
            */

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
                    str = ((CL_COMMAND_STATUS)i).ToString();
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
