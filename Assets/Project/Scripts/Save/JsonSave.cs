﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSave : ISave
{
    public SaveMap SaveMap { get; set; }

    public void Load()
    {
        var tempStr = GetSaveMapString();
        SaveMap = tempStr == "" ? new SaveMap() : JsonUtility.FromJson<SaveMap>(tempStr);
    }

    public void Save()
    {
        PlayerPrefs.SetString("SaveKey", JsonUtility.ToJson(SaveMap));
    }

    public string GetSaveMapString()
    {
        return PlayerPrefs.GetString("SaveKey", "");
    }
}