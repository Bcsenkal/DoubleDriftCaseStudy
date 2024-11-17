using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //Simple state interface

    //I could use global state machine too, but thought it's gonna be overkill for the scope of this project
    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }
    public void CacheEvents();
}
