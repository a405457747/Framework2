/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using UnityEditor;
using Object = UnityEngine.Object;

public class ExcelTool : EditorWindow
{
    public static readonly string ReadExcelPath = @"C:\Users\SkyAllen\Downloads";

    [MenuItem("Framework/ExcelTool/QuickTest")]
    public static void QuickTest()
    {
        ReadExcel("t1");
    }

    public static Dictionary<string, CreateFieldType> BigDic = new Dictionary<string, CreateFieldType>();

    private static void ReadExcel(string name, int sheetIndex = 0)
    {
        BigDic.Clear();

        string excelFullPath = $"{ReadExcelPath}/{name}.xlsx";

        var stream = File.Open(excelFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        var result = excelReader.AsDataSet();
        var t = result.Tables[sheetIndex];
        var collection = t.Rows;
        int row = t.Rows.Count;
        int col = t.Columns.Count;

        List<string> content = new List<string>();
        for (int c = 0; c < col; c++)
        {
            content.Add(collection[0][c].ToString());
        }


        Log.LogParas(t.Rows[1][3].ToString());


        Log.LogParas(t.Columns.Count, t.Rows.Count);
    }

    private static void FillBigDic(List<string> contents)
    {
    }

    private static void CreateCS(List<string> field)
    {
        foreach (var f in field)
        {
        }
    }

    private static CreateFieldType GetType(string field)
    {
        CreateFieldType res = default;
        return res;
    }
}

public enum CreateFieldType
{
    Null,
    Int,
    String,
    Float,
    Bool,
}