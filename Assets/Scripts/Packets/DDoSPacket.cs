using System.Collections.Generic;

namespace Assets.Scripts.Packets
{
    public class DDoSPacket : Packet
    {
        public DDoSPacket(List<Node> path, float size) : base(path, size) { }
        public override void OnReceive(Node nodeReceiving)
        {
            return;
        }

        public override string GetNameType()
        {
            return "DDoS";
        }
    }
}
