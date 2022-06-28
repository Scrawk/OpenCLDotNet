using System;
using System.Collections.Generic;
using System.Diagnostics;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    public sealed class CLCommandGraph
    {

        public CLCommandGraph()
        {
            Context = new CLContext();
            Nodes = new List<CLCommandNode>();
            Edges = new List<CLCommandEdge>();
        }

        public CLCommandGraph(CLContext context)
        {
            Context = context;
            Nodes = new List<CLCommandNode>();
            Edges = new List<CLCommandEdge>();
        }

        public CLContext Context { get; private set; }

        private List<CLCommandNode> Nodes { get; set; }

        private List<CLCommandEdge> Edges { get; set; }

        public void AddNode(CLCommandNode node)
        {
            Nodes.Add(node);    
        }

        public void AddEdge(int from, int to)
        {
            Edges.Add(new CLCommandEdge(from, to)); 
        }

        public void Run()
        {
            //var props = new CLCommandProperties();
            //props.Properties = CL_COMMAND_QUEUE_POPERTIES.OUT_OF_ORDER_EXEC_MODE_ENABLE;

            var cmd = new CLCommand(Context);

            for(int i = 0; i < Nodes.Count; i++)
            {
                var node = Nodes[i];    
                var e = node.Run(cmd);

                //cmd.ClearWaitEvents();

                //if(!e.IsNull)
                //    cmd.WaitOn(e);
            }
        }

    }
}
