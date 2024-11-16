using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }
    public void CacheEvents();

    public virtual void GameOver(bool isSuccess)
    {

    }

    public virtual void GameStart()
    {
        
    }
}
