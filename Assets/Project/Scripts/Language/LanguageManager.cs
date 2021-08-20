using UnityEngine;

public class LanguageManager : GameSystem
{
    private int languageId;

    public LanguageManager(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        AutoSetLanguageId();
    }

    private void AutoSetLanguageId()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                languageId = 0;
                break;

            case SystemLanguage.ChineseTraditional:
                languageId = 1;
                break;

            default:
                languageId = 2;
                break;
        }
    }

    public static string GetMessage(int messageId)
    {
        return "";
    }
}