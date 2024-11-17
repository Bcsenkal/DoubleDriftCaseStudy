using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingWheel : Rotatable
{
    //Driving wheel visual for UI
    
    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        base.SetRotationBasedOnDelta(delta);
        targetRotation = Quaternion.Euler(0, 0, -delta.x * 120f);
    }

    protected override void Rotate()
    {
        if (Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
    }

    public override void GameStart()
    {
        transform.localScale = Vector3.one;
        base.GameStart();
        
    }
}
