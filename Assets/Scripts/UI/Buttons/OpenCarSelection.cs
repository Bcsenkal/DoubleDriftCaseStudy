using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

public class OpenCarSelection : UIButton
{
    
    protected override void Start() 
    {
        base.Start();
        Managers.EventManager.Instance.OnCloseCarSelection += Enable;
        Managers.EventManager.Instance.ONLevelStart += LevelStart;
    }

    protected override void OnClick()
    {
        base.OnClick();
        button.interactable = false;
        Managers.EventManager.Instance.ONOnOpenCarSelection();
        Managers.EventManager.Instance.ONOnBlockInput(true);
    }

    private void Enable()
    {
        button.interactable = true;
        Managers.EventManager.Instance.ONOnBlockInput(false);
    }

    private void LevelStart()
    {
        button.interactable = false;
        LMotion.Create(125f,-150f,0.3f).WithEase(Ease.InBack).BindToAnchoredPositionX(rect);
    }
}
