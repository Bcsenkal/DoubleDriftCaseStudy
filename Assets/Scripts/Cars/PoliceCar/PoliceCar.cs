using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour,IState
{
    public bool IsGameOver { get; set; }
    private Transform player;
    private bool isChasing;
    private Vector3 velocityRef;

    public void CacheEvents()
    {
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
        Managers.EventManager.Instance.OnSendPlayerData += SetPlayer;
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
        
        if(IsGameOver) return;
        if(!isChasing) return;
        var targetPos = new Vector3(player.position.x, transform.position.y, player.position.z - 3f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocityRef, 0.1f);
        var direction = player.position - targetPos;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
    }

    private void SetPlayer(Transform p)
    {
        player = p;
        isChasing = true;
    }
}
