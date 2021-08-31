using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BreadUI : MonoBehaviour
{
    private Button btn;

    public void Init(string title, string content)
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Incident.SendEvent(new BtnClickEvent()
            {
                breadUI = this,
                content = content,
            });
        });
        TitleTextRefresh(title);
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