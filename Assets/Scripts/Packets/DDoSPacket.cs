using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDoSPacket : Packet
{
    public DDoSPacket(Node startingNode, Node endingNode, List<Node> path) : base(startingNode, endingNode, path) { }
    public override void OnReceipt()
    {
        return;
    }
}
