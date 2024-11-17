using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    protected Button button;
    protected RectTransform rect;
    
    protected virtual void Start()
    {
        CacheComponents();
    }

    protected void CacheComponents()
    {
        rect = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        Managers.AudioManager.Instance.PlayButtonClick();
    }
}
