using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Events
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CLCommandGraph
    {

        private const int NOT_VISITED_TAG = 0;

        private const int IS_VISITED_TAG = 1;

        /// <summary>
        /// 
        /// </summary>
        public CLCommandGraph()
        {
            Context = new CLContext();
            Nodes = new List<CLCommandNode>();
            Edges = new List<List<CLCommandEdge>>();
            Events = new List<cl_event>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CLCommandGraph(CLContext context)
        {
            Context = context;
            Nodes = new List<CLCommandNode>();
            Edges = new List<List<CLCommandEdge>>();
            Events = new List<cl_event>();
        }

        /// <summary>
        /// 
        /// </summary>
        public CLContext Context { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private List<CLCommandNode> Nodes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<List<CLCommandEdge>> Edges { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<cl_event> Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(CLCommandNode node)
        {
            node.Index = Nodes.Count;
            Nodes.Add(node);
            Edges.Add(new List<CLCommandEdge>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="node"></param>
        public void AddNode(int index, CLCommandNode node)
        {
            node.Index = index;
            Nodes[index] = node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddEdge(int from, int to)
        {
            if (from == to)
                return;

            var edges = Edges[from];
            edges.Add(new CLCommandEdge(from, to)); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public void AllocateNodes(int size)
        {
            Nodes.Clear();
            Edges.Clear();

            for(int i = 0; i < size; i++)
            {
                Nodes.Add(null);
                Edges.Add(new List<CLCommandEdge>());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int IndexOf(CLCommandNode node)
        {
            return Nodes.IndexOf(node);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearEvents()
        {
            Events.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        public void RunSequential(bool profile = false)
        {
            Events.Clear();

            CLCommand cmd = CreateCommand(profile);

            for (int i = 0; i < Nodes.Count; i++)
            {
                var node = Nodes[i];
                if (node == null) continue;

                var e = node.Run(cmd);

                if (!e.IsNull)
                    Events.Add(e);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        public void Run(bool profile = false)
        {
            var order = TopologicalSort();

            Events.Clear();

            CLCommand cmd = CreateCommand(profile);

            for (int i = 0; i < order.Count; i++)
            {
                var node = order[i];
                if (node == null) continue;

                var e = node.Run(cmd);

                if(!e.IsNull)
                    Events.Add(e);
            }
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void PrintProfile(StringBuilder builder)
        {
            foreach (var e in Events)
            {
                var _event = new CLEvent(Context, e);
                _event.PrintProfile(builder);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private CLCommand CreateCommand(bool profile)
        {
            CLCommand cmd = null;

            if (profile)
            {
                var props = new CLCommandProperties();
                props.Properties = CL_COMMAND_QUEUE_POPERTIES.PROFILING_ENABLE;
                cmd = new CLCommand(Context, props);
            }
            else
            {
                cmd = new CLCommand(Context);
            }

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<CLCommandNode> DepthFirstOrder(int root)
        {
            TagNodes(NOT_VISITED_TAG);
            int count = Nodes.Count;

            var queue = new Stack<int>(count);
            queue.Push(root);

            Nodes[root].Tag = IS_VISITED_TAG;

            var ordering = new List<CLCommandNode>(count);

            while (queue.Count != 0)
            {
                int u = queue.Pop();
                ordering.Add(Nodes[u]);

                var edges = Edges[u];
                if (edges == null) continue;

                for (int i = 0; i < edges.Count; i++)
                {
                    int to = edges[i].To;

                    if (Nodes[to].Tag == IS_VISITED_TAG) continue;

                    queue.Push(to);
                    Nodes[to].Tag = IS_VISITED_TAG;
                }
            }

            return ordering;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<CLCommandNode> BreadthFirstOrder(int root)
        {
            TagNodes(NOT_VISITED_TAG);

            int count = Nodes.Count;

            var queue = new Queue<int>(count);
            queue.Enqueue(root);

            Nodes[root].Tag = IS_VISITED_TAG;

            var ordering = new List<CLCommandNode>(count);

            while (queue.Count != 0)
            {
                int u = queue.Dequeue();
                ordering.Add(Nodes[u]);

                var edges = Edges[u];
                if (edges == null) continue;

                for (int i = 0; i < edges.Count; i++)
                {
                    int to = edges[i].To;
                    if (Nodes[to].Tag == IS_VISITED_TAG) continue;

                    queue.Enqueue(to);
                    Nodes[to].Tag = IS_VISITED_TAG;
                }
            }

            return ordering;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CyclicGraphExeception"></exception>
        private List<CLCommandNode> TopologicalSort()
        {
            var list = new List<CLCommandNode>();
            var vertices = new LinkedList<CLCommandNode>();

            int edgeCount = Edges.Count;
            var edges = new List<CLCommandEdge>[edgeCount];

            for (int i = 0; i < edgeCount; i++)
            {
                if (Edges[i] == null) continue;
                if (Edges[i].Count == 0) continue;

                edges[i] = new List<CLCommandEdge>(Edges[i]);
            }

            for (int i = 0; i < Nodes.Count; i++)
            {
                int idegree = GetInverseDegree(edges, i);

                if (idegree == 0)
                    vertices.AddLast(Nodes[i]);
            }

            while (vertices.Count > 0)
            {
                var v = vertices.Last.Value;
                vertices.RemoveLast();

                list.Add(v);
                int i = v.Index;

                if (edges[i] == null || edges[i].Count == 0) continue;

                for (int j = 0; j < edges[i].Count; j++)
                {
                    int to = edges[i][j].To;

                    int idegree = GetInverseDegree(edges, to);
                    if (idegree == 1)
                    {
                        vertices.AddLast(Nodes[to]);
                    }
                }

                edges[i].Clear();
            }

            if (CountEdges(edges) > 0)
                throw new CyclicGraphExeception("Can not find a topological sort on a cyclic graph");
            else
                return list;
        }

        /// <summary>
        /// Find the number of vertices that go to this vertex.
        /// </summary>
        /// <param name="Edges">A list of the edges for each vertex.</param>
        /// <param name="i">The vertex index.</param>
        /// <returns></returns>
        private int GetInverseDegree(List<CLCommandEdge>[] Edges, int i)
        {
            int degree = 0;

            foreach (var edges in Edges)
            {
                if (edges == null || edges.Count == 0) continue;

                foreach (var edge in edges)
                {
                    if (edge.To == i) degree++;
                }
            }

            return degree;
        }

        /// <summary>
        /// Count the number of edges in the list of lists.
        /// </summary>
        /// <param name="Edges">A list of the edges for each vertex.</param>
        /// <returns></returns>
        private int CountEdges(List<CLCommandEdge>[] Edges)
        {
            int count = 0;

            foreach (var edges in Edges)
            {
                if (edges == null) continue;
                count += edges.Count;
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        private void TagNodes(int tag)
        {
            for(int i = 0; i < Nodes.Count; i++)
                Nodes[i].Tag = tag;
        }

    }
}
