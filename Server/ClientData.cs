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
            if (messageArray[0] == "/s")
            {
                if (Server.Auction.FindItem(int.Parse(messageArray[1])))
                {
                    itemSubscribed = int.Parse(messageArray[1]);
                    writeToClient("/s "+ itemSubscribed);
                }
                else
                {
                    writeToClient("Cannot find item");
                }
            }
            if (messageArray[0] == "/us")
            {
                itemSubscribed = 0;
                writeToClient("/us 0");
            }
            if (messageArray[0] == "/o" && itemSubscribed != 0)
            {
                double price = double.Parse(messageArray[1]);
                bool answer = Server.Auction.Bid(itemSubscribed, price);
                if (answer) writeToClient("/oa ");
                else writeToClient("/od");
            }
            if (messageArray[0] == "/la")
            {
                writeToClient(Server.Auction.GetListe());
            }
            else
            {
                writeToClient("Unknown command");
            }
        }



    }
}
