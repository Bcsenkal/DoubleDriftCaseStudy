using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveByParticlePool : ObjectPooler
{
    public static DriveByParticlePool instance{get; private set;}
    
    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
}
