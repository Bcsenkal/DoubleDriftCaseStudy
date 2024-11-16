using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
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
        return obj ?? Instantiate(prefab, transform.position, Quaternion.identity,transform).GetComponent<IPoolable>();
    }
}
