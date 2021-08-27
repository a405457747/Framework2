using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : Panel
{
    private List<BreadUI> breadGos = new List<BreadUI>();

    public override void Initialize(PanelArgs arguments)
    {
        base.Initialize(arguments);
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

        Log.LogPrint(datas.Count);
    }

//auto
    private void Awake()
    {
        BreadGameObjects = transform.Find("Scroll View/Viewport/BreadGameObjects").gameObject;
        BottomImage = transform.Find("BottomImage").GetComponent<Image>();
        ReadButton = transform.Find("BottomImage/ReadButton").GetComponent<Button>();
        WriteButton = transform.Find("BottomImage/WriteButton").GetComponent<Button>();
        BackupButton = transform.Find("BottomImage/BackupButton").GetComponent<Button>();

        ReadButton.onClick.AddListener(() => { ReadButtonAction?.Invoke(); });
        WriteButton.onClick.AddListener(() => { WriteButtonAction?.Invoke(); });
        BackupButton.onClick.AddListener(() => { BackupButtonAction?.Invoke(); });
        Incident.DeleteEvent<ReadSuccessEvent>(ReadSuccessEventCallback);
        Incident.RigisterEvent<ReadSuccessEvent>(ReadSuccessEventCallback);
    }

    private GameObject BreadGameObjects = null;
    private Image BottomImage = null;
    private Button ReadButton = null;
    public Action ReadButtonAction { get; set; }
    private Button WriteButton = null;
    public Action WriteButtonAction { get; set; }
    private Button BackupButton = null;
    public Action BackupButtonAction { get; set; }

    private void BreadGameObjectsSetChild(Transform t) => t.transform.SetParent(BreadGameObjects.transform, false);
    private void BottomImageRefresh(Sprite s) => BottomImage.sprite = s;
}