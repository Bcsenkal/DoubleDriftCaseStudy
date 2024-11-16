using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Rotatable
{

    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        base.SetRotationBasedOnDelta(delta);
        targetRotation = Quaternion.Euler(0, 0, -delta.x * 120f);
    }

    protected override void Rotate()
    {
        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
    }
}
