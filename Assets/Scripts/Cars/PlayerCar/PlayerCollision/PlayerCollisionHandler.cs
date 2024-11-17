using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    private Rigidbody rb;
    private Collider col;
    private void Start() 
    {
        CacheComponents();
    }
    private void OnTriggerEnter(Collider other) 
    {
        //Checks if collision is AICar
        if(other.TryGetComponent(out AICar aiCar))
        {
            aiCar.Destroy();
            Crash();
        }

        //Checks if collision is Collectible
        if(other.TryGetComponent(out Collectible collectible))
        {
            collectible.GetCollected();
        }
    }

    private void CacheComponents()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Managers.EventManager.Instance.ONLevelEnd += OnWinCondition;
    }

    //On destructive collision with AICar, turns on physics of the car and adds crash force to it.
    private void Crash()
    {
        Managers.EventManager.Instance.ONOnPlayerCrash();
        rb.isKinematic = false;
        col.isTrigger = false;
        rb.AddForce(new Vector3(0,1.5f,3f)* 6f,ForceMode.Impulse);
        //rb.AddTorque(new Vector3(1,Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f)) * 30f,ForceMode.Impulse);
        rb.AddTorque(new Vector3(1,Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)) * 150f,ForceMode.Acceleration);
    }

    //On win condition, disables player collision to prevent player from colliding with AICar
    private void OnWinCondition(bool isSuccess)
    {
        if(!isSuccess) return;
        col.enabled = false;

    }
}
