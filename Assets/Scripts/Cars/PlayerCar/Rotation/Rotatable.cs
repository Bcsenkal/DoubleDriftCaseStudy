using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable: MonoBehaviour, IState
{
    protected bool hasInput;
    protected Quaternion targetRotation;
    [SerializeField]protected float rotationSpeed;
    public bool IsGameOver { get; set; }

    protected virtual void Start()
    {
        CacheEvents();
    }

    protected virtual void Update()
    {
        if(IsGameOver) return;
        Rotate();
    }

    protected virtual void SetRotationBasedOnDelta(Vector2 delta)
    {
        if(IsGameOver) return;
        if(!hasInput) hasInput = true;
    }

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
        Managers.EventManager.Instance.OnPlayerCrash += Crash;
    }

    public void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }   

    protected virtual void Crash()
    {
        GameOver(false);
    }
}
