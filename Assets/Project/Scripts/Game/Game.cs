using UnityEngine;
using System.Collections;

public class Game
{
    // Singleton模版
    private static Game _instance;

    public static Game Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Game();
            return _instance;
        }
    }

    // 場景狀態控制
    private bool m_bGameOver = false;

    // 遊戲系統
    // 界面
    private Game()
    {
    }

    // 初始P-BaseDefense遊戲相關設定
    public void Initinal()
    {
        // 場景狀態控制
        m_bGameOver = false;
        // 遊戲系統

        // 界面

        // 注入到其它系統

        // 載入存檔

        // 註冊遊戲事件系統
    }

    // 釋放遊戲系統
    public void Release()
    {
        // 遊戲系統

        // 界面

        // 存檔
    }

    // 更新
    public void Update()
    {
        // 玩家輸入
        InputProcess();

        // 遊戲系統更新

        // 玩家界面更新
    }

    // 玩家輸入
    private void InputProcess()
    {
    }

    // 遊戲狀態
    public bool ThisGameIsOver()
    {
        return m_bGameOver;
    }

    // 換回主選單
    public void ChangeToMainMenu()
    {
        m_bGameOver = true;
    }
}