using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarOption : MonoBehaviour, IPointerClickHandler
{

    //Car option on car selection panel. It will change color of frame when player clicks on it and will send event to set player color


    [SerializeField] private ColorType carColor;
    private bool isSelected;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        Managers.EventManager.Instance.OnSetPlayerColor += OnSetPlayerColor;
        SetSavedColor();
    }

    //Checks player prefs to see if this color was selected
    private void SetSavedColor()
    {
        if(PlayerPrefs.GetInt("PlayerColor",0) == (int)carColor)
        {
            isSelected = true;
            image.color = Color.green;
        }
    }

    //According to player selection, change color of frame so player will know which car is selected
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
