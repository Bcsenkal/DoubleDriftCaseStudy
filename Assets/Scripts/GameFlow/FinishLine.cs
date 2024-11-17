using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, IState
{
    private Transform player;

    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }
    private float totalDistance;
    void Start()
    {
        CacheEvents();
        SetFinishLinePosition();
    }

    void Update()
    {
        if(IsGameOver) return;
        if(!IsGameStarted) return;
        SetCurrentProgress();
        if(player.position.z > transform.position.z)
        {
            Managers.EventManager.Instance.OnONLevelEnd(true);
        }
    }

    //Places the finish line a bit further from the last finish;
    private void SetFinishLinePosition()
    {
        transform.position = new Vector3(0,0,PlayerPrefs.GetFloat("LatestFinish", 500f)+ 250f);
    }

    private void SetCurrentProgress()
    {
        var currentDistance = Mathf.Abs(player.position.z - transform.position.z);
        Managers.EventManager.Instance.ONOnSetRoadProgress(currentDistance/totalDistance);
    }

    private void SetPlayer(Transform p)
    {
        player = p;
        totalDistance = Mathf.Abs(player.position.z - transform.position.z);
    }

    public void CacheEvents()
    {
        Managers.EventManager.Instance.ONLevelStart += GameStart;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
        Managers.EventManager.Instance.OnSendPlayerData += SetPlayer;
    }

    private void GameStart()
    {
        if(IsGameStarted) return;
        IsGameStarted = true;
    }

    //if we win, save current position to place next line further
    private void GameOver(bool isSuccess)
    {
        if(IsGameOver) return;
        IsGameOver = true;
        if(isSuccess)
        {
            PlayerPrefs.SetFloat("LatestFinish", transform.position.z);
        }

    }
}
