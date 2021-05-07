using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Packet
{
    public List<Node> Path = new List<Node>();
    public Node StartingNode;
    public Node EndingNode;

    public Packet(Node startingNode, Node endingNode, List<Node> path)
    {
        StartingNode = startingNode;
        EndingNode = endingNode;
        Path = path;
    }

    public int GetNextNodeID(int NodeID)
    {
        for (int i = 0; i < Path.Count - 1; i++)
            if (Path[i].ID == NodeID)
                return Path[i + 1].ID;
        return -1;
    }

    public abstract void OnReceipt();
}
