using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CarOption : MonoBehaviour, IPointerClickHandler
{

    //Car option on car selection panel. It will change color of frame when player clicks on it and will send event to set player color


    [SerializeField] private ColorType carColor;
    private bool isSelected;
    private Image image;

    private Transform lockParent;
    private TextMeshProUGUI lockCostText;
    [SerializeField]private int unlockCost;
    private bool isUnlocked;
    void Start()
    {
        CacheComponents();
        CacheEvents();
        CheckIfUnlocked();
        SetTextColor();
        SetSavedColor();
    }

    private void CheckIfUnlocked()
    {
        isUnlocked = PlayerPrefs.GetInt(carColor.ToString() + "Unlocked",0) == 1;
        if(!isUnlocked)
        {
            if(carColor == ColorType.red || carColor == ColorType.blue)
            {
                PlayerPrefs.SetInt(carColor.ToString() + "Unlocked",1);
                isUnlocked = true;
            } 
        }
    }

    

    private void CacheComponents()
    {
        image = GetComponent<Image>();
        lockParent = transform.GetChild(1);
        lockCostText = lockParent.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        lockCostText.text = unlockCost.ToString();
    }

    private void CacheEvents()
    {
        Managers.EventManager.Instance.OnSetPlayerColor += OnSetPlayerColor;
        Managers.EventManager.Instance.OnColorUnlock += OnColorUnlock;
    }

    //Checks player prefs to see if this color was selected
    private void SetSavedColor()
    {
        if(PlayerPrefs.GetInt("PlayerColor",0) == (int)carColor)
        {
            isSelected = true;
            image.color = Color.green;
        }
        if(!isUnlocked)
        {
            lockParent.localScale = Vector3.one;
        }
    }
    
    
    private void SetTextColor()
    {
        lockCostText.color= ResourceManager.Instance.HasEnoughGold(unlockCost) ? Color.white : Color.red;
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
        if(isUnlocked) return;
        SetTextColor();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected) return;
        if(!isUnlocked)
        {
            var canUnlock = ResourceManager.Instance.HasEnoughGold(unlockCost);
            Managers.AudioManager.Instance.PlayUnlockSfx(canUnlock);
            if(!canUnlock) return;
            isUnlocked = true;
            ResourceManager.Instance.SpendCoin(unlockCost);
            lockParent.localScale = Vector3.zero;
            PlayerPrefs.SetInt(carColor.ToString() + "Unlocked",1);
        }
        Managers.EventManager.Instance.ONOnSetPlayerColor(carColor);
    }

    public void OnColorUnlock(ColorType type)
    {
        if(type != carColor) return;
        if(isUnlocked) return;
        isUnlocked = true;
        lockParent.localScale = Vector3.zero;
    }
}
