using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class CoreUnavaible : Core
{
    public CoreUnavaible(Node node) : base(node)
    {
        coreType = CoreType.UNAVAIBLE;
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
