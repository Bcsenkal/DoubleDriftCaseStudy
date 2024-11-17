using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : EnvironmentObject
{

    [SerializeField]private Renderer roadRenderer;
    
    public override void Place(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    //Gets the bounds of the road so when we create new roads we know where to place them
    public override void SetBounds()
    {
        Bounds = roadRenderer.bounds.size;
    }
}
