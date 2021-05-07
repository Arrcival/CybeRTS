using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class CoreMining : Core
{
    public Currency MinedCurrency;
    float workedTime;
    float timeToCompletion { get { return 3 / coreSpeed; } }
    public CoreMining(Node node, float speed) : base(node)
    {
        coreType = CoreType.MINING;
        coreSpeed = speed;
    }

    public override void Work(float deltaTime)
    {
        workedTime += deltaTime;
        if(workedTime >= timeToCompletion)
        {
            workedTime -= timeToCompletion;

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
        RemovePlayerMiningValue(coreSpeed);
    }

    public override void SpeedUpgrade(float amount)
    {
        coreSpeed += amount;
        AddPlayerMiningValue(amount);
    }

    void ChangeCurrencyMined(Currency newCurrency)
    {
        RemovePlayerMiningValue(coreSpeed);
        MinedCurrency = newCurrency;
        AddPlayerMiningValue(coreSpeed);
    }

    #region Interaction with PlayerData
    void AddPlayerMiningValue(float amount)
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

    void RemovePlayerMiningValue(float amount)
    {
        AddPlayerMiningValue(-amount);
    }
    #endregion
}
