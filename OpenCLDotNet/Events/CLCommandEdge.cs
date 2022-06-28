using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLCommandEdge
    {

        public CLCommandEdge(int from, int to)
        {
            From = from;
            To = to;
        }

        public int From { get; private set; }

        public int To { get; private set; }

    }
}
