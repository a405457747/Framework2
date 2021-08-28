using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TipPanelArgs : PanelArgs
{
    public string mainTip { get; set; }
}

public class TipPanel : Panel
{
    public override void Initialize(PanelArgs arguments)
    {
        tier = PanelTier.AlwaysInFront;
        base.Initialize(arguments);
    }

    public override void Open(PanelArgs arguments)
    {
        base.Open(arguments);

        TipPanelArgs tipPanelArgs = arguments as TipPanelArgs;

        MainTextRefresh(tipPanelArgs.mainTip);

        this.Delay(3f, () => { game.ClosePanel<TipPanel>(); });
    }

    //auto
    private void Awake()
    {
        MainText = transform.Find("MainText").GetComponent<Text>();
    }

    private Text MainText = null;

    public void MainTextRefresh(string t) => MainText.text = t;
}