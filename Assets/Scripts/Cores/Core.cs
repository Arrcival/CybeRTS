using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public abstract class Core
    {
        public CoreType CoreType;
        public float CoreSpeed;
        private Node _Node;

        public Core(Node nodeReference)
        {
            _Node = nodeReference;
        }
        public abstract void RemoveToPlayerOldCore();
        public abstract void AddToPlayerNewCore();
        public abstract void SpeedUpgrade(float amount);
        public abstract void Work(float deltaTime);

    }
}
