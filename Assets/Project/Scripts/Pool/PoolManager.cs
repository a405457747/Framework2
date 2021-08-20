using System.Collections.Generic;
using UnityEngine;

public class PoolManager : GameSystem
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

    public PoolManager(Game game) : base(game)
    {
    }
}

public interface IPool
{
    void OnSpawn();
    void OnRecycle();
}

public class Pool
{
    private readonly Queue<GameObject> _objs;

    public Pool()
    {
        _objs = new Queue<GameObject>();
    }

    public GameObject Spawn(string name)
    {
        GameObject tempGo;
        if (_objs.Count == 0)
        {
            tempGo = Object.Instantiate(Factorys.GetAssetFactory().LoadPanel(name));
            tempGo.Name(name);
        }
        else
        {
            tempGo = _objs.Dequeue();
        }

        tempGo.SetActive(true);
        tempGo.SendMessage("OnSpawn", SendMessageOptions.RequireReceiver);

        return tempGo;
    }

    public void Recycle(GameObject obj)
    {
        obj.SendMessage("OnRecycle");
        obj.SetActive(false);

        if (_objs.Contains(obj))
            Log.LogError("Already existed.");
        else
            _objs.Enqueue(obj);
    }
}
