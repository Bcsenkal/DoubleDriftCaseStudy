using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootParticlePool : ObjectPooler
{
    public static LootParticlePool instance;
    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
}
