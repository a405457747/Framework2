using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BreadUI : MonoBehaviour
{
    public void Init(Bread bread)
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Game.I.OpenContentPanel(new ContentPanelArg()
            {
                MainStr = bread.Content
            });
        });
        TitleTextRefresh(bread.Title);
    }

//auto
    private void Awake()
    {
        TitleText = transform.Find("TitleText").GetComponent<Text>();
    }

    private Text TitleText = null;

    public void TitleTextRefresh(string t) => TitleText.text = t;
}