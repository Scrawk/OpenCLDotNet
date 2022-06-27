using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CLPlatform : CLObject
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="device_type"></param>
        public CLPlatform(cl_platform_id id, CL_DEVICE_TYPE device_type)
        {
            Id = id;
            CreateDevices(device_type);
            GetInfo();
            GetExtensions();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Vendor { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Profile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<string> Extensions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NumDevices => Devices.Count;

        /// <summary>
        /// 
        /// </summary>
        private CLDevice Device { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CLDevice> Devices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[CLPlatform: Id={0}, Vendor={1}, DeviceId={2}, NumDevices={3}, Error={4}]", 
                Id, Vendor, Device.Id, NumDevices, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public cl_device_id GetDeviceID(int index)
        {
            return Devices[index].Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public cl_device_id[] GetDeviceIds()
        {
            var ids = new cl_device_id[Devices.Count];

            for (int i = 0; i < Devices.Count; i++)
                ids[i] = Devices[i].Id;

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasDevice(CL_DEVICE_TYPE type)
        {
            foreach (var device in Devices)
                if (type.HasFlag(device.DeviceType))
                    return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeviceSupportsImages(int index)
        {
            return Devices[index].SupportsImages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SupportsImages()
        {
            foreach (var device in Devices)
                if (!device.SupportsImages)
                    return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasExtension(string name)
        {
            GetExtensions();
            return Extensions.Contains(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CL_ERROR UnloadComplier()
        {
            return CL.UnloadPlatformCompiler(Id);
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
            builder.AppendLine("Vendor: " + Vendor);
            builder.AppendLine("Name: " + Name);
            builder.AppendLine("Version: " + Version);
            builder.AppendLine("Profile: " + Profile);
            builder.AppendLine("Extensions: ");

            GetExtensions();
            for (int i = 0; i < Extensions.Count; i++)
                builder.AppendLine(Extensions[i]);

            var values = CL.GetValues<CL_PLATFORM_INFO>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetInfo(CL_PLATFORM_INFO info)
        {
            if (!IsValid)
                return ERROR_INVALID_OBJECT;

            var type = CL.GetReturnType(info);

            string str = ERROR_UNKNOWN_TYPE;

            if (type == CL_INFO_RETURN_TYPE.CHAR_ARRAY)
                str = GetInfoString(info);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        private void  GetInfo()
        {
            Vendor = GetInfoString(CL_PLATFORM_INFO.VENDOR);
            Name = GetInfoString(CL_PLATFORM_INFO.NAME);
            Version = GetInfoString(CL_PLATFORM_INFO.VERSION);
            Profile = GetInfoString(CL_PLATFORM_INFO.PROFILE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetInfoString(CL_PLATFORM_INFO name)
        {
            string info;
            CL.GetPlatformInfo(Id, name, out info);

            if (string.IsNullOrWhiteSpace(info))
                return "";
            else
                return info;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateDevices(CL_DEVICE_TYPE device_type)
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

            Device = null;
            Devices.Capacity = device_ids.Count;

            foreach (var device_id in device_ids)
            {
                var device = new CLDevice(device_id, this);

                Devices.Add(device);

                if(Device == null && device.DeviceType == device_type)
                    Device = device;
            }

            if (Device == null && Devices.Count > 0)
                Device = Devices[0];

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
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
