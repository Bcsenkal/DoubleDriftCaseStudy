using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLightController: MonoBehaviour
{
    private ParticleSystem[] particles;
    private int particleIndex;
    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        PlayParticle();
    }

    private void PlayParticle()
    {
        particles[particleIndex].Play();
        particleIndex = (particleIndex + 1) % particles.Length;
        Invoke(nameof(PlayParticle),0.5f);
    }
}
