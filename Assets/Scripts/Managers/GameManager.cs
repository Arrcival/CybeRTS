using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static Statics;

public class GameManager : MonoBehaviour
{
    public static float TimeSinceStart = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //ClientPlayer = new PlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceStart += Time.deltaTime;
    }
}
