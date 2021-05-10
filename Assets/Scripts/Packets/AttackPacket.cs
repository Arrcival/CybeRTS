using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Packets
{
    public class AttackPacket : Packet
    {
        public float Damages;
        public AttackPacket(List<Node> path, float size, float damages) : base(path, size)
        {
            Damages = damages;
        }
        public override void OnReceive(Node nodeReceiving)
        {
            nodeReceiving.DealDamage(Damages);
        }

        public override string GetNameType()
        {
            return "Attack";
        }
    }
}

