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
        [TestMethod]
        public void ConstructorTest()
        {
            var context = new CLContext(); 
            var data = new int[100];
            var buffer = new CLBuffer.CreateReadBuffer(context, data);
        }

        [TestMethod]
        public void CreateReadBufferTest()
        {

        }

        [TestMethod]
        public void Create WriteBufferTest()
        {

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
        
        private CLBuffer CreateReadBuffer(int size)
        {
            var context = new CLContext(); 
            var data = new int[size];
            
            for( int i = 0; i < data.Length; i++)
                data[i] = i; 
            
            var buffer = new CLBuffer.CreateReadBuffer(context, data);
             return buffer;
        }
        
        private CLBuffer CreateWritebuffer(int size)
        {
            var context = new CLContext(); 
             var type = CL_MEM_DATA_TYPE.INT;
            
            var buffer = new CLBuffer.CreateWriteBuffer(context, type, size);
             return buffer;
        }

    }
}
