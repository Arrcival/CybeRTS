using Assets.Scripts;
using Assets.Scripts.Cores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public class CoreAnalyzer : Core
    {
        public CoreAnalyzer(Node node, float speed) : base(node)
        {
            CoreSpeed = speed;
        }
        public override void AddToPlayerNewCore()
        {
            ClientPlayer.PacketAnalyzeSpeed += CoreSpeed;
        }

        public override void RemoveToPlayerOldCore()
        {
            ClientPlayer.PacketAnalyzeSpeed -= CoreSpeed;
        }

        public override void SpeedUpgrade(float amount)
        {
            CoreSpeed += amount;
            ClientPlayer.PacketAnalyzeSpeed += amount;
        }

        public override void Work(float deltaTime)
        {
            return;
        }
    }
}