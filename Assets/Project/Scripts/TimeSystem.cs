using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TimeSystem : GameSystem
{
    public List<double> times = null;

    public TimeSystem(Game game) : base(game)
    {
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

    public double GetTimeStamp()
    {
        return TimeHelper.GetNowTimeStamp();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public void StartPerSecondSendEvent()
    {
        VersionToolData data = Factorys.GetAssetFactory().LoadScriptableObject<VersionToolData>();
        if (data.Debug)
        {
            times = new List<double>()
            {
                10,
                30,
                100,
                200,
                500,
                500,
                500,
                500,
                500,
                500,
            };
        }
        else
        {
            times = new List<double>()
            {
                TimeHelper.Minute * 30,
                TimeHelper.Hour * 12,
                TimeHelper.Day,
                TimeHelper.Day * 2,
                TimeHelper.Day * 4,
                TimeHelper.Day * 7,
                TimeHelper.Day * 15,
                TimeHelper.Month,
                TimeHelper.Month * 3,
                TimeHelper.Month * 6,
            };
        }

        Assert.IsTrue(times.Count == 10);

        InvokeRepeating(nameof(PerSecondSendEvent), 0f, 1f);
    }

    private void PerSecondSendEvent()
    {
        Incident.SendEvent<PerSecondEvent>(new PerSecondEvent()
        {
            timeStamp = GetTimeStamp()
        });
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