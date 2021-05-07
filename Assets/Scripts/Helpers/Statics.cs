using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static readonly GameObject LineGameobject = Resources.Load("NodeLink") as GameObject;

    public static int DEFAULT_CORES = 2;
    public static int MAX_CORES = 2;
    public static float DEFAULT_CPU_SPEED = 1f;

    public static PlayerData ClientPlayer;

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    
    public enum TransfertType
    {
        SENT, TRANSFERT, RECEIPT
    }

    public enum CoreType
    {
        MINING, FIREWALL, PACKET_KILLER, RESEARCH, TRACKING,
        EMPTY,
        UNAVAIBLE
    }

    public enum Currency
    {
        CURRENCY1, CURRENCY2, CURRENCY3, CURRENCY4, CURRENCY5
    }
}
