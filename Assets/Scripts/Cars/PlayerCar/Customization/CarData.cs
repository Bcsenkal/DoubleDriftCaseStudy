using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    red,
    green,
    blue,
    yellow,
    orange
}

[CreateAssetMenu(fileName = "CarData", menuName = "Customization/Car Data")]
public class CarData : ScriptableObject
{
    public ColorType colorType;
    public Color color;
}
