using System;
using System.Collections.Generic;
using System.IO;
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

        private StreamWriter writer;
        private StreamReader reader;
        private int itemSubscribed;
        public Socket ClientSocket
        {
            get
            {
                return clientSocket;
            }
        }

        public ClientData(Socket clientSocket)
        {
            NetworkStream networkStream = new NetworkStream(clientSocket);
            reader = new StreamReader(networkStream);
            writer = new StreamWriter(networkStream);
            brugerNavn = clientSocket.LocalEndPoint.ToString();
            new Thread(OpenReader).Start();
        }

        public void writeToClient(string message)
        {
            writer.WriteLine(message);
            writer.Flush();

        }

        public void OpenReader()
        {
            while (true)
            {
                string message;
                message = reader.ReadLine();
                dataManager(message);
            }
        }

        private void dataManager(string message)
        {
            ///commands
            string[] messageArray = message.Split(' ');
            if (messageArray[0] == "/a")
            {
                if (Server.Auction.FindItem(int.Parse(messageArray[1])))
                {
                    itemSubscribed = int.Parse(messageArray[1]);
                }
                else
                {
                    writeToClient("Cannot find item");
                }
            }
            if (messageArray[0] == "/ua")
            {
                itemSubscribed = 0;
            }
            if (messageArray[0] == "/o" && itemSubscribed != 0)
            {
                double price = double.Parse(messageArray[1]);
                bool answer = Server.Auction.Bid(itemSubscribed, price);
                if (answer) writeToClient("Offer accepted: " + price);
                else writeToClient("Offer denied");
            }
            
            else
            {
                writeToClient("Unknown command");
            }
        }



    }
}
