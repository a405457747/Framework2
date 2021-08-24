using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadPanel : Panel
{
	public override void EachFrame()
	{
		base.EachFrame();
		Log.LogPrint("loading per ");
	}

	//auto
   private void Awake()
	{
		
        
	}
	
        
}
