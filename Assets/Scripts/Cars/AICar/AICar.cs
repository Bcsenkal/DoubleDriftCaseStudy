using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : Car, IPoolable
{
    private Renderer bodyRenderer;
    private bool isActive;
    private Transform player;
    public void CacheComponents()
    {
        bodyRenderer = transform.GetChild(0).GetComponent<Renderer>();
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
        if(isActive)
        {
            MoveForward();
            CheckDeSpawn();
        }
    }

    public void Spawn(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        isActive = true;
    }

    public void CheckDeSpawn()
    {
        if(transform.position.z > player.position.z) return;
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
}
