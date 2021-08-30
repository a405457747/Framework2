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
    private string DataPath => TargetDir + "/Data.xlsx";
    private string DataBackupPath => TargetDir + "/DataBackup.xlsx";

    private List<Bread> _breads = new List<Bread>();
    public List<double> times = new List<double>();

    public BreadMakerSystem(Game game) : base(game)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        if (Directory.Exists(TargetDir) == false)
        {
            Directory.CreateDirectory(TargetDir);
        }

        if (File.Exists(DataPath) == false)
        {
            WriteExcel(DataPath, false);
        }

        ReadExcel(DataPath);
    }

    public bool CanInteraction(Bread bread, double timeStamp)
    {
        double p = bread.ProducedDate;
        int ripe = bread.Ripe;

        double interval = timeStamp - p;

        int level = 0;
        for (int i = 0; i < times.Count; i++)
        {
            var per = times[i];

            if (interval >= per)
            {
                level += 1;
            }
            else
            {
                break;
            }
        }

        return level >= ripe;
    }


    private double GetTimeStamp()
    {
        return TimeHelper.GetNowTimeStamp();
    }
    
    public void WriteData()
    {
        WriteExcel(DataPath, true);
    }

    public void WriteDataBackup()
    {
        WriteExcel(DataBackupPath, true);
    }

    void OpenTargetDir()
    {
        Application.OpenURL(TargetDir);
    }

    void ReadExcel(string path)
    {
        try
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();
            var t = result.Tables[0];

            var collection = t.Rows;
            var row = t.Rows.Count;
            var col = t.Columns.Count;

            _breads = new List<Bread>();
            for (int i = 1; i < row; i++)
            {
                Bread b = new Bread();

                b.ID = Convert.ToString(collection[i][0]);
                b.Title = Convert.ToString(collection[i][1]);
                b.Content = Convert.ToString(collection[i][2]);
                var pStr = collection[i][3].ToString();
                if (string.IsNullOrEmpty(pStr))
                {
                    b.ProducedDate = GetTimeStamp();
                }
                else
                {
                    b.ProducedDate = Convert.ToDouble(pStr);
                }

                var rStr = collection[i][4].ToString();
                if (string.IsNullOrEmpty(rStr))
                {
                    b.Ripe = 1;
                }
                else
                {
                    b.Ripe = Convert.ToInt32(rStr);
                }

                _breads.Add(b);
            }

            stream.Close();
            excelReader.Close();
        }
        catch (Exception e)
        {
            //  game.OpenPanel<TipPanel>(new TipPanelArgs() {mainTip = "发生错误，请先关闭Excel，再重试" + e.ToString()});
        }
    }

    void WriteExcel(string path, bool isDeleteOrigin)
    {
        try
        {
            FileInfo newFile = new FileInfo(path);

            if (isDeleteOrigin)
            {
                if (newFile.Exists == true)
                {
                    newFile.Delete();
                }
            }

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
                worksheet.Cells[1, 5].Value = "Ripe";

                if (_breads == null)
                {
                    _breads = new List<Bread>();
                }

                for (var index = 0; index < _breads.Count; index++)
                {
                    var bread = _breads[index];
                    worksheet.Cells[(index + 2), 1].Value = Convert.ToString(bread.ID);
                    worksheet.Cells[(index + 2), 2].Value = Convert.ToString(bread.Title);
                    worksheet.Cells[(index + 2), 3].Value = Convert.ToString(bread.Content);
                    worksheet.Cells[(index + 2), 4].Value = Convert.ToString(bread.ProducedDate);
                    worksheet.Cells[(index + 2), 5].Value = Convert.ToString(bread.Ripe);
                }

                package.Save();
            }
        }
        catch (Exception e)
        {
            //game.OpenPanel<TipPanel>(new TipPanelArgs() {mainTip = "发生错误，请先关闭Excel，再重试" + e.ToString()});
        }
    }

//auto
    private void Awake()
    {
    }
}