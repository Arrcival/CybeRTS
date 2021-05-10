using static Assets.Scripts.Helpers.Statics;

namespace Assets.Scripts.Cores
{
    public class CoreMining : Core
    {
        public Currency MinedCurrency;
        private float _WorkedTime;
        public float TimeToCompletion => 3 / CoreSpeed;

        public CoreMining(Node node, float speed) : base(node)
        {
            CoreType = CoreType.MINING;
            CoreSpeed = speed;
        }

        public override void Work(float deltaTime)
        {
            _WorkedTime += deltaTime;
            if(_WorkedTime >= TimeToCompletion)
            {
                _WorkedTime -= TimeToCompletion;

                // Create Packet !
            }
            return;
        }


        public override void AddToPlayerNewCore()
        {
            return; // Must select a currency later
        }

        public override void RemoveToPlayerOldCore()
        {
            ChangePlayerMiningValue(-CoreSpeed);
        }

        public override void SpeedUpgrade(float amount)
        {
            base.SpeedUpgrade(amount);
            ChangePlayerMiningValue(amount);
        }

        private void ChangeCurrencyMined(Currency newCurrency)
        {
            ChangePlayerMiningValue(-CoreSpeed);
            MinedCurrency = newCurrency;
            ChangePlayerMiningValue(CoreSpeed);
        }

        #region Interaction with PlayerData

        private void ChangePlayerMiningValue(float amount)
        {
            switch(MinedCurrency)
            {
                case (Currency.CURRENCY1):
                    ClientPlayer.SpeedCurrency1 += amount;
                    break;
                case (Currency.CURRENCY2):
                    ClientPlayer.SpeedCurrency2 += amount;
                    break;
                case (Currency.CURRENCY3):
                    ClientPlayer.SpeedCurrency3 += amount;
                    break;
                case (Currency.CURRENCY4):
                    ClientPlayer.SpeedCurrency4 += amount;
                    break;
                case (Currency.CURRENCY5):
                    ClientPlayer.SpeedCurrency5 += amount;
                    break;
                default:
                    break;
            }
            return;
        }
        #endregion
    }
}
