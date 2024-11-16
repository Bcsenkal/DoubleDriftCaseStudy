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
        if(other.TryGetComponent(out AICar aiCar))
        {
            Debug.Log("Collision Detected");
            if(Mathf.Abs(other.transform.position.x - transform.position.x) > 1.2f)
            {
                Debug.Log("Got no harm");
            }
            else
            {
                aiCar.Destroy();
                Crash();
            }
        }
    }

    private void CacheComponents()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Crash()
    {
        Managers.EventManager.Instance.ONOnPlayerCrash();
        rb.isKinematic = false;
        col.isTrigger = false;
        rb.AddForce(new Vector3(0,1.5f,3f)* 6f,ForceMode.Impulse);
        //rb.AddTorque(new Vector3(1,Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f)) * 30f,ForceMode.Impulse);
        rb.AddTorque(new Vector3(1,Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)) * 150f,ForceMode.Acceleration);
    }
}
