using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace pl_lab3
{
    internal struct NodeParameters
    {
        public NodeParameters(int nodeId, Channel<Token> channelFrom, Channel<Token> channelTo)
        {
            NodeId = nodeId;
            ChannelFrom = channelFrom;
            ChannelTo = channelTo;
        }

        public int NodeId { get; private set; }
        public Channel<Token> ChannelFrom { get; private set; }
        public Channel<Token> ChannelTo { get; private set; }
    }
}
