using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Items
    {
        private delegate void soldDelegate(int number);
        private event soldDelegate soldItemEvent;
        private delegate void bidDelegate(string message);
        private event bidDelegate bidItemEvent;


        private TimeSpan auctionRunning;
        private int runningTime = 30;
        public int RunningTime
        {
            set { runningTime += value; }
        }

        public void StartAuction()
        {
            if (auctionRunning == null)
            {
                auctionRunning = DateTime.Now.TimeOfDay;
                new Thread(auctionStart).Start();
            }
        }
        private void auctionStart()
        {
            while (DateTime.Now.TimeOfDay - auctionRunning < TimeSpan.FromMinutes(runningTime))
            {
            }
            soldItemEvent.Invoke(id);
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double _highestOffer = 0;

        public double HighestOffer
        {
            get { return _highestOffer; }
            set
            {
                lock (this)
                {
                    if (_highestOffer < value)
                    {
                        _highestOffer = value;
                        RunningTime = 5;
                        bidItemEvent.Invoke("/i " + id + " " + HighestOffer);
                    }
                }
            }
        }

        private double vurdering;

        public double Vurdering
        {
            get { return vurdering; }
            set { vurdering = value; }            
        }

        public Items()
        {
            bidItemEvent += Server.writeToAllItemSold;
            soldItemEvent += Server.Auction.RemoveItem;             
            soldItemEvent += Server.writeToAllItemSold;
        }
        

    }
}
