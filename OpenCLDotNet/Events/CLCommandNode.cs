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

        internal virtual void OnStart()
        {

        }

        internal virtual void OnFinish()
        {

        }

        internal abstract cl_event Run(CLCommand cmd);

    }
}
