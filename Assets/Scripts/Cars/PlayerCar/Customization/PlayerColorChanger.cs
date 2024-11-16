using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    [SerializeField]private List<CarData> carDatas;
    private Renderer[] renderers;
    void Start()
    {
        CacheComponents();
        Managers.EventManager.Instance.OnSetPlayerColor += ChangeColor;

        if(!PlayerPrefs.HasKey("PlayerColor")) return;
        ChangeColor((ColorType)PlayerPrefs.GetInt("PlayerColor"));
    }

    private void CacheComponents()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void ChangeColor(ColorType colorType)
    {
        var targetColor = carDatas.Find(x => x.colorType == colorType).color;
        PlayerPrefs.SetInt("PlayerColor", (int)colorType);
        foreach (var renderer in renderers)
        {
            renderer.material.color = targetColor;
        }
    }
}
