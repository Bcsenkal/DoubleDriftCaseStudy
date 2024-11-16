using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum TireType
{
    front,
    back
}
public class Tire : Rotatable
{
    [SerializeField] private TireType tireType;

    [HideIf("tireType", TireType.back)]

    protected override void SetRotationBasedOnDelta(Vector2 delta)
    {
        
        if (tireType == TireType.back) return;
        base.SetRotationBasedOnDelta(delta);
        targetRotation =  Quaternion.Euler(transform.rotation.x, delta.x * 30f,0f);
    }

    protected override void Rotate()
    {
        if(tireType == TireType.back) return;
        if(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
