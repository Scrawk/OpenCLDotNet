using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public class CLContext : CLObject
    {
        public CLContext() : this(CL_DEVICE_TYPE.GPU)
        {

        }

        public CLContext(CL_DEVICE_TYPE device_type)
        {
            var platfrom_ids = new List<cl_platform_id>();
            CL.GetPlatformIDs(platfrom_ids);

            if (platfrom_ids.Count == 0)
                return;

            Platform = new CLPlatform(platfrom_ids[0]);
    
            var device_ids = new List<cl_device_id>();
            CL.GetDeviceIDs(Platform.Id, device_type, device_ids);

            CreateContext(device_ids);
            CreateDevices(device_ids);
        }

        public CLContext(CLPlatform platform, List<cl_device_id> device_ids)
        {
            Platform = platform;
            CreateContext(device_ids);
            CreateDevices(device_ids);
        }

        public cl_context Id { get; private set; }

        private CLPlatform Platform { get; set; }

        public int NumDevices => Devices.Count;

        private List<CLDevice> Devices { get; set; }

        public override string ToString()
        {
            return String.Format("[CLContext: Id={0}, PlatformID={1}, Devices={2}]", 
                Id.Value, Platform.Id.Value, NumDevices);
        }

        public cl_device_id[] GetDeviceIds()
        {
            var ids = new cl_device_id[Devices.Count];
            for(int i = 0; i < Devices.Count; i++)
                ids[i] = Devices[i].Id;

            return ids;
        }

        public bool HasDevice(CL_DEVICE_TYPE type)
        {
            foreach(var device in Devices)
                if(device.Type == type)
                    return true;

            return false;
        }

        public bool DeviceSupportsImages(int index)
        {
            return Devices[index].SupportsImages;
        }

        public bool SupportsImages()
        {
            foreach (var device in Devices)
                if (!device.SupportsImages)
                    return false;

            return true;    
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());
            var values = Enum.GetValues<CL_CONTEXT_INFO>();

            foreach (var e in values)
            {
                builder.AppendLine(e + ": " + GetInfo(e));
            }

            builder.AppendLine();
            Platform.Print(builder);

            foreach(var device in Devices)
            {
                builder.AppendLine();
                device.Print(builder);
            }
                

        }

        public string GetInfo(CL_CONTEXT_INFO info)
        {
            var type = EnumUtil.GetReturnType(info);

            string str = "";

            if (type == CL_INFO_RETURN_TYPE.UINT)
                str = GetInfoUInt64(info).ToString();
            else if (type == CL_INFO_RETURN_TYPE.OBJECT_ARRAY)
                str = GetInfoObjectArray(info);
            else
                str = "Unknown";

            return str;
        }

        private UInt64 GetInfoUInt64(CL_CONTEXT_INFO name)
        {
            CL.GetContextInfoSize(Id, name, out uint size);

            UInt64 info;
            CL.GetContextInfo(Id, name, size, out info);
            return info;
        }

        private string GetInfoObjectArray(CL_CONTEXT_INFO name)
        {
            int size_of = 0;
            unsafe
            {
                size_of = sizeof(cl_object);
            }

            CL.GetContextInfoSize(Id, name, out uint size);

            var info = new cl_object[size / size_of];
            CL.GetContextInfo(Id, name, size, info);

            string str = $"[cl_object_array: Count={info.Length}]";

            return str;
        }

        private void CreateContext(IList<cl_device_id> devices)
        {
            Id = CL.CreateContext(
                Platform.Id,
                (uint)devices.Count,
                devices.ToArray());
        }

        private void CreateDevices(IList<cl_device_id> devices)
        {
            Devices = new List<CLDevice>(devices.Count);
            foreach (var device_id in devices)
                Devices.Add(new CLDevice(device_id, Platform));
        }

        protected override void Release()
        {
            CL.ReleaseContext(Id);
        }

    }
}
