using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PoolSystem : GameSystem
{
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    public override void Initialize()
    {
        base.Initialize();
    }

    public GameObject Spawn(string goName)
    {
        if (!_pools.ContainsKey(goName))
        {
            var tempPool = new Pool();
            _pools.Add(goName, tempPool);
        }

        return _pools[goName].Spawn(goName);
    }

    public void Recycle(GameObject obj)
    {
        var goName = obj.name;
        if (_pools.ContainsKey(goName))
            _pools[goName].Recycle(obj);
        else
            Log.LogException(new KeyNotFoundException());
    }

    public PoolSystem(Game game) : base(game)
    {
    }
//auto
   private void Awake()
	{
		
        
	}
	
        
}
