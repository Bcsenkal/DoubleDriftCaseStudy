using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public bool IsGameOver { get; set; }
    public void CacheEvents();

    public void GameOver(bool isSuccess);
}
