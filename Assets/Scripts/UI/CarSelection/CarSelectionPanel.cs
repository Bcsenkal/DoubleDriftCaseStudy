using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

public class CarSelectionPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Button closeButton;
    private float closedPosition = -600f;

    private bool isOpened;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        closeButton = transform.GetChild(2).GetComponent<Button>();
        Managers.EventManager.Instance.OnOpenCarSelection += OpenPanel;
        closeButton.onClick.AddListener(ClosePanel);
        transform.localScale = Vector3.zero;
    }

    private void OpenPanel()
    {
        if(isOpened) return;
        isOpened = true;
        StartCoroutine(OpenRoutine());
    }
    
    private void ClosePanel()
    {
        if(!isOpened) return;
        isOpened = false;
        StartCoroutine(CloseRoutine());
    }

    IEnumerator OpenRoutine()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 0.5f).WithEase(Ease.OutBack).BindToLocalScale(transform);
        yield return LMotion.Create(closedPosition,0,0.5f).WithEase(Ease.OutBack).BindToLocalPositionX(transform).ToYieldInteraction();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Managers.EventManager.Instance.ONOnEnableCarSelection(true);
    }

    IEnumerator CloseRoutine()
    {
        canvasGroup.interactable = false;
        LMotion.Create(Vector3.one, Vector3.zero, 0.5f).WithEase(Ease.InBack).BindToLocalScale(transform);
        yield return LMotion.Create(0,closedPosition,0.5f).WithEase(Ease.InBack).BindToLocalPositionX(transform).ToYieldInteraction();
        canvasGroup.blocksRaycasts = false;
        Managers.EventManager.Instance.ONOnCloseCarSelection();
        Managers.EventManager.Instance.ONOnEnableCarSelection(false);
    }

}
