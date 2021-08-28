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
    private string path2 => TargetDir + "/DataBackup.xlsx";

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
            WriteExcel(path1, false);
        }

        this.Delay(3f, () =>
        {
            var temp = new FileInfo(path1);
            game.OpenTipPanel(new TipPanelArgs() {mainTip = "初始化成功" + temp.Exists + path1});

            ReadExcelPath1();

            game._timeSystem.StartPerSecondSendEvent();
        });
    }

    private void PerSecondEventCallback(PerSecondEvent obj)
    {
    }

    public void ReadExcelPath1()
    {
        ReadExcel(path1);
        if (datas == null)
        {
            return;
        }

        Incident.SendEvent<ReadSuccessEvent>(new ReadSuccessEvent() {list = datas});
    }

    public void WriteExcelPath1()
    {
        WriteExcel(path1, true);
        game.OpenTipPanel(new TipPanelArgs() {mainTip = "执行成功了，请编辑Content和Title所对应的单元格(其余不需要编辑)，然后重新启动应用吧！"});
        this.Delay(3f, () => { OpenTargetDir(); });
    }

    public void WriteExcelPath2()
    {
        WriteExcel(path2, true);
        game.OpenTipPanel(new TipPanelArgs() {mainTip = "执行成功了，如需要使用备份，把DataBackup重命名为Data，然后重新启动应用吧！"});
        this.Delay(3f, () => { OpenTargetDir(); });
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

            datas = new List<Bread>();
            for (int i = 1; i < row; i++)
            {
                Bread b = new Bread();

                b.ID = Convert.ToString(collection[i][0]);
                b.Title = Convert.ToString(collection[i][1]);
                b.Content = Convert.ToString(collection[i][2]);
                var pStr = collection[i][3].ToString();
                if (string.IsNullOrEmpty(pStr))
                {
                    b.ProducedDate = game._timeSystem.GetTimeStamp();
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

                datas.Add(b);
            }

            stream.Close();
            excelReader.Close();
        }
        catch (Exception e)
        {
            game.OpenTipPanel(new TipPanelArgs() {mainTip = "发生错误，请先关闭Excel，再重试" + e.ToString()});
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
                    worksheet.Cells[(index + 2), 5].Value = Convert.ToString(bread.Ripe);
                }

                package.Save();
            }
        }
        catch (Exception e)
        {
            game.OpenTipPanel(new TipPanelArgs() {mainTip = "发生错误，请先关闭Excel，再重试"});
        }
    }


    public override void Release()
    {
        base.Release();
    }

//auto
    private void Awake()
    {
        Incident.DeleteEvent<PerSecondEvent>(PerSecondEventCallback);
        Incident.RigisterEvent<PerSecondEvent>(PerSecondEventCallback);
    }
}