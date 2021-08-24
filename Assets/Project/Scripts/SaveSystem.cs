using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveSystem : GameSystem
{
    private readonly ISave CurSave = new UnityJsonSave();

    public SaveSystem(Game game) : base(game)
    {
    }

    public SaveMap SaveMap => CurSave.SaveMap;

    private void Load()
    {
        CurSave.Save();
    }

    private void Save()
    {
        CurSave.Save();
    }

    private string GetSaveMapString()
    {
        return CurSave.GetSaveMapString();
    }
//auto
   private void Awake()
	{
		
        
	}
	
        
}
