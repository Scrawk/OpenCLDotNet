using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenCLDotNet.Core;
using OpenCLDotNet.Buffers;
using OpenCLDotNet.Events;
using OpenCLDotNet.Utility;
using OpenCLDotNet.Programs;

namespace OpenCLDotNetTest.Buffers
{
    [TestClass]
    public class CLImage2DTest
    {

        private const uint WIDTH = 100;

        private const uint HEIGHT = 100;

        private const uint SIZE = WIDTH * HEIGHT;

        private CLContext Context { get; set; }

        private CLCommandQueue Cmd { get; set; }

        private float[] Data { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new CLContext();
            Cmd = new CLCommandQueue(Context);

            Data = new float[SIZE];
            for (int i = 0; i < Data.Length; i++)
                Data[i] = i;
        }

        [TestMethod]
        public void CreateReadImageTest()
        {
            var image = CreateReadImage();

            Assert.IsTrue(image.IsValid);
            Assert.AreEqual(CL_MEM_DATA_TYPE.FLOAT, image.DataType);
            Assert.AreEqual(4u, image.DataSize);
            Assert.AreEqual(SIZE * 4, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.IsTrue(image.IsReadOnly);
            Assert.IsTrue(image.CanRead);
            Assert.IsFalse(image.IsWriteOnly);
            Assert.IsFalse(image.CanWrite);
        }

        [TestMethod]
        public void CreateWriteImageTest()
        {
            var image = CreateWriteImage();

            Assert.IsTrue(image.IsValid);
            Assert.AreEqual(CL_MEM_DATA_TYPE.FLOAT, image.DataType);
            Assert.AreEqual(4u, image.DataSize);
            Assert.AreEqual(SIZE * 4, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.IsFalse(image.IsReadOnly);
            Assert.IsFalse(image.CanRead);
            Assert.IsTrue(image.IsWriteOnly);
            Assert.IsTrue(image.CanWrite);
        }

        [TestMethod]
        public void ReadTest()
        {

        }

        [TestMethod]
        public void WriteTest()
        {

        }

        [TestMethod]
        public void CopyTest()
        {

        }

        [TestMethod]
        public void FillTest()
        {

        }

        private CLImage2D CreateReadImage()
        {
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.R;
            param.ChannelType = CL_CHANNEL_TYPE.FLOAT;
            param.DataType = CL_MEM_DATA_TYPE.FLOAT;
            param.DataLength = SIZE;
            param.Source = Data;

            return CLImage2D.CreateReadImage2D(Context, param);
        }

        private CLImage2D CreateWriteImage()
        {
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.R;
            param.ChannelType = CL_CHANNEL_TYPE.FLOAT;
            param.DataType = CL_MEM_DATA_TYPE.FLOAT;
            param.DataLength = SIZE;
            param.Source = null;

            return CLImage2D.CreateWriteImage2D(Context, param);
        }

    }
}
