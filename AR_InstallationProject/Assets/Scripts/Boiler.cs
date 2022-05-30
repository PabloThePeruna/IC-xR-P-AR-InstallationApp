using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boiler", menuName = "AddBoiler/Boiler")]
public class Boiler : ScriptableObject
{
    public float price;
    public GameObject boilerPrefab;
    public Sprite boilerImage;
}
