using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Klient
{
    class Client
    {
        StreamReader reader;
        StreamWriter writer;
        public Client(string address, int ip)
        {
            TcpClient Connection = new TcpClient(address,ip);
            NetworkStream netstream = Connection.GetStream();
            reader = new StreamReader(netstream);
            writer = new StreamWriter(netstream);

        }
        public string ClientWriter(string input)
        {
            writer.WriteLine(input);
            writer.Flush();
            return input;
        }

        public string ClientReader()
        {
            return reader.ReadLine();
        }

    }
}
