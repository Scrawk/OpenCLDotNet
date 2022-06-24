using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Programs;

namespace OpenCLDotNetTest.Core
{
    [TestClass]
    public class CLDeviceTest
    {
        [TestMethod]
        public void Constructor()
        {
            var device = CreateDevice(CL_DEVICE_TYPE.GPU);

            Assert.IsTrue(device.IsValid);
        }

        [TestMethod]
        public void DeviceType()
        {
            var device = CreateDevice(CL_DEVICE_TYPE.GPU);

            Assert.AreEqual(CL_DEVICE_TYPE.GPU, device.DeviceType);
            Assert.IsTrue(device.IsGPU);
            Assert.IsFalse(device.IsCPU);
        }

        private CLPlatform CreatePlatform()
        {
            var ids = new List<cl_platform_id>();
            var error = CL.GetPlatformIDs(ids);

            Assert.IsTrue(error == CL_ERROR.SUCCESS);
            Assert.IsTrue(ids.Count > 0);

            return new CLPlatform(ids[0]);
        }

        private CLDevice CreateDevice(CL_DEVICE_TYPE type)
        {
            var platform = CreatePlatform();

            var ids = new List<cl_device_id>();
            var error = CL.GetDeviceIDs(platform.Id, type, ids);

            Assert.IsTrue(error == CL_ERROR.SUCCESS);
            Assert.IsTrue(ids.Count > 0);

            return new CLDevice(ids[0], platform);
        }

    }
}