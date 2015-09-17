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
        private int id = 0; 
        private List<Items> itemList = new List<Items>();

        public AuctionHouse()
        {
            
            Items i1 = new Items();
            i1.Vurdering = 300;
            i1.Type = "Stol";
            Items i2 = new Items();
            i2.Vurdering = 250;
            i2.Type = "Computer";
            Items i3 = new Items();
            i3.Vurdering = 100;
            i3.Type = "N64 MarioKart";
            AddItem(i1);
            AddItem(i2);
            AddItem(i3);
        }
        public bool Bid(int itemSubscribed, double currentBet)
        {

            foreach (Items item in itemList)
            {
                if (item.Id == itemSubscribed)
                {
                    item.HighestOffer = currentBet;
                        return true;
                }
            }
            return false;
        }
        public void AddItem(Items item)
        {
            id++;
            item.Id = id;
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
        public string GetListe()
        {
            string tabelItems = "/la ";
            foreach (Items i in itemList)
            {
                tabelItems += i.Id + " " + i.Vurdering + " " + i.HighestOffer + " " + i.Type + "/n";

            }
            return tabelItems;
        }
    }
}
