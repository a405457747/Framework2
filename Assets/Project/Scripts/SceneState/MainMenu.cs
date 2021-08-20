using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 主選單狀態
public class MainMenu : ISceneState
{
    public MainMenu(SceneStateController controller) : base(controller)
    {
        this.StateName = nameof(MainMenu);
    }

    // 開始
    public override void StateBegin()
    {
        /*Button tmpBtn = UITool.GetUIComponent<Button>("StartGameBtn");
        if(tmpBtn!=null)
            tmpBtn.onClick.AddListener( ()=> OnStartGameBtnClick(tmpBtn) );*/
    }

    // 開始戰鬥
    private void OnStartGameBtnClick(Button theButton)
    {
        m_Controller.SetState(new Battle(m_Controller), nameof(Battle));
    }
}