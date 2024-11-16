using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;
using LitMotion.Collections;
public class SlideToDrift : MonoBehaviour
{
    private Transform hand;
    private Transform carAnchor;
    private CompositeMotionHandle compositeHandler;
    void Start()
    {
        hand = transform.GetChild(3);
        carAnchor = transform.GetChild(2);
        StartCoroutine(ShowIntroduction());
        Managers.EventManager.Instance.ONLevelStart += LevelStart;
    }

    IEnumerator ShowIntroduction()
    {
        compositeHandler = new CompositeMotionHandle();
        yield return new WaitForSeconds(0.1f);
        LMotion.Create(0,300f,0.6f).WithEase(Ease.OutQuad).BindToLocalPositionX(hand).AddTo(compositeHandler);
        LMotion.Create(0, 100f, 0.6f).WithEase(Ease.OutQuad).BindToLocalPositionX(carAnchor).AddTo(compositeHandler);
        yield return LMotion.Create(0, 20f,0.6f).WithEase(Ease.OutQuad).BindToLocalEulerAnglesZ(carAnchor).AddTo(compositeHandler).ToYieldInteraction();

        LMotion.Create(300f, -300f, 1.2f).WithLoops(-1, LoopType.Yoyo).WithEase(Ease.OutQuad).BindToLocalPositionX(hand).AddTo(compositeHandler);
        LMotion.Create(100f, -100f, 1.2f).WithLoops(-1, LoopType.Yoyo).WithEase(Ease.OutQuad).BindToLocalPositionX(carAnchor).AddTo(compositeHandler);
        LMotion.Create(20f, -20f, 1.2f).WithLoops(-1, LoopType.Yoyo).WithEase(Ease.OutQuad).BindToLocalEulerAnglesZ(carAnchor).AddTo(compositeHandler).ToYieldInteraction();
    }

    private void LevelStart()
    {
        StopAllCoroutines();
        compositeHandler.Cancel();
        transform.localScale = Vector3.zero;
    }
}
