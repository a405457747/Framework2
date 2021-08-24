using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// todo excel工具,excel工具做好了信息的收集比如宽高这些
public class Game : MonoBehaviour
{
    private static Game _instance;

    public static Game I;
    private readonly Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
    private readonly Dictionary<string, GameSystem> _systems = new Dictionary<string, GameSystem>();
    public static string SystemSuffix = "System";

    private Game()
    {
    }

    public Transform CanvasTrans => GameObject.Find("Game/Canvas").transform;

    private void Reset()
    {
        SetMatchWidthOrHeight(true);
    }

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

    public void Initinal()
    {
        DontDestroyOnLoad(gameObject);

        I = this;

        AddSystem<AudioSystem>();

    }

    private void Release()
    {
        foreach (var s in _systems.Values) s.Release();

        foreach (var p in _panels.Values) p.Release();
    }

    private void InputProcess()
    {
    }

    private void AddSystem<T>() where T : GameSystem
    {
        var systemName = typeof(T).Name;
        T t = GetComponentInChildren<T>();
        GameObject go = t.gameObject;

        go.transform.LocalReset();

        _systems.Add(systemName, t);
        _systems[systemName].Initialize();
    }

    public void OpenPanel<T>(PanelArgs args = null, PanelTier tier = PanelTier.Default) where T : Panel
    {
        var panelName = typeof(T).Name;
        if (!_panels.ContainsKey(panelName))
        {
            var go = Factorys.GetAssetFactory().LoadPanel(panelName);
            var tempGo = Instantiate(go,
                CanvasTrans.Find(tier.ToString()), false);
            tempGo.Name(panelName);

            _panels.Add(panelName, tempGo.GetComponent<Panel>());

            _panels[panelName].Initialize(args);
        }

        _panels[panelName].gameObject.Show();
        _panels[panelName].Open(args);
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

    private void SetMatchWidthOrHeight(bool isLandscape) //横1竖0
    {
        const float longNumber = 1280;
        const float shortNumber = 720;

        var canvasScaler = CanvasTrans.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        if (isLandscape)
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