using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    protected Button button;
    protected RectTransform rect;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rect = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {

    }

}
