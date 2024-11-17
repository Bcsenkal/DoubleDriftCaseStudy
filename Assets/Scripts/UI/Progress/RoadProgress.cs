using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoadProgress : MonoBehaviour
{

    //It's the road progress on top of the UI, checks the current distance until finish line and adjusts the fill.
    
    private RectTransform parentRect;
    private Image progressFill;
    private RectTransform progressCar;
    private float targetFill;
    private Vector2 targetPos;
    private bool isGameOver;

    void Start()
    {
        parentRect = GetComponent<RectTransform>();
        progressFill = transform.GetChild(0).GetComponent<Image>();
        progressCar = transform.GetChild(1).GetComponent<RectTransform>();
        Managers.EventManager.Instance.OnSetRoadProgress += SetProgress;
        Managers.EventManager.Instance.ONLevelEnd += LevelEnd;
    }

    private void Update() 
    {
        if(isGameOver) return;
        progressFill.fillAmount = Mathf.Lerp(progressFill.fillAmount, targetFill, Time.deltaTime * 5f);
        progressCar.anchoredPosition = Vector2.Lerp(progressCar.anchoredPosition, targetPos, Time.deltaTime * 5f);
    }

    private void SetProgress(float f)
    {
        targetFill = 1 - f;
        targetPos = new Vector2((1-f) * parentRect.rect.width, 0);
    }

    private void LevelEnd(bool b)
    {
        isGameOver = true;
    }
}
