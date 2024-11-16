using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPool : ObjectPooler
{
    public static RoadPool instance;
    // Start is called before the first frame update
    protected override void Awake()
    {
        instance = this;
        base.Awake();
        
    }
}
