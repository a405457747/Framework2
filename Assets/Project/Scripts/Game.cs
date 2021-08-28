using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public const string SystemSuffix = "System";
    private static Game _instance;
    public static Game I;
    private readonly Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    private readonly Dictionary<string, GameSystem> _systems = new Dictionary<string, GameSystem>();

    private Game()
    {
    }

    public static Transform CanvasTrans => GameObject.Find("Game/Canvas").transform;


    private void Start()
    {
        Initinal();
    }

    public void Update()
    {
        InputProcess();
        foreach (var s in _systems.Values) s.EachFrame();

        foreach (var p in _panels.Values) p.EachFrame();
    }

    private void OnDestroy()
    {
        Release();
    }

    private MainPanel _mainPanel;
    private BreadMakerSystem _breadMakerSystem;
    public TimeSystem _timeSystem;

    public void Initinal()
    {
        var config = Factorys.GetAssetFactory()
            .LoadScriptableObject<ProductConfigList>().list[0];
        SetMatchWidthOrHeight(config);

        var versionToolData = Factorys.GetAssetFactory().LoadScriptableObject<VersionToolData>();
        if (versionToolData.Debug)
        {
            gameObject.AddComponent<OutLog>().Init();
        }

        DontDestroyOnLoad(gameObject);

        I = this;

        AddSystem<AudioSystem>();

        _mainPanel = OpenPanel<MainPanel>();
        _timeSystem = AddSystem<TimeSystem>();
        _breadMakerSystem = AddSystem<BreadMakerSystem>();

        SetWriteExcelButton();
        SetBackupExcelButton();
    }

    private void SetBackupExcelButton()
    {
        _mainPanel.BackupButtonAction += _breadMakerSystem.WriteExcelPath2;
    }

    private void SetWriteExcelButton()
    {
        _mainPanel.WriteButtonAction += _breadMakerSystem.WriteExcelPath1;
    }

    private void SetLoadExcelButton()
    {
        _mainPanel.ReadButtonAction += _breadMakerSystem.ReadExcelPath1;
    }

    private void Release()
    {
        foreach (var s in _systems.Values) s.Release();

        foreach (var p in _panels.Values) p.Release();
    }

    private void InputProcess()
    {
    }

    public void CloseContentPanel()
    {
        ClosePanel<ContentPanel>();
    }

    public void OpenTipPanel(PanelArgs panelArgs)
    {
        OpenPanel<TipPanel>(panelArgs);
    }

    public void OpenContentPanel(PanelArgs panelArgs)
    {
        OpenPanel<ContentPanel>(panelArgs);
    }

    private T AddSystem<T>() where T : GameSystem
    {
        var systemName = typeof(T).Name;
        var t = GetComponentInChildren<T>();
        var go = t.gameObject;

        go.transform.LocalReset();

        _systems.Add(systemName, t);
        _systems[systemName].Initialize();

        return _systems[systemName] as T;
    }

    public T OpenPanel<T>(PanelArgs args = null) where T : Panel
    {
        var panelName = typeof(T).Name;
        if (!_panels.ContainsKey(panelName))
        {
            var go = Factorys.GetAssetFactory().LoadPanel(panelName);
            var tempGo = Instantiate(go);
            tempGo.Name(panelName);

            _panels.Add(panelName, tempGo.GetComponent<Panel>());

            _panels[panelName].Initialize(args);
        }

        _panels[panelName].gameObject.Show();
        _panels[panelName].Open(args);

        return _panels[panelName] as T;
    }

    public void ClosePanel<T>() where T : Panel
    {
        var panelName = typeof(T).Name;

        if (_panels.ContainsKey(panelName))
        {
            _panels[panelName].Close();
            _panels[panelName].gameObject.Hide();
        }
    }

    public void ReleasePanel<T>() where T : Panel
    {
        var panelName = typeof(T).Name;

        if (_panels.ContainsKey(panelName))
        {
            ClosePanel<T>();

            _panels[panelName].Release();
            Destroy(_panels[panelName].gameObject);
            _panels.Remove(panelName);
        }
    }

    private void Reset()
    {
    }

    private static void SetMatchWidthOrHeight(ProductConfig config) //横1竖0
    {
        float longNumber = config.ScreenLong;
        float shortNumber = config.ScreenShort;

        var canvasScaler = Game.CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (config.LandScape)
        {
            canvasScaler.referenceResolution = new Vector2(longNumber, shortNumber);
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(shortNumber, longNumber);
            canvasScaler.matchWidthOrHeight = 0;
        }
    }
}

public enum PanelTier
{
    Default,
    PopUp,
    AlwaysInFront,
    Guide,
    Effect,
    Curtain
}