using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    private ScrollRect scrollRect;
    private RectTransform viewPortTransform;
    private RectTransform selectionParent;
    private VerticalLayoutGroup layoutGroup;

    private RectTransform[] carList;
    private Vector2 oldVelocity;
    private bool isUpdated;
    void Start()
    {
        isUpdated = false;
        oldVelocity = Vector2.zero;
        scrollRect = GetComponent<ScrollRect>();
        viewPortTransform = scrollRect.viewport;
        selectionParent = scrollRect.content;
        layoutGroup = selectionParent.GetComponent<VerticalLayoutGroup>();
        SetCarList();
        CreateMoreItems();
        Debug.Log(carList.Length * (carList[0].rect.height + layoutGroup.spacing));
    }

    private void Update() 
    {
        if (isUpdated)
        {
            isUpdated = false;
            scrollRect.velocity = oldVelocity;
        }

        float offset = carList.Length * (carList[0].rect.height + layoutGroup.spacing);
        float localPosY = selectionParent.localPosition.y;

        if (localPosY > offset || localPosY < 0)
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            selectionParent.localPosition += new Vector3(0, localPosY > offset ? -offset : offset, 0);
            isUpdated = true;
        }
    }

    private void SetCarList()
    {
        var carCount = selectionParent.childCount;
        carList = new RectTransform[carCount];
        for(int i = 0; i < carCount; i++)
        {
            carList[i] = selectionParent.GetChild(i) as RectTransform;
        }
    }

    private void CreateMoreItems()
    {
        
        int itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.height / (carList[0].rect.height + layoutGroup.spacing));
        for (int i = 0; i < itemsToAdd; i++)
        {
            RectTransform rect = Instantiate(carList[i % carList.Length], selectionParent);
            rect.SetAsLastSibling();
        }

        for (int i = 0; i < itemsToAdd; i++)
        {
            int count = carList.Length-i-1;
            while (count < 0)
            {
                count += carList.Length;
            }
            RectTransform rect = Instantiate(carList[count], selectionParent);
            rect.SetAsFirstSibling();
        }

        selectionParent.localPosition = new Vector3(
        selectionParent.localPosition.x,
        (carList[0].rect.height + layoutGroup.spacing) * itemsToAdd,
        selectionParent.localPosition.z);
    }
}
