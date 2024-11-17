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
    //I created 2 particles (red and blue) with 0.5s duration, and plays them in a loop to create a police light effect
    private void PlayParticle()
    {
        particles[particleIndex].Play();
        particleIndex = (particleIndex + 1) % particles.Length;
        Invoke(nameof(PlayParticle),0.5f);
    }
}
