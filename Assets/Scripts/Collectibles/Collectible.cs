using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum CollectibleType
{
    coin
}
public class Collectible : MonoBehaviour, IPoolable
{
    [SerializeField]protected CollectibleType collectibleType;
    protected Collider col;
    protected Transform player;
    
    [SerializeField,ShowIf("collectibleType", CollectibleType.coin)]protected int value;

    public void CacheComponents()
    {
        col = GetComponent<Collider>();
    }

    public virtual void Spawn(Transform p)
    {
        col.enabled = true;
        player = p;
        gameObject.SetActive(true);
    }

    public virtual void GetCollected()
    {
        col.enabled = false;
    }

    protected void Disable()
    {
        gameObject.SetActive(false);
    }
}
