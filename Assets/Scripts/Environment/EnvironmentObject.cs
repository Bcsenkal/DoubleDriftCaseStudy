using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentObject : MonoBehaviour,IPoolable
{
    public Vector3 Bounds { get; protected set; }

    public void CacheComponents()
    {
        SetBounds();
    }

    public abstract void Place(Vector3 position);

    public abstract void SetBounds();
}
