using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
    //Base class for pooling system. Creates pool, gets pooled object if available, if not creates new

    //But most of the time we have to make sure we created enough objects and deactive them after use so we can reuse them

    
    [SerializeField]protected GameObject prefab;
    [SerializeField]protected int poolsize;
    protected List<IPoolable> pool; 
    protected virtual void Awake()
    {
        CreateObjects();
    }

    protected void CreateObjects()
    {
        pool = new List<IPoolable>();
        for(int i = 0; i < poolsize; i++)
        {
            var obj = Instantiate(prefab, transform.position, Quaternion.identity,transform);
            var poolable = obj.GetComponent<IPoolable>();
            poolable.CacheComponents();
            pool.Add(poolable);
            obj.SetActive(false);
        }
    }

    public IPoolable GetPooledObject()
    {
        IPoolable obj = null;
        for(int i = 0; i < pool.Count; i++)
        {
            if(!(pool[i] as MonoBehaviour).gameObject.activeInHierarchy)
            {
                obj = pool[i];
                break;
            }
        }
        if(obj == null)
        {
            obj = Instantiate(prefab, transform.position, Quaternion.identity,transform).GetComponent<IPoolable>();
            obj.CacheComponents();
            pool.Add(obj);   
        }
        return obj;
    }
}
