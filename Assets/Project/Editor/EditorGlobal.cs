using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorGlobal
{
    public static string GetAssetsPathAbsolute(string willPath)
    {
        var tempPath = willPath.Replace("Assets/", "");
        return Path.Combine(Application.dataPath, tempPath);
    }

    public static string GetMyOtherVersionPath()
    {
        return GetMyOtherPath() + "/version.txt";
    }

    public static string GetMyOtherPath()
    {
        var p = Path.Combine(EditorGlobal.GetAssetsPathPrev(), "MyOther");
        return p;
    }

    public static string GetAssetsPathPrev()
    {
        return Application.dataPath + "/..";
    }

    public static string GetAssetsPath(string name)
    {
        return Path.Combine(Application.dataPath, name);
    }

    public static void Refresh()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void CopyToBoard(string content)
    {
        var editor = new TextEditor();
        editor.text = content;
        editor.SelectAll();
        editor.Copy();
    }
}