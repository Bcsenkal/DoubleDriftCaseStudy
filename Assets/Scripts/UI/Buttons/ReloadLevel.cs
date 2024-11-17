using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : UIButton
{
    protected override void OnClick()
    {
        base.OnClick();
        Managers.GameManager.Instance.ReloadLevel();
    }
}
