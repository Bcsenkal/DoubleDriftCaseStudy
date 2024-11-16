using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Sirenix.OdinInspector;

public class PlayerCar : Car
{
    private float horizontalMovement;

    [SerializeField]private float horizontalSpeed;
    [SerializeField]private Vector2 XLimits;
    private float smoothDampRef = 0f;

    private bool hasInput;
    
    void Start()
    {
        EventManager.Instance.OnSendCurrentDelta += SetHorizontalMovement;
        EventManager.Instance.OnStopRotation += StopRotation;
        Invoke(nameof(SendPlayerData),1f);
    }

    // Update is called once per frame
    private void Update()
    {
        MoveForward();
        NormalizeHorizontalMovement();
    }

    private void LateUpdate() 
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, XLimits.x, XLimits.y);
        transform.position = pos;
    }

    

    private void SetHorizontalMovement(Vector2 delta)
    {
        horizontalMovement = horizontalSpeed * delta.x;
    }

    private void NormalizeHorizontalMovement()
    {
        if(!hasInput)
        {
            horizontalMovement = Mathf.SmoothDamp(horizontalMovement, 0, ref smoothDampRef, 0.3f);
        }
    }

    private void StopRotation()
    {
        hasInput = false;
    }

    protected override void MoveForward()
    {
        transform.Translate(new Vector3(horizontalMovement, 0, speed) * Time.deltaTime, Space.World);
    }
    
    private void SendPlayerData()
    {
        EventManager.Instance.ONOnSendPlayerData(transform);
    }
}
