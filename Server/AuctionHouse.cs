using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class AuctionHouse
    {
        
        private List<Items> itemList = new List<Items>();
        private delegate void soldDelegate(int number);

        private event soldDelegate soldItemEvent;
        
        private delegate void bidDelegate(string message);

        private event bidDelegate bidItemEvent;
 

        public AuctionHouse()
        {
            soldItemEvent += RemoveItem;
            soldItemEvent += Server.writeToAll;

            bidItemEvent += Server.writeToAll;
        }

        public bool Bid(int itemSubscribed, double currentBet)
        {
            
            foreach (Items item in itemList )
            {
                if (item.Id == itemSubscribed)
                {
                    
                    if (item.HighestOffer < currentBet)
                    {
                        item.HighestOffer = currentBet;
                        bidItemEvent.Invoke("/i " + itemSubscribed + " " + item.HighestOffer);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }
        public void AddItem(Items item)
        {
            itemList.Add(item);
        }

        public void RemoveItem(int number)
        {
            foreach (Items item in itemList)
            {
                if (item.Id == number)
                    itemList.Remove(item);
            }  
        }

        public bool FindItem(int number)
        {
            foreach (Items item in itemList)
            {
                if (item.Id == number)
                    return true;
            }
            return false;
        }
    }
}
