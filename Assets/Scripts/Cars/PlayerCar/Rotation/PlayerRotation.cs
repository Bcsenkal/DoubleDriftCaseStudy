using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : Rotatable
{
    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        targetRotation = Quaternion.Euler(0, -delta.x * 70f, 0);
        
    }

    protected override void Rotate()
    {
        if(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
