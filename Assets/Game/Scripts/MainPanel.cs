using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : Panel
{
    private List<BreadUI> breadGos = new List<BreadUI>();
    private BreadMakerSystem _breadMakerSystem;

    public override void Initialize()
    {
        base.Initialize();
        Show();
        _breadMakerSystem = game._breadMakerSystem;

        BackupButtonAction += () => { _breadMakerSystem.WriteDataBackup(); };
        WriteButtonAction += () => { _breadMakerSystem.WriteData(); };

        ReadSuccess();
    }

    private void ReadSuccess()
    {
        var datas = _breadMakerSystem._breads;

        var BreadGo = Factorys.GetAssetFactory().loadGameObject("BreadUI");

        for (int i = 0; i < datas.Count; i++)
        {
            GameObject go = Instantiate(BreadGo);
            go.name = nameof(BreadUI) + i;
            BreadGameObjectsSetChild(go.transform);
            BreadUI breadUI = go.GetComponent<BreadUI>();
            breadUI.Init(datas[i].Title, datas[i].Content);
            breadGos.Add(breadUI);
        }
    }

    private void PerSecondEventCallback(PerSecondEvent obj)
    {
        var datas = _breadMakerSystem._breads;

        List<BreadUI> canInteractionBreadUis = new List<BreadUI>();

        foreach (var breadUI in breadGos)
        {
            var bread = datas[breadUI.gameObject.Number()];

            bool btnInteraction =
                game._breadMakerSystem.CanInteraction(bread, obj.timeStamp);

            breadUI.SetBtnInteraction(btnInteraction);
            breadUI.RipeTextRefresh(bread.GetRipeStr());

            if (btnInteraction == true)
            {
                canInteractionBreadUis.Add(breadUI);
                int index = canInteractionBreadUis.Count - 1;
                canInteractionBreadUis[index].transform.SetSiblingIndex(index);
            }
        }
    }

//auto
    private void Awake()
    {
        TopImage = transform.Find("TopImage").GetComponent<Image>();
        BreadGameObjects = transform.Find("Scroll View/Viewport/BreadGameObjects").gameObject;
        BottomImage = transform.Find("BottomImage").GetComponent<Image>();
        WriteButton = transform.Find("BottomImage/WriteButton").GetComponent<Button>();
        ReadButton = transform.Find("BottomImage/ReadButton").GetComponent<Button>();
        BackupButton = transform.Find("BottomImage/BackupButton").GetComponent<Button>();
        LvItemsGameObject = transform.Find("BottomImage/LvItemsGameObject").gameObject;

        WriteButton.onClick.AddListener(() => { WriteButtonAction?.Invoke(); });
        ReadButton.onClick.AddListener(() => { ReadButtonAction?.Invoke(); });
        BackupButton.onClick.AddListener(() => { BackupButtonAction?.Invoke(); });
        Incident.DeleteEvent<PerSecondEvent>(PerSecondEventCallback);
        Incident.RigisterEvent<PerSecondEvent>(PerSecondEventCallback);
    }

    private Image TopImage = null;
    private GameObject BreadGameObjects = null;
    private Image BottomImage = null;
    private Button WriteButton = null;
    public Action WriteButtonAction { get; set; }
    private Button ReadButton = null;
    public Action ReadButtonAction { get; set; }
    private Button BackupButton = null;
    public Action BackupButtonAction { get; set; }
    private GameObject LvItemsGameObject = null;

    private void TopImageRefresh(Sprite s) => TopImage.sprite = s;
    private void BreadGameObjectsSetChild(Transform t) => t.transform.SetParent(BreadGameObjects.transform, false);
    private void BottomImageRefresh(Sprite s) => BottomImage.sprite = s;
    private void LvItemsGameObjectSetChild(Transform t) => t.transform.SetParent(LvItemsGameObject.transform, false);
}