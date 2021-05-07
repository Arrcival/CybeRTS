using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public abstract class Core
{
    public CoreType coreType;
    public float coreSpeed;
    Node node;

    public Core(Node nodeReference)
    {
        node = nodeReference;
    }
    public abstract void RemoveToPlayerOldCore();
    public abstract void AddToPlayerNewCore();
    public abstract void SpeedUpgrade(float amount);
    public abstract void Work(float deltaTime);

}
