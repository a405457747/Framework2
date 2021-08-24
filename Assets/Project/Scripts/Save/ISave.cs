using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    SaveMap SaveMap { get; set; }
    void Load();
    void Save();
    string GetSaveMapString();
}