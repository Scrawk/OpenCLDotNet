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
    public class CLContextTest
    {
        [TestMethod]
        public void ConstructorGPU()
        {
            var context = new CLContext(CL_DEVICE_TYPE.GPU);

            Assert.IsTrue(context.IsValid);
            Assert.IsTrue(context.HasDevice(CL_DEVICE_TYPE.GPU));
        }

        [TestMethod]
        public void ConstructorCPU()
        {
            var context = new CLContext(CL_DEVICE_TYPE.CPU);

            Assert.IsTrue(context.IsValid);
            Assert.IsTrue(context.HasDevice(CL_DEVICE_TYPE.CPU));
        }

        [TestMethod]
        public void NumDevices()
        {
            var context = new CLContext(CL_DEVICE_TYPE.ALL);

            Assert.IsTrue(context.NumDevices > 0);  
        }

        [TestMethod]
        public void NumPlatforms()
        {
            var context = new CLContext(CL_DEVICE_TYPE.ALL);

            Assert.IsTrue(context.NumPlatforms > 0);
        }

        [TestMethod]
        public void GetDeviceIds()
        {
            var context = new CLContext(CL_DEVICE_TYPE.ALL);

            int count = context.NumDevices;
            var devices = context.GetDeviceIds();

            Assert.AreEqual(count, devices.Length);
        }

        [TestMethod]
        public void GetDeviceID()
        {
            var context = new CLContext();
            var devices = context.GetDeviceIds();

            Assert.IsTrue(devices.Length > 0);

            Assert.AreEqual(devices[0], context.GetDeviceID(0));
        }

        [TestMethod]
        public void HasDevice()
        {
            var context = new CLContext(CL_DEVICE_TYPE.GPU);
            Assert.IsTrue(context.HasDevice(CL_DEVICE_TYPE.GPU));
        }

        [TestMethod]
        public void GetInfo()
        {

        }
    }
}