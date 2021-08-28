using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

public class OutLog : MonoBehaviour
{
    private static readonly List<string> mLines = new List<string>();
    private static readonly List<string> mWriteTxt = new List<string>();
    private Thread mFileLogThread;
    private object mLogLock;
    private string outpath;

    private void OnGUI()
    {
        //GUILayout.Label($"<size=30>{outpath}</size>");

        GUI.color = Color.red;
        for (int i = 0, imax = mLines.Count; i < imax; ++i) GUILayout.Label(mLines[i]);
    }

    /// <summary>
    ///     也可以手动控制这个手动启动
    /// </summary>
    public void Init()
    {
        mLogLock = new object();
        //Application.persistentDataPath Unity中只有这个路径是既可以读也可以写的。

        if (Directory.Exists(Application.persistentDataPath) == false)
            Directory.CreateDirectory(Application.persistentDataPath);

        outpath = Application.persistentDataPath + "/outLog.txt";
        //每次启动客户端删除之前保存的Log
        if (File.Exists(outpath)) File.Delete(outpath);
        //在这里做一个Log的监听(老方法,已弃用)
        //Application.RegisterLogCallback(HandleLog);

        //用线程刷
        Application.logMessageReceivedThreaded += HandleLog;
        mFileLogThread = new Thread(WriteLog);
        mFileLogThread.Start();

        //一个输出
//        Debug.Log("==============Unity客户端Log日志=========");
    }

    /// <summary>
    ///     线程刷
    /// </summary>
    private void WriteLog()
    {
        while (true)
            //线程锁
            lock (mLogLock)
            {
                if (mWriteTxt.Count > 0)
                {
                    var temp = mWriteTxt.ToArray();
                    foreach (var t in temp)
                    {
                        using (var writer = new StreamWriter(outpath, true, Encoding.UTF8))
                        {
                            writer.WriteLine(t);
                        }

                        mWriteTxt.Remove(t);
                    }
                }
            }
    }

    /// <summary>
    ///     用update来刷（已经弃用）
    /// </summary>
    private void UpdateNotUse()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中哦给你写入文件。
        if (mWriteTxt.Count > 0)
        {
            var temp = mWriteTxt.ToArray();
            foreach (var t in temp)
            {
                using (var writer = new StreamWriter(outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }

                mWriteTxt.Remove(t);
            }
        }
    }

    public static string getHead()
    {
        return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "] ";
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(getHead() + logString);
        if (type == LogType.Error || type == LogType.Exception)
            //Log(logString);
            //Log(stackTrace);
            mWriteTxt.Add("ERROR: " + stackTrace);
    }

    //这里我把错误的信息保存起来，用来输出在手机屏幕上(暂时关闭)
    public static void Log(params object[] objs)
    {
        var text = "";
        for (var i = 0; i < objs.Length; ++i)
            if (i == 0)
                text += objs[i].ToString();
            else
                text += ", " + objs[i];

        if (Application.isPlaying)
        {
            if (mLines.Count > 20) mLines.RemoveAt(0);

            mLines.Add(text);
        }
    }
}