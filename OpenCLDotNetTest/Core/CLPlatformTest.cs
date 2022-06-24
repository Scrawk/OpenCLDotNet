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
    public class CLPlatformTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var platform = CreatePlatform();
            Assert.IsTrue(platform.IsValid);
        }

        [TestMethod]
        public void NumDevicesTest()
        {
            var platform = CreatePlatform();

            Assert.IsTrue(platform.NumDevices > 0);
        }

        [TestMethod]
        public void GetDeviceID()
        {
            var platform = CreatePlatform();

            int count = platform.NumDevices;
            var devices = platform.GetDeviceIds();

            Assert.AreEqual(count, devices.Length);
        }

        private CLPlatform CreatePlatform()
        {
            var ids = new List<cl_platform_id>();
            var error = CL.GetPlatformIDs(ids);

            Assert.IsTrue(error == CL_ERROR.SUCCESS);
            Assert.IsTrue(ids.Count > 0);

            return new CLPlatform(ids[0]);
        }

    }
}