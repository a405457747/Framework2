using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OfficeOpenXml;
using UnityEngine.UI;
using Excel;

public class BreadMakerSystem : GameSystem
{
    private string TargetDir => Application.persistentDataPath;
    private string path1 => TargetDir + "/Data.xlsx";

    private List<Bread> datas;

    public BreadMakerSystem(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        Init();
    }

    void Init()
    {
        DirectoryInfo di = new DirectoryInfo(TargetDir);
        if (di.Exists == false)
        {
            di.Create();
        }

        FileInfo dataF = new FileInfo(path1);

        if (dataF.Exists == false)
        {
            WriteExcel(path1);
        }

        ReadExcelPath1();
    }

    public void ReadExcelPath1()
    {
        ReadExcel(path1);
        Incident.SendEvent<ReadSuccessEvent>(new ReadSuccessEvent() {list = datas});
    }

    void OpenTargetDir()
    {
        Application.OpenURL(TargetDir);
    }

    void ReadExcel(string path)
    {
        var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        var result = excelReader.AsDataSet();
        var t = result.Tables[0];

        var collection = t.Rows;
        var row = t.Rows.Count;
        var col = t.Columns.Count;

        datas = new List<Bread>();
        for (int i = 1; i < row; i++)
        {
            Bread b = new Bread();

            b.ID = Convert.ToString(collection[i][0]);
            b.Title = Convert.ToString(collection[i][1]);
            b.Content = Convert.ToString(collection[i][2]);
            var dataStr = collection[i][3].ToString();
            if (string.IsNullOrEmpty(dataStr))
            {
            }
            else
            {
                b.ProducedDate = Convert.ToDouble(dataStr);
            }

            datas.Add(b);
        }

        stream.Close();
        excelReader.Close();
    }

    void WriteExcel(string path)
    {
        FileInfo newFile = new FileInfo(path);

        // 所有操作语句要放到using中
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1"); // 添加sheet
            worksheet.DefaultColWidth = 15; // 默认列宽
            worksheet.DefaultRowHeight = 20; // 默认行高
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Title";
            worksheet.Cells[1, 3].Value = "Content";
            worksheet.Cells[1, 4].Value = "ProducedDate";

            if (datas == null)
            {
                datas = new List<Bread>();
            }

            for (var index = 0; index < datas.Count; index++)
            {
                var bread = datas[index];
                worksheet.Cells[(index + 2), 1].Value = Convert.ToString(bread.ID);
                worksheet.Cells[(index + 2), 2].Value = Convert.ToString(bread.Title);
                worksheet.Cells[(index + 2), 3].Value = Convert.ToString(bread.Content);
                worksheet.Cells[(index + 2), 4].Value = Convert.ToString(bread.ProducedDate);
            }

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