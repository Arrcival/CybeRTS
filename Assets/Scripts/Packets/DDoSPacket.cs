using System.Collections.Generic;

namespace Assets.Scripts.Packets
{
    public class DDoSPacket : Packet
    {
        public DDoSPacket(List<Node> path) : base(path) { }
        public DDoSPacket(Node startingNode, Node endingNode, List<Node> path) : base(startingNode, endingNode, path) { }
        public override void OnReceipt()
        {
            return;
        }
    }
}
