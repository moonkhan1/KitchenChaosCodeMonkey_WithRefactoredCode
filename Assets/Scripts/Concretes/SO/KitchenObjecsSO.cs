using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen", menuName = "Kitchen/Kitchen Object", order = 51)]
public class KitchenObjecsSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
