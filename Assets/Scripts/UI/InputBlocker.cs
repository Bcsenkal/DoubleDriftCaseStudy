using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBlocker : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
        Managers.EventManager.Instance.OnBlockInput += SetBlock;
    }

    private void SetBlock(bool block)
    {
        image.raycastTarget = block;
    }


}
