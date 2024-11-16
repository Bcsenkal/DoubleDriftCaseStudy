using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ColorType carColor;
    private bool isSelected;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        Managers.EventManager.Instance.OnSetPlayerColor += OnSetPlayerColor;
        if(PlayerPrefs.GetInt("PlayerColor",0) == (int)carColor)
        {
            isSelected = true;
            image.color = Color.green;
        }
    }

    private void OnSetPlayerColor(ColorType colorType)
    {
        if(carColor == colorType)
        {
            isSelected = true;
            image.color = Color.green;
        }
        else
        {
            isSelected = false;
            image.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected) return;
        Managers.EventManager.Instance.ONOnSetPlayerColor(carColor);
    }
}
