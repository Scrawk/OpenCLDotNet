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
    public class CLSampler : CLObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLSampler(CLContext context) 
            : this(context, CLSamplerProperties.Default)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="normalizedCoords"></param>
        /// <param name="mode"></param>
        /// <param name="filter"></param>
        public CLSampler(CLContext context, bool normalizedCoords, CL_ADDRESSING_MODE mode, CL_FILTER_MODE filter)
    :       this(context, new CLSamplerProperties(normalizedCoords, mode, filter))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        public CLSampler(CLContext context, CLSamplerProperties properties)
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
            return String.Format("[CLSampler: Id={0}, ContextId={1}, Error={2}]",
                Id, Context.Id, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="properties"></param>
        private void Create(CLContext context, CLSamplerProperties properties)
        {
            ResetErrorCode();
            Context = context;

            CL_ERROR error;
            Id = CL.CreateSamplerWithProperties(context.Id, properties, out error);
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

            var values = CL.GetValues<CL_SAMPLER_INFO>();

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
        public string GetInfo(CL_SAMPLER_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info);

                if(info == CL_SAMPLER_INFO.ADDRESSING_MODE)
                    str = ((CL_ADDRESSING_MODE)i).ToString();
                else if (info == CL_SAMPLER_INFO.FILTER_MODE)
                    str = ((CL_FILTER_MODE)i).ToString();
                //else if (info == CL_SAMPLER_INFO.MIP_FILTER_MODE)
                //    str = ((CL_SAMPLER_FILTER_MODE)i).ToString();

            }
            else if (type == CL_INFO_RETURN_TYPE.UINT ||
                     type == CL_INFO_RETURN_TYPE.ULONG ||
                     type == CL_INFO_RETURN_TYPE.SIZET)
            {
                str = GetInfoUInt64(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.FLOAT)
            {
                str = GetInfoFloat(info).ToString();
            }
            else if (type == CL_INFO_RETURN_TYPE.BOOL)
            {
                str = GetInfoBool(info).ToString();
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
        /// <param name="name"></param>
        /// <returns></returns>
        private UInt64 GetInfoUInt64(CL_SAMPLER_INFO name)
        {
            Core.CL.GetSamplerInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private cl_object GetInfoObject(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetSamplerInfo(Id, name, size, out info);

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private unsafe string GetInfoObjectArray(CL_SAMPLER_INFO name)
        {
            int size_of = sizeof(cl_object);

            CL.GetSamplerInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetSamplerInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool GetInfoBool(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private float GetInfoFloat(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            float info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Release()
        {
            CL.ReleaseSampler(Id);
        }
    }
}
