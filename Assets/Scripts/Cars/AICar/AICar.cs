using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour, IPoolable
{
    private Renderer bodyRenderer;
    private bool isActive;
    private float carSpeed;
    private Transform player;
    public void CacheComponents()
    {
        bodyRenderer = transform.GetChild(0).GetComponent<Renderer>();
    }

    public void SetTexture(Texture2D texture)
    {
        bodyRenderer.material.mainTexture = texture;
    }

    public void SetSpeed(float speed)
    {
        carSpeed = speed;
    }

    private void Update() 
    {
        if(isActive)
        {
            transform.Translate(Vector3.forward * carSpeed * Time.deltaTime,Space.World);
            if(transform.position.z > player.position.z) return;
            if(Mathf.Abs(player.position.z - transform.position.z) > 20f)
            {
                DeSpawn();
            }
        }
    }

    public void Spawn(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        isActive = true;
    }

    public void DeSpawn()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void SetPlayer(Transform t)
    {
        player = t;
    }
}
