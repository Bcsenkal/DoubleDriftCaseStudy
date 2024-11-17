using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public void SetPlayer(Transform p)
    {
        player = p;
    }

    public override void GetCollected()
    {
        base.GetCollected();
        ResourceManager.Instance.AddCoin(value);
        Managers.AudioManager.Instance.PlayCollectSFX();
        Managers.EventManager.Instance.ONOnPlayParticleHere(transform.position + Vector3.up * 0.4f, ParticleType.loot);
        Disable();
    }

    private void Update() 
    {
        CheckDespawn();
    }

    public void CheckDespawn()
    {
        if(transform.position.z > player.position.z) return;
        if(Mathf.Abs(player.position.z - transform.position.z) > 20f)
        {
            Disable();
        }
    }
}
