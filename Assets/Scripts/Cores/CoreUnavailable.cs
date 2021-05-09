using static Assets.Scripts.Helpers.Statics;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Cores
{
    public class CoreUnavailable : Core
    {
        public CoreUnavailable(Node node) : base(node)
        {
            CoreType = CoreType.UNAVAILABLE;
        }

        public override void AddToPlayerNewCore()
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveToPlayerOldCore()
        {
            throw new System.NotImplementedException();
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
