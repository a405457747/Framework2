using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 戰鬥狀態
public class Battle : ISceneState
{
    public Battle(SceneStateController controller) : base(controller)
    {
        this.StateName = nameof(Battle);
    }

    // 開始
    public override void StateBegin()
    {
        Game.Instance.Initinal();
    }

    // 結束
    public override void StateEnd()
    {
        Game.Instance.Release();
    }

    // 更新
    public override void StateUpdate()
    {
        // 遊戲邏輯
        Game.Instance.Update();
        // Render由Unity負責

        // 遊戲是否結束
        if (Game.Instance.ThisGameIsOver())
            m_Controller.SetState(new MainMenu(m_Controller), nameof(MainMenu));
    }
}