using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerData;

namespace Server
{
    class ClientData
    {
        private string brugerNavn;
        public string BrugerNavn
        {
            get
            {
                return brugerNavn;
            }

        }

        private Guid identifier;
        public Guid Identifier
        {
            get
            {
                return identifier;
            }
        }

        private Socket clientSocket;
        public Socket ClientSocket
        {
            get
            {
                return clientSocket;
            }
        }

        public ClientData(Socket clientSocket)
        {
            identifier = new Guid();
            new Thread(Server.OpenReader).Start(clientSocket);
        }

        public void writeToClient(Packet packet)
        {
            clientSocket.Send(packet.ToByte());

        }


    }
}
