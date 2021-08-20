/*⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵
☠ ©2020 Chengdu Mighty Vertex Games. All rights reserved.                                                                        
⚓ Author: SkyAllen                                                                                                                  
⚓ Email: 894982165@qq.com                                                                                  
⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵⛵*/

using UnityEngine;

public enum ADType
{
    Null,
    Banner,
    Insert,
    Award,
    InsertAward,
}

public interface IAdvert
{
    void LoadBanner();
    void LoadInsert();
    void LoadAward();
    void LoadInsertAward();

    bool BannerReady();
    bool InsertReady();
    bool AwardReady();
    bool InsertAwardReady();

    void BannerPlay();
    void InsertPlay();
    void AwardPlay();
    void InsertAwardPlay();
}

public class Advert : GameSystem
{
    private IAdvert CurAdvert;

    public Advert(Game game) : base(game)
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
       return  CurAdvert.BannerReady();
    }

    public bool InsertReady()
    {
        return  CurAdvert.InsertReady();
    }

    public bool AwardReady()
    {
        return  CurAdvert.AwardReady();
    }

    public bool InsertAwardReady()
    {
        return  CurAdvert.InsertAwardReady();
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
}