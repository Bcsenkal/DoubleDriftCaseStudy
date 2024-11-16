using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable: MonoBehaviour
{
    protected bool hasInput;
    protected Quaternion targetRotation;
    [SerializeField]protected float rotationSpeed;

    protected virtual void Start()
    {
        Managers.EventManager.Instance.OnSendCurrentDelta += SetRotationBasedOnDelta;
        Managers.EventManager.Instance.OnStopRotation += StopRotation;
    }

    protected virtual void Update()
    {
        Rotate();
    }

    protected virtual void SetRotationBasedOnDelta(Vector2 delta)
    {
        if(!hasInput) hasInput = true;
    }

    protected virtual void Rotate()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void StopRotation()
    {
        hasInput = false;
        targetRotation = Quaternion.identity;
    }
}
