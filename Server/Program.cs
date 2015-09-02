using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using ServerData;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server();
        }
        
    }

    class Server
    {
        private List<ClientData> listClients = new List<ClientData>();  
        private TcpListener listener;
        private int port = 5000;
        private string ipString = "127.0.0.1";
        public Server()
        {
            openConnection();

            new Thread(listenerForClients).Start();
            
        }

        private void listenerForClients()
        {
            
            while (true)
            {
                Socket clientSocket = listener.AcceptSocket();
                listClients.Add(new ClientData(clientSocket));
            }
        }

        private void openConnection()
        {
            IPAddress ip = IPAddress.Parse(ipString);
            listener = new TcpListener(ip, port);

            listener.Start();

        }

        public static void OpenReader(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;

            byte[] Buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    Buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(Buffer);

                    if (readBytes > 0)
                    {
                        Packet p = new Packet(Buffer);
                        dataManager(p);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Client Disconnected.");
                }
            }
        }

        private static void dataManager(Packet packet)
        {
            switch (packet.PacketType)
            {
                //Implementer Commands
                default:
                    break;
            }
        }

        private void writeToAll(Packet packet)
        {
            foreach (ClientData clientData in listClients)
            {
                clientData.writeToClient(packet);
            }
        }
    }
    
}
