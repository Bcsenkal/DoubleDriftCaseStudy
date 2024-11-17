using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable: MonoBehaviour, IState
{
    protected bool hasInput;
    protected Quaternion targetRotation;
    [SerializeField]protected float rotationSpeed;

    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }

    protected virtual void Start()
    {
        CacheEvents();
    }

    protected virtual void Update()
    {
        if(!IsGameStarted) return;
        if(IsGameOver) return;
        Rotate();
    }
    // Gets delta of the input and sets the rotation according to delta, it's virtual so inheritors can override.
    protected virtual void SetRotationBasedOnDelta(Vector2 delta)
    {
        if(!IsGameStarted) return;
        if(IsGameOver) return;
        if(!hasInput) hasInput = true;
    }

    //After we get target rotation from delta value earlier, we lerp to target rotation until we meet angle check
    protected virtual void Rotate()
    {
        if(IsGameOver) return;
    }

    protected virtual void StopRotation()
    {
        hasInput = false;
        targetRotation = Quaternion.identity;
    }

    public void CacheEvents()
    {
        Managers.EventManager.Instance.OnSendCurrentDelta += SetRotationBasedOnDelta;
        Managers.EventManager.Instance.OnStopRotation += StopRotation;
        Managers.EventManager.Instance.ONLevelStart += GameStart;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
    }

    public virtual void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }

    public virtual void GameStart()
    {
        if(IsGameStarted) return;
        IsGameStarted = true;
    }   
}
