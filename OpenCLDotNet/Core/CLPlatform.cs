using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public class CLPlatform : CLObject
    {

        public CLPlatform(cl_platform_id id)
        {
            Id = id;
            CreateDevices();
            GetInfo();
            GetExtensions();
        }

        public string Vendor { get; private set; }

        public string Name { get; private set; }

        public string Version { get; private set; }

        public string Profile { get; private set; }

        private List<string> Extensions { get; set; }

        public int NumDevices => Devices.Count;

        private List<CLDevice> Devices { get; set; }

        public override string ToString()
        {
            return String.Format("[CLPlatform: Id={0}, Vendor={1}, NumDevices={2}, Error={3}]", 
                Id, Vendor, NumDevices, Error);
        }

        public cl_device_id[] GetDeviceIds()
        {
            var ids = new cl_device_id[Devices.Count];

            for (int i = 0; i < Devices.Count; i++)
                ids[i] = Devices[i].Id;

            return ids;
        }

        public bool HasDevice(CL_DEVICE_TYPE type)
        {
            foreach (var device in Devices)
                if (type.HasFlag(device.Type))
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

        public bool HasExtension(string name)
        {
            GetExtensions();
            return Extensions.Contains(name);
        }

        public CL_ERROR UnloadComplier()
        {
            return CL.UnloadPlatformCompiler(Id);
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());

            if (!IsValid)
                return;

            builder.AppendLine();
            builder.AppendLine("Vendor: " + Vendor);
            builder.AppendLine("Name: " + Name);
            builder.AppendLine("Version: " + Version);
            builder.AppendLine("Profile: " + Profile);
            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);

            var values = Enum.GetValues<CL_PLATFORM_INFO>();

            foreach (var e in values)
            {
                if (e == CL_PLATFORM_INFO.VENDOR ||
                   e == CL_PLATFORM_INFO.NAME ||
                   e == CL_PLATFORM_INFO.VERSION ||
                   e == CL_PLATFORM_INFO.PROFILE ||
                   e == CL_PLATFORM_INFO.EXTENSIONS)
                    continue;

                builder.AppendLine(e + ": " + GetInfo(e));
            }

            if (NumDevices == 0)
                return;

            builder.AppendLine();
            builder.AppendLine("Devices.");
            builder.AppendLine();

            foreach (var device in Devices)
            {
                device.Print(builder);
            }
        }

        public string GetInfo(CL_PLATFORM_INFO info)
        {
            if (!IsValid)
                return "UNKNOWN";

            var type = CL.GetReturnType(info);

            string str = "";

            if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);
            else
                str = "UNKNOWN";

            return str;
        }

        private void  GetInfo()
        {
            Vendor = GetInfoString(CL_PLATFORM_INFO.VENDOR);
            Name = GetInfoString(CL_PLATFORM_INFO.NAME);
            Version = GetInfoString(CL_PLATFORM_INFO.VERSION);
            Profile = GetInfoString(CL_PLATFORM_INFO.PROFILE);
        }

        private string GetInfoString(CL_PLATFORM_INFO name)
        {
            string info;
            CL.GetPlatformInfo(Id, name, out info);

            if (string.IsNullOrWhiteSpace(info))
                return "";
            else
                return info;
        }

        private void CreateDevices()
        {
            Devices = new List<CLDevice>();

            if (!IsValid) return;

            var device_ids = new List<cl_device_id>();
            var error = CL.GetDeviceIDs(Id, CL_DEVICE_TYPE.ALL, device_ids);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            Devices.Capacity = device_ids.Count;
            foreach (var device_id in device_ids)
                Devices.Add(new CLDevice(device_id, this));

            SetErrorCodeToSuccess();
        }

        private void GetExtensions()
        {
            if (Extensions != null || !IsValid)
                return;

            string info;
            CL.GetPlatformInfo(Id, CL_PLATFORM_INFO.EXTENSIONS, out info);

            Extensions = new List<string>(info.Split(' '));
        }
    }
}
