using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Programs;

namespace OpenCLDotNetTest.Util
{
    [TestClass]
    public class EnumUtilTest
    {
        [TestMethod]
        public void TypeOfTest()
        {

            Assert.AreEqual(CL_DATA_TYPE.DOUBLE, CL.TypeOf(new double[1]));
            Assert.AreEqual(CL_DATA_TYPE.FLOAT, CL.TypeOf(new float[1]));
            Assert.AreEqual(CL_DATA_TYPE.HALF, CL.TypeOf(new Half[1]));

            Assert.AreEqual(CL_DATA_TYPE.LONG, CL.TypeOf(new long[1]));
            Assert.AreEqual(CL_DATA_TYPE.ULONG, CL.TypeOf(new ulong[1]));

            Assert.AreEqual(CL_DATA_TYPE.INT, CL.TypeOf(new int[1]));
            Assert.AreEqual(CL_DATA_TYPE.UINT, CL.TypeOf(new uint[1]));

            Assert.AreEqual(CL_DATA_TYPE.SHORT, CL.TypeOf(new short[1]));
            Assert.AreEqual(CL_DATA_TYPE.USHORT, CL.TypeOf(new ushort[1]));

            Assert.AreEqual(CL_DATA_TYPE.BYTE, CL.TypeOf(new byte[1]));
            Assert.AreEqual(CL_DATA_TYPE.SBYTE, CL.TypeOf(new sbyte[1]));

        }

    }
}