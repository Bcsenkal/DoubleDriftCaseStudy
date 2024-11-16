using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : EnvironmentObject
{

    [SerializeField]private Renderer roadRenderer;

    public void OnSpawn()
    {
        throw new System.NotImplementedException();
    }

    public override void Place(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public override void SetBounds()
    {
        Bounds = roadRenderer.bounds.size;
    }
}
