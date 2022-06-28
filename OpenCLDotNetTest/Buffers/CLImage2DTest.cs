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

        private const uint CHANNELS = 4;

        private const uint SIZE = WIDTH * HEIGHT * CHANNELS;

        private const CL_DATA_TYPE DATA_TYPE = CL_DATA_TYPE.BYTE;

        private const uint DATA_SIZE = 1;

        private const uint BYTE_SIZE = SIZE * DATA_SIZE;

        private CLContext Context { get; set; }

        private CLCommand Cmd { get; set; }

        private byte[] Data { get; set; }

        private byte[] FillData { get; set; }

        private byte[] EmptyData { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new CLContext();
            Cmd = new CLCommand(Context);
            EmptyData = new byte[SIZE];
            FillData = new byte[SIZE];
            Data = new byte[SIZE];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = (byte)i;
            }

            for (int i = 0; i < Data.Length / 4; i++)
            {
                FillData[i * 4 + 0] = 64;
                FillData[i * 4 + 1] = 127;
                FillData[i * 4 + 2] = 191;
                FillData[i * 4 + 3] = 255;
            }

        }

        [TestMethod]
        public void CreateReadImageTest()
        {
            var image = CreateReadImage();

            Assert.IsTrue(image.IsValid);
            Assert.AreEqual(DATA_TYPE, image.DataType);
            Assert.AreEqual(DATA_SIZE, image.DataSize);
            Assert.AreEqual(BYTE_SIZE, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.AreEqual(CHANNELS, image.Channels);
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
            Assert.AreEqual(DATA_TYPE, image.DataType);
            Assert.AreEqual(DATA_SIZE, image.DataSize);
            Assert.AreEqual(BYTE_SIZE, image.ByteSize);
            Assert.AreEqual(SIZE, image.Length);
            Assert.AreEqual(WIDTH, image.Width);
            Assert.AreEqual(HEIGHT, image.Height);
            Assert.AreEqual(CHANNELS, image.Channels);
            Assert.IsFalse(image.IsReadOnly);
            Assert.IsFalse(image.CanRead);
            Assert.IsTrue(image.IsWriteOnly);
            Assert.IsTrue(image.CanWrite);
            Assert.AreEqual(WIDTH, image.Region.Size.x);
            Assert.AreEqual(HEIGHT, image.Region.Size.y);
        }

        [TestMethod]
        public void ReadTest()
        {
            var image = CreateReadImage();

            var data = new byte[SIZE];
            image.Read(Cmd, data);

            Assert.IsTrue(image.IsValid);
            Assert.IsFalse(image.HasError);
            CollectionAssert.AreEqual(Data, data);
            
        }

        [TestMethod]
        public void WriteTest()
        {
            var image = CreateWriteImage();

            var data = new byte[SIZE];
            image.Read(Cmd, data);

            //Validate that image is empty.
            CollectionAssert.AreEqual(EmptyData, data);

            //Write Data into image and read it back into data array.
            image.Write(Cmd, Data);
            image.Read(Cmd, data);

            Assert.IsTrue(image.IsValid);
            Assert.IsFalse(image.HasError);
            CollectionAssert.AreEqual(Data, data);
        }

        [TestMethod]
        public void CopyTest()
        {
            var image = CreateReadImage();

            var copy = image.Copy(Cmd);

            var data = new byte[SIZE];
            copy.Read(Cmd, data);

            Assert.IsTrue(copy.IsValid);
            Assert.IsFalse(copy.HasError);
            CollectionAssert.AreEqual(Data, data);
        }

        [TestMethod]
        public void FillTest()
        {
            var image = CreateEmptyImage();

            var color = new CLColorRGBA(0.25f, 0.5f, 0.75f, 1.0f);
            image.Fill(Cmd, color);

            var data = new byte[SIZE];
            image.Read(Cmd, data);

            Assert.IsTrue(image.IsValid);
            Assert.IsFalse(image.HasError);
            CollectionAssert.AreEqual(FillData, data);

            var float_array = new float[]
            {
                0.25f, 0.5f, 0.75f, 1.0f
            };

            image.Fill(Cmd, float_array, CL_DATA_TYPE.FLOAT);

            data = new byte[SIZE];
            image.Read(Cmd, data);

            Assert.IsTrue(image.IsValid);
            Assert.IsFalse(image.HasError);
            CollectionAssert.AreEqual(FillData, data);
        }

        private CLImage2D CreateReadImage()
        {
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.RGBA;
            param.ChannelType = CL_CHANNEL_TYPE.UNORM_INT8;
            param.DataType = CL_DATA_TYPE.BYTE;
            param.DataLength = SIZE;
            param.Source = Data;

            return CLImage2D.CreateReadImage2D(Context, param);
        }

        private CLImage2D CreateEmptyImage()
        {
            return CreateWriteImage();   
        }

        private CLImage2D CreateWriteImage()
        {
            var param = new CLImageParameters2D();
            param.Width = WIDTH;
            param.Height = HEIGHT;
            param.ChannelOrder = CL_CHANNEL_ORDER.RGBA;
            param.ChannelType = CL_CHANNEL_TYPE.UNORM_INT8;
            param.DataType = CL_DATA_TYPE.BYTE;
            param.DataLength = SIZE;
            param.Source = null;

            return CLImage2D.CreateWriteImage2D(Context, param);
        }

    }
}
