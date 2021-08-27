using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContentPanelArg : PanelArgs
{
    public string MainStr { get; set; }
}

public class ContentPanel : Panel, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Game.I.CloseContentPanel();
    }

    public override void Initialize(PanelArgs arguments)
    {
        base.Initialize(arguments);

        ContentPanelArg contentPanelArg = arguments as ContentPanelArg;

        MainTextRefresh(contentPanelArg.MainStr);
    }

//auto
    private void Awake()
    {
        MainText = transform.Find("MainText").GetComponent<Text>();
    }

    private Text MainText = null;

    public void MainTextRefresh(string t) => MainText.text = t;
}