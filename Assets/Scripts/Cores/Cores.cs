using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Statics;

public class Cores
{
    public Core[] cores = new Core[MAX_CORES];
    float cores_speed = DEFAULT_CPU_SPEED;

    Node node;

    public Cores(Node nodeRef)
    {
        node = nodeRef;
        for (int i = 0; i < DEFAULT_CORES; i++)
            cores[i] = new CoreEmpty(nodeRef);
        for (int i = DEFAULT_CORES; i <= MAX_CORES - DEFAULT_CORES; i++)
            cores[i] = new CoreUnavaible(nodeRef);
    }

    public void WorkTime(float deltaTime)
    {
        Array.ForEach(cores, c => c.Work(deltaTime));
    }

    public void SetCore(int coreID, CoreType type)
    {
        if(coreID >= 0 && coreID <= MAX_CORES)
        {
            CoreType selectedType = cores[coreID].coreType;
            if(selectedType != CoreType.UNAVAIBLE)
            {
                cores[coreID].RemoveToPlayerOldCore();
                Core newCore = CreateCoreByType(type);
                cores[coreID] = newCore;
            }
        }
    }

    public Core CreateCoreByType(CoreType type)
    {
        switch(type)
        {
            case (CoreType.MINING):
                return new CoreMining(node, cores_speed);
            default:
                return new CoreEmpty(node);
        }
    }

    public void SpeedUpgrade(float upgradeAmount)
    {
        cores_speed += upgradeAmount;
        for (int i = 0; i < cores.Length; i++)
            cores[i].SpeedUpgrade(upgradeAmount);
    }
}
