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

        internal int Index { get; set; }

        public override string ToString()
        {
            return String.Format("[CLCommandNode: Index={0}]", Index);
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
