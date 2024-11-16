using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : ObjectPooler
{
    public static CoinPool instance;

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
}
