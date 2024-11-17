using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPool : ObjectPooler
{
    public static CarPool instance{get; private set;}
    protected override void Awake() 
    {
        instance = this;
        base.Awake();
    }
}
