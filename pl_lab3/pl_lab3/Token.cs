using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pl_lab3
{
    internal struct Token
    {
        public Token(string data, int receiverId, int ttl)
        {
            Data = data;
            ReceiverId = receiverId;
            Ttl = ttl;
        }

        public string Data { get; private set; }
        public int ReceiverId { get; private set; }
        public int Ttl { get; set; }
    }
}
