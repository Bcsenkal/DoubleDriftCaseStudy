using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : Car, IPoolable
{
    private Renderer bodyRenderer;
    private bool isActive;
    private Transform player;
    private bool isDrivebyParticlePlayed;

    protected override void Start()
    {
        
    }
    public void CacheComponents()
    {
        bodyRenderer = transform.GetChild(0).GetComponent<Renderer>();
        CacheEvents();
    }

    public void SetTexture(Texture2D texture)
    {
        bodyRenderer.material.mainTexture = texture;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    private void Update() 
    {
        if(!isActive) return;
        if(!IsGameStarted) return;
        MoveForward();
        CheckDespawn();
    }

    public void Spawn(Vector3 position, bool gameStarted)
    {
        gameObject.SetActive(true);
        transform.position = position;
        isActive = true;
        isDrivebyParticlePlayed = false;
        IsGameStarted = gameStarted;
    }


    //First it checks if it's near the player, if player is too close to the AICar, "driveby" particle is played
    //Then it checks distance from the player to despawn so we won't need to create more AICars in runtime
    private void CheckDespawn()
    {
        if(transform.position.z > player.position.z) return;
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 1.7f)
        {
            if(!IsGameOver)
            {
                PlayDrivebyParticle();
            }
        }
        if(Mathf.Abs(player.position.z - transform.position.z) > 20f)
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }

    public void SetPlayer(Transform t)
    {
        player = t;
    }
    
    public void Destroy()
    {
        isActive = false;
        Managers.EventManager.Instance.ONOnPlayParticleHere(transform.position + Vector3.up * 0.4f, ParticleType.explosion);
        gameObject.SetActive(false);
    }

    //Calculates the player direction and adds offset according to it to play driveby particle
    private void PlayDrivebyParticle()
    {
        if(isDrivebyParticlePlayed) return;
        isDrivebyParticlePlayed = true;
        var particlePositionDirection = player.position.WithReplace(y : transform.position.y) - transform.position;
        var particlePosition = transform.position + particlePositionDirection.normalized * 1.1f;
        Managers.EventManager.Instance.ONOnPlayParticleHere(particlePosition + Vector3.up * 0.5f, ParticleType.driveBy);
    }
}
