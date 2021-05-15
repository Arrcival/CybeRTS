using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Statics
    {
        public static readonly GameObject LineGameObject = Resources.Load("NodeLink") as GameObject;

        public static readonly int DEFAULT_CORES = 2;
        public static readonly int MAX_CORES = 8;
        public static readonly float DEFAULT_CPU_SPEED = 1f;


        public static float SIZE_TREATED_PER_SECONDS = 2f;

        public static float RAW_HP = 100f;

        public static float TIME_FOR_FULL_REGEN = 5f;

        public static PlayerData ClientPlayer;

        public static UIManager UIManager;

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static Vector2 Rotate(this Vector2 vector, float degree)
        {
            float angle = degree * Mathf.Deg2Rad;
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);

            return new Vector2(
                vector.x * cos - vector.y * sin,
                vector.x * sin - vector.y * cos
            );
        }
        public enum TransferType
        {
            SENT, TRANSFER, RECEIVED
        }

        public enum CoreType
        {
            MINING, FIREWALL, PACKET_KILLER, RESEARCH, TRACKING,
            EMPTY,
            UNAVAILABLE
        }

        public enum Currency
        {
            CURRENCY1, CURRENCY2, CURRENCY3, CURRENCY4, CURRENCY5
        }


        // Debug
        public static Currency GetRandomCurrencyType()
        {
            float nb = Random.Range(1, 6); // int is exclusive
            if (nb == 1) return Currency.CURRENCY1;
            if (nb == 2) return Currency.CURRENCY2;
            if (nb == 3) return Currency.CURRENCY3;
            if (nb == 4) return Currency.CURRENCY4;
            return Currency.CURRENCY5;
        }
    }
}
