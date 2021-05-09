using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Packets
{
    public abstract class Packet
    {
        public List<Node> Path;
        public Node StartingNode;
        public Node EndingNode;

        public Packet(List<Node> path)
        {
            StartingNode = path.First();
            EndingNode = path.Last();
            Path = path;
        }

        public Packet(Node startingNode, Node endingNode, List<Node> path)
        {
            StartingNode = startingNode;
            EndingNode = endingNode;
            Path = path;
        }

        public int GetNextNodeID(int nodeID)
        {
            for (int i = 0; i < Path.Count - 1; i++)
                if (Path[i].ID == nodeID)
                    return Path[i + 1].ID;
            return -1;
        }

        public abstract void OnReceipt();
    }
}
