using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : Rotatable
{
    private bool isGameOver;
    protected override void Start()
    {
        base.Start();
        Managers.EventManager.Instance.OnPlayerCrash += Crash;
    }
    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        if(isGameOver) return;
        targetRotation = Quaternion.Euler(0, -delta.x * 70f, 0);
    }

    protected override void Rotate()
    {
        if(isGameOver) return;
        if(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Crash()
    {
        isGameOver = true;
    }
}
