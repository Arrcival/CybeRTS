using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Packets
{
    public abstract class Packet
    {
        public List<Node> Path;
        public Node StartingNode;
        public Node EndingNode;

        public float Size;
        public Packet(List<Node> path, float size)
        {
            StartingNode = path.First();
            EndingNode = path.Last();
            Path = path;
            Size = size;
        }

        public int GetNextNodeID(int nodeID)
        {
            for (int i = 0; i < Path.Count - 1; i++)
                if (Path[i].ID == nodeID)
                    return Path[i + 1].ID;
            return -1;
        }

        public abstract void OnReceive(Node nodeReceiving);

        public abstract string GetNameType();
    }
}
