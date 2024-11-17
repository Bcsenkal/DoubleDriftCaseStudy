using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : ObjectPooler
{
    public static CoinPool instance{get; private set;}

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
}
