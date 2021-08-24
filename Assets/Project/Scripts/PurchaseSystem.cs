using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PurchaseSystem : GameSystem
{
    private IPurchase CurPurchase = new UnityIAP();

    public PurchaseSystem(Game game) : base(game)
    {
    }

    public void Buy(int i)
    {
        CurPurchase.Buy(i);
    }
//auto
   private void Awake()
	{
		
        
	}
	
        
}
