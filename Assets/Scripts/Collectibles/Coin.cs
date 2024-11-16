using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible
{
    public override void GetCollected()
    {
        base.GetCollected();
        ResourceManager.Instance.AddCoin(value);
        Disable();
    }
}
