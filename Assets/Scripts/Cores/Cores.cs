using System;
using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public class Cores
    {
        private Core[] _Cores = new Core[MAX_CORES];
        float _CoresSpeed = DEFAULT_CPU_SPEED;

        private Node _Node;

        public Cores(Node nodeRef)
        {
            _Node = nodeRef;
            for (int i = 0; i < DEFAULT_CORES; i++)
                _Cores[i] = new CoreEmpty(nodeRef);
            for (int i = DEFAULT_CORES; i <= MAX_CORES - DEFAULT_CORES; i++)
                _Cores[i] = new CoreUnavailable(nodeRef);
        }

        public void WorkTime(float deltaTime)
        {
            Array.ForEach(_Cores, c => c.Work(deltaTime));
        }

        public void SetCore(int coreID, CoreType type)
        {
            if (coreID < 0 || coreID > MAX_CORES) return;

            CoreType selectedType = _Cores[coreID].CoreType;
            if (selectedType == CoreType.UNAVAILABLE) return;

            _Cores[coreID].RemoveToPlayerOldCore();
            Core newCore = CreateCoreByType(type);
            _Cores[coreID] = newCore;
        }

        public Core CreateCoreByType(CoreType type)
        {
            switch(type)
            {
                case (CoreType.MINING):
                    return new CoreMining(_Node, _CoresSpeed);
                default:
                    return new CoreEmpty(_Node);
            }
        }

        public void SpeedUpgrade(float upgradeAmount)
        {
            _CoresSpeed += upgradeAmount;
            for (int i = 0; i < _Cores.Length; i++)
                _Cores[i].SpeedUpgrade(upgradeAmount);
        }
    }
}
