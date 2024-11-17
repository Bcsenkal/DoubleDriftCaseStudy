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
    private bool isSlowingDownOnEnd;
    private bool isSpeedingUp;
    
    protected override void Start()
    {
        base.Start();
        EventManager.Instance.OnSendCurrentDelta += SetHorizontalMovement;
        EventManager.Instance.OnStopRotation += StopRotation;
        EventManager.Instance.OnPlayerCrash += Crash;
        Managers.AudioManager.Instance.PlayMusic(true);
        isSpeedingUp = true;
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

    
    //According to delta, set horizontal movement so we do not just rotate around our anchor, we also move slowly in that direction
    private void SetHorizontalMovement(Vector2 delta)
    {
        horizontalMovement = horizontalSpeed * delta.x;
    }

    //If we have no input, horizontal movement should be stopped smoothly.
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
    
    //Send player data to required components. I'm using this only for position check, I could also broadcast our position continuously on Update, both works.
    private void SendPlayerData()
    {
        EventManager.Instance.ONOnSendPlayerData(transform);
    }

    //First we check if the game just started and we are speeding up. If we reach minimum speed limit, we stop making first check cuz our "minimum" speed on gameplay is "speedlimits.x"
    //Then we check if we reached at the end, in that case we gonna slow down so target speed is 0 and acceleration (deceleration) should be faster to reach 0 quickly
    //At the end, we check if we have move input, if not, we speed up slowly since we're not drifting and moving horizontally.

    private void AdjustSpeed()
    {
        if(isSpeedingUp)
        {
            if(speedLimits.x - speed < 0.5f)
            {
                speed = speedLimits.x + 1;
                isSpeedingUp = false;
            } 
        }
        
        var target = speed < speedLimits.x ? speedLimits.x : Mathf.Abs(horizontalMovement) < 0.4f ? speedLimits.y : speedLimits.x;
        if(isSlowingDownOnEnd)
        {
            target = 0;
            accelerationRate = 2f;
        }
        else
        {
            accelerationRate = speed < speedLimits.x ? 1.5f : 0.3f;
        }
        
        
        speed = Mathf.Lerp(speed,target,Time.deltaTime * accelerationRate);
        Managers.EventManager.Instance.ONOnSetSpeedMeter(speed/speedLimits.y);
    }

    private void Crash()
    {
        AudioManager.Instance.PlayMusic(false);
        AudioManager.Instance.PlayCrashSFX();
        Managers.EventManager.Instance.OnONLevelEnd(false);
    }

    public override void GameOver(bool isSuccess)
    {
        if(!isSuccess)
        {
            IsGameOver = true;
            return;
        }
        isSlowingDownOnEnd = true;

    }
}
