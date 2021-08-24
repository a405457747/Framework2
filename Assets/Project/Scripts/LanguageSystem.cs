using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LanguageSystem : GameSystem
{

    public LanguageSystem(Game game) : base(game)
    {
    }

#pragma warning disable 414
    private int _languageId;
#pragma warning restore 414
    private void AutoSetLanguageId()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                _languageId = 0;
                break;

            case SystemLanguage.ChineseTraditional:
                _languageId = 1;
                break;

            default:
                _languageId = 2;
                break;
        }
    }

    public static string GetMessage(int messageId)
    {
        return "";
    }
    public override void Initialize()
    {
        base.Initialize();
        AutoSetLanguageId();

    }

    public override void Release()
    {
        base.Release();
    }
//auto
   private void Awake()
	{
		
        
	}
	
        
}
