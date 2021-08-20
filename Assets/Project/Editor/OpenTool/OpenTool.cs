/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using UnityEditor;
using UnityEngine;

public class OpenTool
{
    [MenuItem("Framework/OpenTool/OpenPersistentDataPath")]
    public static void OpenPersistentDataPath()
    {
        var path = Application.persistentDataPath;

        Application.OpenURL(path);
    }
    [MenuItem("Framework/OpenTool/OpenMyOther")]
    public static void OpenMyOther()
    {
        Application.OpenURL(EditorGlobal.GetMyOtherPath());
    }
}