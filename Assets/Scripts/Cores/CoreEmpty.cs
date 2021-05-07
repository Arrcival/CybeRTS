using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class CoreEmpty : Core
{
    public CoreEmpty(Node node) : base(node)
    {
        coreType = CoreType.EMPTY;
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
