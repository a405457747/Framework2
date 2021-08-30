using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TipPanel : Panel
{
    public override void Initialize()
    {
        tier = PanelTier.AlwaysInFront;
        base.Initialize();
    }
    
    //auto
    private void Awake()
    {
        MainText = transform.Find("MainText").GetComponent<Text>();
    }

    private Text MainText = null;

    public void MainTextRefresh(string t) => MainText.text = t;
}