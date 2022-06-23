using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public class CLCommandQueue : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLCommandQueue(CLContext context)
        {
            Create(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        public CLCommandQueue(CLContext context, CLCommandQueueProperties properties)
        {
            Create(context, properties);
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
            return String.Format("[CLCommandQueue: Id={0}, ContextId={1}, Error={2}]",
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
        private void Create(CLContext context, CLCommandQueueProperties properties)
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

            SetErrorCodeToSuccess();
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
