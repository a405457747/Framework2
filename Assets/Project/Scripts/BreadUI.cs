using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BreadUI : MonoBehaviour
{
    private Button btn;
    private Bread _bread;

    public void Init(Bread b)
    {
        _bread = b;

        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            Game.I.OpenContentPanel(new ContentPanelArg()
            {
                MainStr = _bread.Content
            });

            Incident.SendEvent<BtnClickEvent>(new BtnClickEvent()
            {
                btnIndex = gameObject.Number()
            });

        });
        TitleTextRefresh(_bread.Title);
        RefreshPipe();
    }

    public void RefreshPipe()
    {
        RipeTextRefresh(_bread.GetRipeStr());
    }

    public Bread GetBread()
    {
        return _bread;
    }

    public bool GetBtnInteraction()
    {
        return btn.interactable;
    }
    
    public void SetBtnInteraction(bool interaction)
    {
        btn.interactable = interaction;
    }

//auto
    private void Awake()
    {
        TitleText = transform.Find("TitleText").GetComponent<Text>();
        RipeText = transform.Find("RipeText").GetComponent<Text>();
    }

    private Text TitleText = null;
    private Text RipeText = null;

    public void TitleTextRefresh(string t) => TitleText.text = t;
    public void RipeTextRefresh(string t) => RipeText.text = t;
}