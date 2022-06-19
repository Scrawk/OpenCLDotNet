using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLSampler : CLObject
    {

        public CLSampler(CLContext context) 
            : this(context, CLSamplerProperties.Default)
        {

        }

        public CLSampler(CLContext context, CLSamplerProperties properties)
        {
            Create(context, properties);
        }

        public CLContext Context { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLSampler: Id={0}, ContextId={1}, Error={2}]",
                Id, Context.Id, Error);
        }

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

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid)
                return;

            var values = Enum.GetValues<CL_SAMPLER_INFO>();

            builder.AppendLine();
            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        public string GetInfo(CL_SAMPLER_INFO info)
        {
            if (!IsValid)
                return "UNKNOWN";

            var type = CL.GetReturnType(info);

            string str = CL_INFO_RETURN_TYPE.UNKNOWN.ToString();

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info);

                if(info == CL_SAMPLER_INFO.ADDRESSING_MODE)
                    str = ((CL_SAMPLER_ADDRESSING_MODE)i).ToString();
                else if (info == CL_SAMPLER_INFO.FILTER_MODE)
                    str = ((CL_SAMPLER_FILTER_MODE)i).ToString();
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
            else
            {
                str = "UNKNOWN";
            }

            return str;
        }

        private UInt64 GetInfoUInt64(CL_SAMPLER_INFO name)
        {
            Core.CL.GetSamplerInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info;
        }

        private cl_object GetInfoObject(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            cl_object info;
            CL.GetSamplerInfo(Id, name, size, out info);

            return info;
        }

        private bool GetInfoBool(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info > 0;
        }

        private float GetInfoFloat(CL_SAMPLER_INFO name)
        {
            CL.GetSamplerInfoSize(Id, name, out uint size);

            float info;
            CL.GetSamplerInfo(Id, name, size, out info);
            return info;
        }

        protected override void Release()
        {
            CL.ReleaseSampler(Id);
        }
    }
}
