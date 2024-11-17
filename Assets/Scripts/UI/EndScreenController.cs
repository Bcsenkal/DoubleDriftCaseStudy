using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

public class EndScreenController : MonoBehaviour
{
    //One end screen controller for both win and lose condition
    
    private CanvasGroup canvasGroup;
    private Transform panelParent;
    private TextMeshProUGUI gameOverText;
    private Transform progressImage;
    private Image fillImage;
    private TextMeshProUGUI percentText;
    private Button reloadButton;
    private TextMeshProUGUI reloadText;

    private bool isOpening;
    private int currentProgress;
    void Start()
    {
        CacheComponents();
        currentProgress = PlayerPrefs.GetInt("CurrentProgress",0);
        fillImage.fillAmount = currentProgress / 100f;
        percentText.text = $"{currentProgress}%";
        Managers.EventManager.Instance.ONLevelEnd += ShowEndScreen;
    }
    
    private void ShowEndScreen(bool isSuccess)
    {
        if(isOpening) return;
        isOpening = true;
        StartCoroutine(ShowEndScreenRoutine(isSuccess));
    }

    IEnumerator ShowEndScreenRoutine(bool isSuccess)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        gameOverText.text = isSuccess ? "You Win!" : "Game Over";
        yield return new WaitForSeconds(3f);
        yield return LMotion.Create(0,1f,0.3f).BindToCanvasGroupAlpha(canvasGroup).ToYieldInteraction();
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.3f).WithEase(Ease.OutBack).BindToLocalScale(panelParent).ToYieldInteraction();
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.3f).WithEase(Ease.OutBack).BindToLocalScale(gameOverText.transform).ToYieldInteraction();
        LMotion.Create(Vector3.zero, Vector3.one,0.3f).WithEase(Ease.OutBack).BindToLocalScale(percentText.transform);
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.3f).WithEase(Ease.OutBack).BindToLocalScale(progressImage).ToYieldInteraction();
        if(isSuccess)
        {
            var nextProgress = currentProgress + 10;
            nextProgress = Mathf.Clamp(nextProgress,0,100);
            yield return LMotion.Create(currentProgress, nextProgress, 0.3f).WithEase(Ease.Linear).Bind((f) => SetProgress(f)).ToYieldInteraction();
            PlayerPrefs.SetInt("CurrentProgress",nextProgress);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        reloadText.text = isSuccess ? "Continue" : "Try Again";
        yield return LMotion.Create(Vector3.zero, Vector3.one,0.3f).WithEase(Ease.OutBack).BindToLocalScale(reloadButton.transform).ToYieldInteraction();
        canvasGroup.interactable = true;
        
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
        gameOverText = panelParent.GetChild(0).GetComponent<TextMeshProUGUI>();
        progressImage = panelParent.GetChild(1);
        fillImage = progressImage.GetChild(0).GetComponent<Image>();
        percentText = panelParent.GetChild(2).GetComponent<TextMeshProUGUI>();
        reloadButton = panelParent.GetChild(3).GetComponent<Button>();
        reloadText = reloadButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}
