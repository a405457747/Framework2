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

        BackupButtonAction += Game.I._breadMakerSystem.WriteExcelPath2;
        WriteButtonAction += Game.I._breadMakerSystem.WriteExcelPath1;
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
            bool btnInteraction = game._timeSystem.CanInteraction(b, obj.timeStamp);
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
        TopImage = transform.Find("TopImage").GetComponent<Image>();
        BreadGameObjects = transform.Find("Scroll View/Viewport/BreadGameObjects").gameObject;
        BottomImage = transform.Find("BottomImage").GetComponent<Image>();
        ReadButton = transform.Find("BottomImage/ReadButton").GetComponent<Button>();
        WriteButton = transform.Find("BottomImage/WriteButton").GetComponent<Button>();
        BackupButton = transform.Find("BottomImage/BackupButton").GetComponent<Button>();
        LvItemsGameObject = transform.Find("BottomImage/LvItemsGameObject").gameObject;

        ReadButton.onClick.AddListener(() => { ReadButtonAction?.Invoke(); });
        WriteButton.onClick.AddListener(() => { WriteButtonAction?.Invoke(); });
        BackupButton.onClick.AddListener(() => { BackupButtonAction?.Invoke(); });
        Incident.DeleteEvent<ReadSuccessEvent>(ReadSuccessEventCallback);
        Incident.RigisterEvent<ReadSuccessEvent>(ReadSuccessEventCallback);
        Incident.DeleteEvent<PerSecondEvent>(PerSecondEventCallback);
        Incident.RigisterEvent<PerSecondEvent>(PerSecondEventCallback);
        Incident.DeleteEvent<BtnClickEvent>(BtnClickEventCallback);
        Incident.RigisterEvent<BtnClickEvent>(BtnClickEventCallback);
    }

    private Image TopImage = null;
    private GameObject BreadGameObjects = null;
    private Image BottomImage = null;
    private Button ReadButton = null;
    public Action ReadButtonAction { get; set; }
    private Button WriteButton = null;
    public Action WriteButtonAction { get; set; }
    private Button BackupButton = null;
    public Action BackupButtonAction { get; set; }
    private GameObject LvItemsGameObject = null;

    private void TopImageRefresh(Sprite s) => TopImage.sprite = s;
    private void BreadGameObjectsSetChild(Transform t) => t.transform.SetParent(BreadGameObjects.transform, false);
    private void BottomImageRefresh(Sprite s) => BottomImage.sprite = s;
    private void LvItemsGameObjectSetChild(Transform t) => t.transform.SetParent(LvItemsGameObject.transform, false);
}