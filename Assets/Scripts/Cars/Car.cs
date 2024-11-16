using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, IState
{
    [SerializeField]protected float speed;

    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }

    protected virtual void Start()
    {
        CacheEvents();
    }

    public virtual void CacheEvents()
    {
        Managers.EventManager.Instance.ONLevelStart += GameStart;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
    }

    public virtual void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }

    public virtual void GameStart()
    {
        IsGameStarted = true;
    }

    protected virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
}
