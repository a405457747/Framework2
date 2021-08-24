using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AdvertSystem : GameSystem
{
    private IAdvert CurAdvert = new GoogleAdvert();

    public AdvertSystem(Game game) : base(game)
    {
    }

    public void LoadBanner()
    {
        CurAdvert.LoadBanner();
    }

    public void LoadInsert()
    {
        CurAdvert.LoadInsert();
    }

    public void LoadAward()
    {
        CurAdvert.LoadAward();
    }

    public void LoadInsertAward()
    {
        CurAdvert.LoadInsertAward();
    }

    public bool BannerReady()
    {
        return CurAdvert.BannerReady();
    }

    public bool InsertReady()
    {
        return CurAdvert.InsertReady();
    }

    public bool AwardReady()
    {
        return CurAdvert.AwardReady();
    }

    public bool InsertAwardReady()
    {
        return CurAdvert.InsertAwardReady();
    }

    public void BannerPlay()
    {
        CurAdvert.BannerPlay();
    }

    public void InsertPlay()
    {
        CurAdvert.InsertPlay();
    }

    public void AwardPlay()
    {
        CurAdvert.AwardPlay();
    }

    public void InsertAwardPlay()
    {
        CurAdvert.InsertAwardPlay();
    }

    public override void Initialize()
    {
        base.Initialize();
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
