using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine.UI;

public class BreadMakerSystem : GameSystem
{
    private string TargetDir => Application.persistentDataPath;
    private string path1 => TargetDir + "/Data.xlsx";

    public BreadMakerSystem(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        Application.OpenURL(TargetDir);
        CreateExcel();
    }

    void CreateExcel()
    {
        FileInfo newFile = new FileInfo(path1);

        if (newFile.Exists)
        {
            newFile.Delete(); // 确保创建新工作簿  
            newFile = new FileInfo(path1);
        }

        // 所有操作语句要放到using中
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1"); // 添加sheet
            worksheet.DefaultColWidth = 15; // 默认列宽
            worksheet.DefaultRowHeight = 20; // 默认行高
            worksheet.Cells[1, 1].Value = "姓名";
            worksheet.Cells[1, 2].Value = "绰号";
            worksheet.Cells[1, 3].Value = "武功";
            worksheet.Cells[2, 1].Value = "aa11";
            worksheet.Cells[2, 2].Value = "asa11";
            worksheet.Cells[2, 3].Value = "bbb11b";
            package.Save();
        }
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