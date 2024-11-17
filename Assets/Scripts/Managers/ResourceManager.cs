using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{

    //scalable resource manager


    private int currentCoin;
    protected override void Awake() 
    {
        base.Awake();
        LoadResources();
    }

    private void LoadResources()
    {
        currentCoin = PlayerPrefs.GetInt("TotalCoin");
    }

    
    public void SpendCoin(int amount)
    {
        currentCoin -= amount;
        if (currentCoin < 0)
        {
            currentCoin = 0;
        }
        PlayerPrefs.SetInt("TotalCoin",currentCoin);
        EventManager.Instance.ONOnSetCurrentCoin(amount,false);
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        PlayerPrefs.SetInt("TotalCoin",currentCoin);
        EventManager.Instance.ONOnSetCurrentCoin(amount,true);
    }

    public int GetCurrentCoin()
    {
        return currentCoin;
    }
    

    public bool HasEnoughGold(int price)
    {
        return price <= currentCoin;
    }
}
