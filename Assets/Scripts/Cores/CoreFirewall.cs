using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Cores
{
    public class CoreFirewall : Core
    {
        const float BASE_HP = 100f;
        public CoreFirewall(Node node, float speed) : base(node)
        {
            CoreSpeed = speed;
        }

        public override void AddToPlayerNewCore()
        {
            _Node._HPmax += BASE_HP * CoreSpeed;
            _Node.Defense++;
        }

        public override void RemoveToPlayerOldCore()
        {
            _Node._HPmax -= BASE_HP * CoreSpeed;
            if (_Node._HPmax < _Node._HP)
                _Node._HP = _Node._HPmax;
            _Node.Defense--;
        }

        public override void SpeedUpgrade(float amount)
        {
            CoreSpeed += amount;
            _Node._HPmax += BASE_HP * amount;
        }

        public override void Work(float deltaTime)
        {
            return;
        }

    }
}

