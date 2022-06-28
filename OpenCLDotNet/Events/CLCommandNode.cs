using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public abstract class CLCommandNode
    {

        public CLCommandNode()
        {

        }

        internal abstract void Run(CLCommand cmd);

    }
}
