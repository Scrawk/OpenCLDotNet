﻿using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public class CLCommand : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLCommand(CLContext context)
        {
            Create(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        public CLCommand(CLContext context, CLCommandProperties properties)
        {
            Create(context, properties);
        }

        /// <summary>
        /// 
        /// </summary>
        private CLContext Context { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        private CLEvent Event { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public bool IsComplete => GetStatus() == CL_COMMAND_STATUS.COMPLETE;

        /// <summary>
        /// 
        /// </summary>
        public bool HasEvent => Event != null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var event_id = HasEvent ? Event.Id : UIntPtr.Zero;

            return String.Format("[CLCommandQueue: Id={0}, ContextId={1}, EventId={2}, Status={3}, Error={4}]",
                Id, Context.Id, event_id, GetStatus(), Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void Create(CLContext context)
        {
            ResetErrorCode();
            Context = context;

            if (context.NumDevices <= 0)
            {
                Error = ERROR_NO_DEVICES_FOUND;
                return;
            }

            var device = context.GetDeviceID(0);

            CL_ERROR error;
            Id = CL.CreateCommandQueue(context.Id, device, out error);
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
        /// <param name="context"></param>
        /// <param name="properties"></param>
        private void Create(CLContext context, CLCommandProperties properties)
        {
            ResetErrorCode();
            Context = context;

            if(context.NumDevices <= 0)
            {
                Error = ERROR_NO_DEVICES_FOUND;
                return;
            }

            var device = context.GetDeviceID(0);

            CL_ERROR error;
            Id = CL.CreateCommandQueueWithProperties(context.Id, device, properties, out error);
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
        /// <returns></returns>
        public CL_COMMAND_STATUS GetStatus()
        {
            if (!HasEvent)
                return CL_COMMAND_STATUS.COMPLETE;
            else
                return Event.GetStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event_id"></param>
        public void SetEvent(cl_event event_id)
        {
            if (event_id.Value == UIntPtr.Zero)
                throw new ArgumentException("Event id is 0.");

            Event = new CLEvent(Context, event_id);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            Event = null;   
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

            var values = CL.GetValues<CL_COMMAND_QUEUE_INFO>();

            builder.AppendLine();
            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }

            if(HasEvent)
            {
                builder.AppendLine("");
                builder.AppendLine("Event: ");
                builder.AppendLine("");

                Event.Print(builder);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_COMMAND_QUEUE_INFO info)
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
        private UInt64 GetInfoUInt64(CL_COMMAND_QUEUE_INFO name)
        {
            Core.CL.GetCommandQueueInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetCommandQueueInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_COMMAND_QUEUE_INFO name)
        {
            CL.GetCommandQueueInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetCommandQueueInfo(Id, name, size, out info);

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_COMMAND_QUEUE_INFO name)
        {
            int size_of = sizeof(cl_object);

            CL.GetCommandQueueInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetCommandQueueInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

    }
}