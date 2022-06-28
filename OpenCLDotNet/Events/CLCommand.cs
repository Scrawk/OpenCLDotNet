using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class CLCommand : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly static cl_event[] EmptyEventArray = new cl_event[0];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLCommand(CLContext context)
        {
            Create(context);
            WaitEvents = new List<CLEvent>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        public CLCommand(CLContext context, CLCommandProperties properties)
        {
            Create(context, properties);
            WaitEvents = new List<CLEvent>();
        }

        /// <summary>
        /// 
        /// </summary>
        private CLContext Context { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CLEvent> WaitEvents { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLCommandQueue: Id={0}, ContextId={1}, WaitEvents={2}, Error={3}]",
                Id, Context.Id, WaitEvents.Count, Error);
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
        public cl_event[] GetWaitEvents()
        {
            int count = WaitEvents.Count;

            if (count == 0)
                return null;
            else
            {
                var array = new cl_event[count];
                for (int i = 0; i < count; i++)
                {
                    var status = WaitEvents[i].GetStatus();
                    if(status != CL_COMMAND_STATUS.COMPLETE)
                        array[i] = WaitEvents[i].Id;
                }
                    
                return array;
            }
        }

        public void ClearWaitEvents()
        {
            WaitEvents.Clear(); 
        }

        public void WaitOn(cl_event e)
        {
            WaitEvents.Add(new CLEvent(Context, e));
        }

        public bool IsComplete()
        {
            foreach(var e in WaitEvents)
                if(e.GetStatus() != CL_COMMAND_STATUS.COMPLETE)
                    return false;

            return true;
        }

        public CL_ERROR Finish()
        {
            return CL.Finish(Id);
        }

        public CL_ERROR Flush()
        {
            return CL.Flush(Id);
        }

        public CL_ERROR EnqueueBarrier()
        {
            var wait_list = GetWaitEvents();
            uint wait_list_size = CL.Length(wait_list);
            cl_event e;

            return CL.EnqueueBarrierWithWaitList(Id, wait_list_size, wait_list, out e);
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

            builder.AppendLine("");
            builder.AppendLine("WaitOn Event: ");
            builder.AppendLine("");

            foreach(var e in WaitEvents)
                e.Print(builder);

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
