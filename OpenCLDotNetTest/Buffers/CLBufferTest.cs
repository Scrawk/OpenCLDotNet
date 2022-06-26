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
    public class CLBufferTest
    {

        private const uint SIZE = 100;

        private const uint OFFSET = 10;

        private const uint SIZE_OFFSET = 80;

        private CLContext Context { get; set; }

        private CLCommandQueue Cmd { get; set; } 
        
        private int[] Data { get; set; }

        private int[] OffsetData { get; set; }

        private int[] EmptyData { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new CLContext();
            Cmd = new CLCommandQueue(Context);
            EmptyData = new int[SIZE];

            Data = new int[SIZE];
            for (int i = 0; i < Data.Length; i++)
                Data[i] = i;

            OffsetData = new int[SIZE_OFFSET];
            for (uint i = 0; i < OffsetData.Length; i++)
                OffsetData[i] = (int)(OFFSET + i);
        }

        [TestMethod]
        public void CreateReadBufferTest()
        {
            var buffer = CreateReadBuffer(SIZE);

            Assert.IsTrue(buffer.IsValid);
            Assert.AreEqual(CL_DATA_TYPE.INT, buffer.DataType);
            Assert.AreEqual(4u, buffer.DataSize);
            Assert.AreEqual(SIZE * 4, buffer.ByteSize);
            Assert.AreEqual(SIZE, buffer.Length);
            Assert.IsTrue(buffer.IsReadOnly);
            Assert.IsTrue(buffer.CanRead);
            Assert.IsFalse(buffer.IsWriteOnly);
            Assert.IsFalse(buffer.CanWrite);
        }

        [TestMethod]
        public void CreateWriteBufferTest()
        {
            var buffer = CreateWriteBuffer(SIZE);

            Assert.IsTrue(buffer.IsValid);
            Assert.AreEqual(CL_DATA_TYPE.INT, buffer.DataType);
            Assert.AreEqual(4u, buffer.DataSize);
            Assert.AreEqual(SIZE * 4, buffer.ByteSize);
            Assert.AreEqual(SIZE, buffer.Length);
            Assert.IsFalse(buffer.IsReadOnly);
            Assert.IsFalse(buffer.CanRead);
            Assert.IsTrue(buffer.IsWriteOnly);
            Assert.IsTrue(buffer.CanWrite);
        }

        [TestMethod]
        public void ReadTest()
        {
            var buffer = CreateReadBuffer(SIZE);

            var data = new int[SIZE];
            buffer.Read(Cmd, data, 0, true);

            CollectionAssert.AreEqual(Data, data);

            var offset_data = new int[SIZE_OFFSET]; 
            buffer.Read(Cmd, offset_data, OFFSET, true);

            CollectionAssert.AreEqual(OffsetData, offset_data);
        }

        [TestMethod]
        public void WriteTest()
        {
            var buffer = CreateWriteBuffer(SIZE);
            
            buffer.Write(Cmd, Data, 0, true);

            var data = new int[SIZE];
            buffer.Read(Cmd, data, 0, true);

            CollectionAssert.AreEqual(Data, data);

            buffer.Write(Cmd, EmptyData, 0, true);
            buffer.Write(Cmd, OffsetData, OFFSET, true);

            var offset_data = new int[SIZE_OFFSET];
            buffer.Read(Cmd, offset_data, OFFSET, true);

            CollectionAssert.AreEqual(OffsetData, offset_data);
        }

        [TestMethod]
        public void CopyTest()
        {
            var buffer1 = CreateWriteBuffer(SIZE);
            buffer1.Write(Cmd, Data, 0, true);

            var buffer2 = CreateWriteBuffer(SIZE);
            buffer1.Copy(Cmd, buffer2, 0, buffer2.Length);

            var data = new int[SIZE];
            buffer2.Read(Cmd, data, 0, true);

            CollectionAssert.AreEqual(Data, data);

            buffer2 = CreateWriteBuffer(SIZE_OFFSET);
            buffer1.Copy(Cmd, buffer2, OFFSET, buffer2.Length);

            var offset_data = new int[SIZE_OFFSET];
            buffer2.Read(Cmd, offset_data, 0, true);

            CollectionAssert.AreEqual(OffsetData, offset_data);

        }

        private CLBuffer CreateReadBuffer(uint size)
        {
            var type = CL_DATA_TYPE.INT;
            var buffer = CLBuffer.CreateReadBuffer(Context, Data, type);
            return buffer;
        }

        private CLBuffer CreateWriteBuffer(uint size)
        {
            var type = CL_DATA_TYPE.INT;
            var buffer = CLBuffer.CreateWriteBuffer(Context, type, size);
            return buffer;
        }

    }
}
