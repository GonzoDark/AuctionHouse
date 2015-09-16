using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient
{
    class Item
    {
        private string _name;
        private string _estimate;
        private int _offer;
        private int _id;

        public Item(string name, string estimate, int offer, int id)
        {
            _name = name;
            _estimate = estimate;
            _offer = offer;
            _id = id;
        }
        public int CurrentBid
        {
            get { return _offer; }
            set { _offer = value; }
        }
        public int Estimate
        {
            get { return _estimate; }
            set { _estimate = value; }
        }
        public string Name
        {
            get { return _name; }
        }
        public int Id
        {
            get { return _id; }
        }
    }
}
