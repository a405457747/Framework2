using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPurchase
{
    void Buy(int i);
}

public class Purchase : GameSystem
{
    private IPurchase CurPurchase;

    public Purchase(Game game) : base(game)
    {
    }

    public void Buy(int i)
    {
        CurPurchase.Buy(i);
    }
}