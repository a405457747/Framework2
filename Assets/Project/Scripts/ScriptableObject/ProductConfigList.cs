using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class ProductConfigList : ScriptableObject
{
    public List<ProductConfig> list = new List<ProductConfig>();
}