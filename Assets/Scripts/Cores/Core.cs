using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public abstract class Core
    {
        public CoreType CoreType;
        public float CoreSpeed;
        protected Node _Node;

        public Core(Node nodeReference)
        {
            _Node = nodeReference;
        }
        public abstract void RemoveToPlayerOldCore();
        public abstract void AddToPlayerNewCore();
        public virtual void SpeedUpgrade(float amount)
        {
            CoreSpeed += amount;
        }
        public abstract void Work(float deltaTime);

    }
}
