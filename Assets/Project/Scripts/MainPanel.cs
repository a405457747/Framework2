using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : Panel
{
    private List<BreadUI> breadGos = new List<BreadUI>();

    public override void Initialize()
    {
        base.Initialize();
        Show();

        BackupButtonAction += Game.I._breadMakerSystem.WriteDataBackup;
        WriteButtonAction += Game.I._breadMakerSystem.WriteData;
    }

    private void ReadSuccessEventCallback(ReadSuccessEvent obj)
    {
        var datas = obj.list;

        var BreadGo = Factorys.GetAssetFactory().loadGameObject("BreadUI");

        for (int i = 0; i < datas.Count; i++)
        {
            GameObject go = Instantiate(BreadGo);
            go.name = nameof(BreadUI) + i;
            BreadGameObjectsSetChild(go.transform);
            BreadUI breadUI = go.GetComponent<BreadUI>();
            breadUI.Init(datas[i]);
            breadGos.Add(breadUI);
        }
    }

    private void PerSecondEventCallback(PerSecondEvent obj)
    {
        List<BreadUI> canInteractionBreadUis = new List<BreadUI>();
        foreach (var breadUI in breadGos)
        {
            var b = breadUI.GetBread();
            bool btnInteraction = game._breadMakerSystem.CanInteraction(b, obj.timeStamp);
            breadUI.SetBtnInteraction(btnInteraction);
            if (btnInteraction == true)
            {
                canInteractionBreadUis.Add(breadUI);
            }
        }

        for (int i = 0; i < canInteractionBreadUis.Count; i++)
        {
            canInteractionBreadUis[i].transform.SetSiblingIndex(i);
        }
    }

    private void BtnClickEventCallback(BtnClickEvent obj)
    {
        var index = obj.btnIndex;

        breadGos[index].GetBread().Ripe += 1;

        breadGos[index].RefreshPipe();
    }

//auto
   private void Awake()
	{
		TopImage=transform.Find("TopImage").GetComponent<Image>();
	BreadGameObjects=transform.Find("Scroll View/Viewport/BreadGameObjects").gameObject;
	BottomImage=transform.Find("BottomImage").GetComponent<Image>();
	WriteButton=transform.Find("BottomImage/WriteButton").GetComponent<Button>();
	ReadButton=transform.Find("BottomImage/ReadButton").GetComponent<Button>();
	BackupButton=transform.Find("BottomImage/BackupButton").GetComponent<Button>();
	LvItemsGameObject=transform.Find("BottomImage/LvItemsGameObject").gameObject;
	
        WriteButton.onClick.AddListener(()=>{WriteButtonAction?.Invoke();});
	ReadButton.onClick.AddListener(()=>{ReadButtonAction?.Invoke();});
	BackupButton.onClick.AddListener(()=>{BackupButtonAction?.Invoke();});
	
	}
	private Image TopImage=null;
	private GameObject BreadGameObjects=null;
	private Image BottomImage=null;
	private Button WriteButton=null;
	public Action WriteButtonAction{get;set;}
	private Button ReadButton=null;
	public Action ReadButtonAction{get;set;}
	private Button BackupButton=null;
	public Action BackupButtonAction{get;set;}
	private GameObject LvItemsGameObject=null;
	
    private void TopImageRefresh(Sprite s)=>TopImage.sprite=s;
	private void BreadGameObjectsSetChild(Transform t)=>t.transform.SetParent(BreadGameObjects.transform, false);
	private void BottomImageRefresh(Sprite s)=>BottomImage.sprite=s;
	private void LvItemsGameObjectSetChild(Transform t)=>t.transform.SetParent(LvItemsGameObject.transform, false);
	    
}
