using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

public class FailScreenController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Transform panelParent;
    private Transform gameOverText;
    private Transform progressImage;
    private Image fillImage;
    private TextMeshProUGUI percentText;
    private Button reloadButton;

    private bool isOpening;
    private int currentProgress;
    void Start()
    {
        CacheComponents();
        currentProgress = PlayerPrefs.GetInt("CurrentProgress",0);
        fillImage.fillAmount = currentProgress / 100f;
        percentText.text = $"{currentProgress}%";
        Managers.EventManager.Instance.ONLevelEnd += ShowFailScreen;
    }
    [ContextMenu("ShowFailScreen")]
    private void Show()
    {
        ShowFailScreen(false);
    }
    private void ShowFailScreen(bool b)
    {
        if(isOpening) return;
        isOpening = true;
        StartCoroutine(ShowFailScreenRoutine());
    }

    IEnumerator ShowFailScreenRoutine()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        yield return new WaitForSeconds(3f);
        yield return LMotion.Create(0,1f,0.5f).BindToCanvasGroupAlpha(canvasGroup).ToYieldInteraction();
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.5f).WithEase(Ease.OutBack).BindToLocalScale(panelParent).ToYieldInteraction();
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.5f).WithEase(Ease.OutBack).BindToLocalScale(gameOverText).ToYieldInteraction();
        LMotion.Create(Vector3.zero, Vector3.one,0.5f).WithEase(Ease.OutBack).BindToLocalScale(percentText.transform);
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.5f).WithEase(Ease.OutBack).BindToLocalScale(progressImage).ToYieldInteraction();
        var nextProgress = currentProgress + 10;
        nextProgress = Mathf.Clamp(nextProgress,0,100);
        yield return LMotion.Create(currentProgress, nextProgress, 1f).WithEase(Ease.Linear).Bind((f) => SetProgress(f)).ToYieldInteraction();
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.5f).WithEase(Ease.OutBack).BindToLocalScale(reloadButton.transform).ToYieldInteraction();
        canvasGroup.interactable = true;
        PlayerPrefs.SetInt("CurrentProgress",nextProgress);
    }

    private void SetProgress(float f)
    {
        fillImage.fillAmount = f / 100;
        percentText.text = $"{f}%";
    }

    

    private void CacheComponents()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        panelParent = transform.GetChild(0);
        gameOverText = panelParent.GetChild(0);
        progressImage = panelParent.GetChild(1);
        fillImage = progressImage.GetChild(0).GetComponent<Image>();
        percentText = panelParent.GetChild(2).GetComponent<TextMeshProUGUI>();
        reloadButton = panelParent.GetChild(3).GetComponent<Button>();
    }
}
