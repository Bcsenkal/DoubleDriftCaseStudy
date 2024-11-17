using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveByParticle : MonoBehaviour, IPoolable, IParticle
{
    private ParticleSystem particle;
    public void CacheComponents()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void PlayParticle()
    {
        gameObject.SetActive(true);
        particle.Play();
        Invoke(nameof(StopParticle),5f);
    }

    public void StopParticle()
    {
        particle.Stop();
        particle.Clear();
        gameObject.SetActive(false);
    }
}
