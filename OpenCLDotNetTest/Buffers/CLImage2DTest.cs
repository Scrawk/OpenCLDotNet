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

        private const uint WIDTH = 10;

        private const uint HEIGHT = 10;

        private const uint CHANNELS = 1;

        private const uint SIZE = WIDTH * HEIGHT * CHANNELS;

        private CLContext Context { get; set; }

        private CLCommandQueue Cmd { get; set; }

        private byte[] Data { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new CLContext();
            Cmd = new CLCommandQueue(Context);

            Data = new byte[SIZE];
            for (int i = 0; i < Data.Length; i++)
                Data[i] = (byte)i;
        }

        [TestMethod]
        public void CreateReadImageTest()
        {
            var image = CreateReadImage();

            Assert.IsTrue(image.IsValid);
            Assert.AreEqual(CL_DATA_TYPE.BYTE, image.DataType);
            Assert.AreEqual(CHANNELS, image.DataSize);
            Assert.AreEqual(SIZE * image.DataSize, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.IsTrue(image.IsReadOnly);
            Assert.IsTrue(image.CanRead);
            Assert.IsFalse(image.IsWriteOnly);
            Assert.IsFalse(image.CanWrite);
            Assert.AreEqual(WIDTH, image.Region.Size.x);
            Assert.AreEqual(HEIGHT, image.Region.Size.y);
        }

        [TestMethod]
        public void CreateWriteImageTest()
        {
            var image = CreateWriteImage();

            Assert.IsTrue(image.IsValid);
            Assert.AreEqual(CL_DATA_TYPE.BYTE, image.DataType);
            Assert.AreEqual(CHANNELS, image.DataSize);
            Assert.AreEqual(SIZE * image.DataSize, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.IsFalse(image.IsReadOnly);
            Assert.IsFalse(image.CanRead);
            Assert.IsTrue(image.IsWriteOnly);
            Assert.IsTrue(image.CanWrite);
            Assert.AreEqual(WIDTH, image.Region.Size.x);
            Assert.AreEqual(HEIGHT, image.Region.Size.y);
        }

        //[TestMethod]
        public void ReadTest()
        {
            /*
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.RGBA;
            param.ChannelType = CL_CHANNEL_TYPE.UNORM_INT8;
            param.DataType = CL_MEM_DATA_TYPE.BYTE;
            param.DataLength = SIZE;
            param.Source = Data;

            var flag = CL_MEM_FLAGS.READ_WRITE;
            flag |= CL_MEM_FLAGS.COPY_HOST_PTR;

            var image = new CLImage2D(Context, param, flag);

            image.Write(Cmd, Data, image.Region, true);

            Console.WriteLine(image.Error);

            var data = new byte[SIZE];
            image.Read(Cmd, data, image.Region, true);

            Console.WriteLine(image.Error);

            for(int i = 0; i < 100; i++)
                Console.WriteLine(data[i]);
            */
        }

        //[TestMethod]
        public void WriteTest()
        {

        }

        //[TestMethod]
        public void CopyTest()
        {

        }

        //[TestMethod]
        public void FillTest()
        {

        }

        private CLImage2D CreateReadImage()
        {
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.R;
            param.ChannelType = CL_CHANNEL_TYPE.UNORM_INT8;
            param.DataType = CL_DATA_TYPE.BYTE;
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
            param.ChannelType = CL_CHANNEL_TYPE.UNORM_INT8;
            param.DataType = CL_DATA_TYPE.BYTE;
            param.DataLength = SIZE;
            param.Source = null;

            return CLImage2D.CreateWriteImage2D(Context, param);
        }

    }
}
