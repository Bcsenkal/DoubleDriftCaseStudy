using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    explosion,
    loot
}
public class VfxManager : MonoBehaviour
{
    private void Start() 
    {
        Managers.EventManager.Instance.OnPlayParticleHere += PlayParticle;
    }
    
    private void PlayParticle(Vector3 position,ParticleType type)
    {
        switch(type)
        {
            case ParticleType.explosion:
                PlayParticleOnPosition(ExplosionPool.instance.GetPooledObject(),position);
                break;
            case ParticleType.loot:
                PlayParticleOnPosition(LootParticlePool.instance.GetPooledObject(),position);
                break;
            default:
                break;
        }
    }

    private void PlayParticleOnPosition(IPoolable poolable, Vector3 pos)
    {
        (poolable as MonoBehaviour).transform.position = pos;
        (poolable as IParticle).PlayParticle();
    }
}
