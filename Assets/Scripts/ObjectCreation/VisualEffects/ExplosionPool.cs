using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : ObjectPooler
{
    public static ExplosionPool instance;
    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
}
