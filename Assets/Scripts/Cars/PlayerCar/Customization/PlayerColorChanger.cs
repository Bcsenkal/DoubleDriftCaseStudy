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

        ChangeColor((ColorType)PlayerPrefs.GetInt("PlayerColor", 0));
    }

    private void CacheComponents()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    //According to event, change the color of material of the car. We can also use textures but it needs refactoring.
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
