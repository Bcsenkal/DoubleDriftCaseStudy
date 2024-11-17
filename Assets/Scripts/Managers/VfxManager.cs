using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    explosion,
    loot,
    driveBy
}
public class VfxManager : MonoBehaviour
{
    private void Start() 
    {
        Managers.EventManager.Instance.OnPlayParticleHere += PlayParticle;
    }
    
    //Plays the requested particle at the requested position
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
            case ParticleType.driveBy:
                PlayParticleOnPosition(DriveByParticlePool.instance.GetPooledObject(),position);
                break;
            default:
                break;
        }
    }

    //Gets the poolable object and cast to set position and play it.
    private void PlayParticleOnPosition(IPoolable poolable, Vector3 pos)
    {
        (poolable as MonoBehaviour).transform.position = pos;
        (poolable as IParticle).PlayParticle();
    }
}
