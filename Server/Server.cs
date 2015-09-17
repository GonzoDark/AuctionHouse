using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using ServerData;

namespace Server
{
    class Server
    {
        public static AuctionHouse Auction = new AuctionHouse();
        private static List<ClientData> listClients;
        private TcpListener listener;
        private int port = 9001;
        private string ipString = "127.0.0.1";

        public Server()
        {
            openConnection();
            Console.WriteLine("Starting server: " + ipString + ":" + port);
            new Thread(listenerForClients).Start();
        }

        private void listenerForClients()
        {

            while (true)
            {
                Socket clientSocket = listener.AcceptSocket();
                Console.WriteLine("Client connected");
                listClients.Add(new ClientData(clientSocket));
            }
        }

        private void openConnection()
        {
            IPAddress ip = IPAddress.Parse(ipString);
            listener = new TcpListener(ip, port);
            listener.Start();

        }

        public static void writeToAllItemSold(string message)
        {
            foreach (ClientData clientData in listClients)
            {
                clientData.writeToClient(message);
            }
        }

        public static void writeToAllItemSold(int numberSold)
        {
            foreach (ClientData clientData in listClients)
            {
                clientData.writeToClient("/sold " + numberSold);
            }
        }
    }
}
    

