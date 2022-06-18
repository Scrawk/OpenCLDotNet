using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLSampler : CLObject
    {
        public CLSampler(CLContext context, UInt64[] properties)
        {

            Create(context, properties);
        }

        public CLSampler(CLContext context)
        {
            var properties = new UInt64[]
            {
                (UInt64)CL_SAMPLER_PROPERTIES.NORMALIZED_COORDS,
                (UInt64)cl_bool.True,
                0,
                (UInt64)CL_SAMPLER_PROPERTIES.ADDRESSING_MODE,
                (UInt64)CL_SAMPLER_ADDRESSING_MODE.MIRRORED_REPEAT,
                0,
                (UInt64)CL_SAMPLER_PROPERTIES.FILTER_MODE,
                (UInt64)CL_SAMPLER_FILTER_MODE.LINEAR,
                0
            };

            Create(context, properties);
        }

        public cl_sampler Id { get; private set; }

        public CLContext Context { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLSampler: Id={0}, ContextId={1}, Error={2}]",
                Id.Value, Context.Id.Value, Error);
        }

        private void Create(CLContext context, UInt64[] properties)
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
        }

        public override void Print(StringBuilder builder)
        {
            base.Print(builder);

            var values = Enum.GetValues<CL_SAMPLER_INFO>();

            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }
        }

        public string GetInfo(CL_SAMPLER_INFO info)
        {
            var type = CL.GetReturnType(info);

            string str = CL_INFO_RETURN_TYPE.UNKNOWN.ToString();

            if (type == CL_INFO_RETURN_TYPE.ENUM)
            {
                var i = GetInfoUInt64(info);

                if(info == CL_SAMPLER_INFO.ADDRESSING_MODE)
                    str = ((CL_SAMPLER_ADDRESSING_MODE)i).ToString();
                else if (info == CL_SAMPLER_INFO.FILTER_MODE)
                    str = ((CL_SAMPLER_FILTER_MODE)i).ToString();
                else if (info == CL_SAMPLER_INFO.MIP_FILTER_MODE)
                    str = ((CL_SAMPLER_FILTER_MODE)i).ToString();

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
