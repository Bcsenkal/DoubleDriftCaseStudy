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
    private float horizontalRef = 0f;
    [SerializeField]private Vector2 speedLimits;
    [SerializeField]private float accelerationRate;

    private bool hasInput;
    
    protected override void Start()
    {
        base.Start();
        EventManager.Instance.OnSendCurrentDelta += SetHorizontalMovement;
        EventManager.Instance.OnStopRotation += StopRotation;
        EventManager.Instance.OnPlayerCrash += Crash;
        Managers.AudioManager.Instance.PlayMusic(true);
        Invoke(nameof(SendPlayerData),0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        if(!IsGameStarted) return;
        if(IsGameOver) return;
        MoveForward();
        NormalizeHorizontalMovement();
        AdjustSpeed();
    }

    private void LateUpdate() 
    {
        if(IsGameOver) return;
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
            horizontalMovement = Mathf.SmoothDamp(horizontalMovement, 0, ref horizontalRef, 0.3f);
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

    private void AdjustSpeed()
    {
        var target = Mathf.Abs(horizontalMovement) < 0.4f ? speedLimits.y : speedLimits.x;
        speed = Mathf.Lerp(speed,target,Time.deltaTime * accelerationRate);
    }

    private void Crash()
    {
        AudioManager.Instance.PlayMusic(false);
        Managers.EventManager.Instance.OnONLevelEnd(false);
    }
}
