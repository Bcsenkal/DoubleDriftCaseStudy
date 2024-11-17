using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBody : Rotatable
{
    //Explanation is on inherited class

    protected override void Rotate()
    {
        if(Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        targetRotation = Quaternion.Euler(0,0,-delta.x * 25f);
    }
}
