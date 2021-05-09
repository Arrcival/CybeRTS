using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public class CoreEmpty : Core
    {
        public CoreEmpty(Node node) : base(node)
        {
            CoreType = CoreType.EMPTY;
        }

        public override void AddToPlayerNewCore()
        {
            return;
        }

        public override void RemoveToPlayerOldCore()
        {
            return;
        }

        public override void SpeedUpgrade(float amount)
        {
            return;
        }
        public override void Work(float deltaTime)
        {
            return;
        }
    }
}
