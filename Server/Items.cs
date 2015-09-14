using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Items
    {
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

        private double _highestOffer;

        public double HighestOffer
        {
            get { return _highestOffer; }
            set { _highestOffer = value; }
        }
   
    }
}
