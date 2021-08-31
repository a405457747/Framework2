using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class Bread
{
    public string ID;
    public string Title;
    public string Content;
    public double ProducedDate;
    public int Ripe;

    public string GetRipeStr()
    {
        return $"熟度Lv.{Ripe}";
    }
}