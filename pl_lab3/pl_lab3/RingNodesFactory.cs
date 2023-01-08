using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pl_lab3
{
    internal static class RingNodesFactory
    {
        public static List<Node> CreateNodes(int countNodes)
        {
            if(countNodes < 2)
                throw new Exception("Минимальное количество узлов - 2");

            var nodes = new List<Node>();

            #region Создание первой Node
            var channelFrom = Channel.CreateBounded<Token>(1);
            var channelTo = Channel.CreateBounded<Token>(1);
            var nodeParameters = new NodeParameters(0, channelFrom, channelTo);
            var node = new Node(nodeParameters);

            nodes.Add(node);
            #endregion Создание первой Node

            var tempChannelForLastNode = channelFrom;

            for (int i = 1; i < countNodes - 1; i++)
            {
                channelFrom = channelTo;
                channelTo = Channel.CreateBounded<Token>(1);
                nodeParameters = new NodeParameters(i, channelFrom, channelTo);
                node = new Node(nodeParameters);
                nodes.Add(node);
            }

            #region Создание последней Node
            channelFrom = channelTo;
            channelTo = tempChannelForLastNode;
            nodeParameters = new NodeParameters(countNodes-1, channelFrom, channelTo);
            node = new Node(nodeParameters);
            nodes.Add(node);
            #endregion Создание последней Node

            StartNodes(nodes);

            return nodes;
        }

        private static void StartNodes(List<Node> nodes) 
        {
            foreach (var node in nodes)
            {
                var thread = new Thread(node.Start);
                thread.Start();
            }
        }
    }
}
