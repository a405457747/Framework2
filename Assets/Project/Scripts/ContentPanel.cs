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

public class ContentPanel : Panel
{
    public override void Initialize(PanelArgs arguments)
    {
        tier = PanelTier.PopUp;
        base.Initialize(arguments);
        CloseButtonAction += () => { game.ClosePanel<ContentPanel>(); };
    }

    public override void Open(PanelArgs arguments)
    {
        base.Open(arguments);

        ContentPanelArg contentPanelArg = arguments as ContentPanelArg;
        MainTextRefresh(contentPanelArg.MainStr);
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
