using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : Panel
{
	public override void Initialize(PanelArgs arguments)
	{
		base.Initialize(arguments);
	}
//auto
   private void Awake()
	{
		BottomImage=transform.Find("BottomImage").GetComponent<Image>();
	ReadButton=transform.Find("BottomImage/ReadButton").GetComponent<Button>();
	WriteButton=transform.Find("BottomImage/WriteButton").GetComponent<Button>();
	BackupButton=transform.Find("BottomImage/BackupButton").GetComponent<Button>();
	
        ReadButton.onClick.AddListener(()=>{ReadButtonAction?.Invoke();});
	WriteButton.onClick.AddListener(()=>{WriteButtonAction?.Invoke();});
	BackupButton.onClick.AddListener(()=>{BackupButtonAction?.Invoke();});
	
	}
	private Image BottomImage=null;
	private Button ReadButton=null;
	private Action ReadButtonAction{get;set;}
	private Button WriteButton=null;
	private Action WriteButtonAction{get;set;}
	private Button BackupButton=null;
	private Action BackupButtonAction{get;set;}
	
    private void BottomImageRefresh(Sprite s)=>BottomImage.sprite=s;
	    
}
