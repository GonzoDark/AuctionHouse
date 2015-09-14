using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData
{
    public class Packet
    {
        public PacketType PacketType;
        public string Chat;
        public byte[] ToByte()
        {
            //Implementer ToByte
            return null;
        }

        public Packet(byte[] packetBytes)
        {
            
        }
    }

    public enum PacketType
    {
        Chat,
        Offer
    }
}
