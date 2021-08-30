using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ContentPanel : Panel
{
    public override void Initialize()
    {
        tier = PanelTier.PopUp;
        base.Initialize();
    }

    //auto
   private void Awake()
	{
		CloseButton=transform.Find("CloseButton").GetComponent<Button>();
	MainText=transform.Find("Image/MainText").GetComponent<Text>();
	
        CloseButton.onClick.AddListener(()=>{CloseButtonAction?.Invoke();});
	
	}
	private Button CloseButton=null;
	public Action CloseButtonAction{get;set;}
	private Text MainText=null;
	
    public void MainTextRefresh(string t)=>MainText.text=t;
	    
}
