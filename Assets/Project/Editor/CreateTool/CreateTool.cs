using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CreateTool
{
    private static void CreateAssetsTxt()
    {
        CrateAssetsTxtFile(".txt");
    }

    private static void CrateAssetsTxtFile(string fileEx, string fileName = "new", string fileContain = "")
    {
        var selectFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        var fileFullName = $"{fileName}{fileEx}";

        var realFolderPath = EditorGlobal.GetAssetsPathAbsolute(selectFolderPath);
        var writePath = $"{Path.Combine(realFolderPath, fileFullName)}";

        CreateTxt(writePath, fileContain);
        EditorGlobal.Refresh();

        var selectFilePath = Path.Combine(selectFolderPath, fileFullName);
        Selection.activeObject = AssetDatabase.LoadAssetAtPath(selectFilePath, typeof(Object));
    }

    private static void CreateTxt(string path, string fileContain = "")
    {
        if (File.Exists(path) == true)
        {
            return;
        }

        var utf8 = new UTF8Encoding(false);
        File.WriteAllText(path, fileContain, utf8);
    }

    [MenuItem("Framework/CreateTool/CreateDirMyOther")]
    private static void CreateDirMyOther()
    {
        var path = EditorGlobal.GetMyOtherPath();
        IOHelper.CreateDir(path);
    }

    [MenuItem("Framework/CreateTool/CreateVersionTxt")]
    private static void CreateVersionTxt()
    {
        var path = EditorGlobal.GetMyOtherVersionPath();
        string content = "<Root>\r\n" + " <Ver Number=\"1\" Explain=\"\" />\r\n" + "</Root>";
        CreateTxt(path, content);
    }
}