using Assets.Scripts.Packets;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerData : MonoBehaviour
    {
        public Packet PacketAnalyzed;
        public float PacketAnalyzeSpeed = 0f;


        public float Currency1 = 0f;
        public float Currency2 = 0f;
        public float Currency3 = 0f;
        public float Currency4 = 0f;
        public float Currency5 = 0f;


        #region Props based on speed
        // Does not represent gain, but the predicted value output gain
        public float SpeedCurrency1 = 0f;
        public float SpeedCurrency2 = 0f;
        public float SpeedCurrency3 = 0f;
        public float SpeedCurrency4 = 0f;
        public float SpeedCurrency5 = 0f;
        #endregion



    }
}
