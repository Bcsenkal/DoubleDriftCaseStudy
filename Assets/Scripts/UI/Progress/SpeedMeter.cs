using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedMeter : MonoBehaviour
{
    //Simple speed meter for UI

    private Transform needle;
    private Quaternion targetNeedleRotation;
    private TextMeshProUGUI speedText;
    private float currentSpeed;
    private float targetSpeed;
    void Start()
    {
        needle = transform.GetChild(1);
        speedText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Managers.EventManager.Instance.OnSetSpeedMeter += SetSpeedMeter;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
    }

    private void Update() 
    {
        SetVisual();
    }

    private void SetSpeedMeter(float speed)
    {
        
        targetNeedleRotation = Quaternion.Euler(new Vector3(0, 0, -(speed * 180f)));
        targetSpeed = speed * 200f;   
    }

    private void GameOver(bool isSuccess)
    {
        targetNeedleRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        targetSpeed = 0;
    }

    private void SetVisual()
    {
        needle.localRotation = Quaternion.Slerp(needle.localRotation, targetNeedleRotation, Time.deltaTime * 5f);
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
        var text = (int)currentSpeed;
        speedText.text = text.ToString("0") + "Kph";
    }
    
}
