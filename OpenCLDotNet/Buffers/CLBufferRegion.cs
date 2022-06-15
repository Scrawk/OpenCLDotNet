using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public readonly record struct CLBufferRegion
    {
        public readonly size_t Origion;

        public readonly size_t Size;

        public CLBufferRegion(size_t origion, size_t size)
        {
            Origion = origion;
            Size = size;
        }

        public override string ToString()
        {
            return String.Format("[CLBufferRegion: Origin={0}, Size={1}]",
                Origion, Size);
        }

    }
}
