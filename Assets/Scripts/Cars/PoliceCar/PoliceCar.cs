using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour,IState
{
    public bool IsGameOver { get; set; }
    public bool IsGameStarted { get; set; }
    private Transform player;
    private bool isChasing;
    private Vector3 velocityRef;

    public void CacheEvents()
    {
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
        Managers.EventManager.Instance.OnSendPlayerData += SetPlayer;
        Managers.EventManager.Instance.ONLevelStart += GameStart;
    }

    public void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }

    void Start()
    {
        CacheEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsGameStarted) return;
        if(IsGameOver) return;
        if(!isChasing) return;
        var targetPos = new Vector3(player.position.x, transform.position.y, player.position.z - 3f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocityRef, 0.1f);
        var targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        if(Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        
    }

    private void SetPlayer(Transform p)
    {
        player = p;
    }

    public void GameStart()
    {
        if(IsGameStarted) return;
        IsGameStarted = true;
        isChasing = true;
    }
}
