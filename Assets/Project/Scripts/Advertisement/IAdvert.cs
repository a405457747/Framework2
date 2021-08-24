using System.Collections;
using System.Collections.Generic;
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
