using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace pl_lab3
{
    internal class Node
    {
        private NodeParameters _nodeParameters;

        private ManualResetEvent _eventStop = new (true);

        public Node(NodeParameters nodeParameters)
        {
            _nodeParameters = nodeParameters; 
        }

        public async void Start()
        {
            var events = new WaitHandle[1];
            events[0] = _eventStop;
            while (WaitHandle.WaitAny(events) == 0)
            {
                await Wait();
                var token = await GetTokenFromChannel();

                await Handle(token);
            }
        }

        private async Task Wait()
        {
            await _nodeParameters.ChannelFrom.Reader.WaitToReadAsync();
        }

        private async Task<Token> GetTokenFromChannel()
        {
            var token = await _nodeParameters.ChannelFrom.Reader.ReadAsync();

            return token;
        }

        private async Task Handle(Token token) 
        {
            Console.WriteLine($"Узел {_nodeParameters.NodeId} получил токен");

            if (CheckNodeIsReceiver(token))
            {
                Console.WriteLine($"Токен дошёл до получателя");
                Console.WriteLine($"Полученное сообщение: {token.Data}");
            }
            else
            {
                await Send(token);
            }
        }

        private bool CheckNodeIsReceiver(Token token)
        {
            return token.ReceiverId == _nodeParameters.NodeId;
        }

        public async Task Send(Token token)
        {
            if (token.Ttl > 0)
            {
                Console.WriteLine($"Узел {_nodeParameters.NodeId} отправил токен");
                token.Ttl -= 1;
                Console.WriteLine($"Ttl = {token.Ttl}.");

                await _nodeParameters.ChannelTo.Writer.WriteAsync(token);
            }
            else
                Console.WriteLine("Ttl токена закончилось");
        }

        public void Stop() 
        {
            _eventStop.Set();
        }
    }
}
