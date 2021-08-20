using UnityEngine;
using System.Collections;
using UnityEngine.U2D;

// 從專案的Resource中,將Unity Asset實體化成GameObject的工廠類別
public class ResourceAssetFactory : IAssetFactory
{
    public const string EffectPath = "";
    public const string AudioPath = "";
    public const string SpritePath = "";

    // 產生特效
    public override GameObject LoadEffect(string assetName)
    {
        return InstantiateGameObject(EffectPath + assetName);
    }

    // 產生AudioClip
    public override AudioClip LoadAudioClip(string clipName)
    {
        UnityEngine.Object res = LoadGameObjectFromResourcePath(AudioPath + clipName);
        if (res == null)
            return null;
        return res as AudioClip;
    }

    // 產生Sprite
    public override Sprite LoadSprite(string spriteName)
    {
        return Resources.Load(SpritePath + spriteName, typeof(Sprite)) as Sprite;
    }

    public override Material LoadMaterial(string name)
    {
        throw new System.NotImplementedException();
    }

    public override Font LoadFont(string name)
    {
        throw new System.NotImplementedException();
    }

    public override TextAsset LoadTextAsset(string name)
    {
        throw new System.NotImplementedException();
    }

    public override SpriteAtlas LoadSpriteAtlas(string name)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject LoadPanel(string name)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject LoadPool(string name)
    {
        throw new System.NotImplementedException();
    }

    // 產生GameObject
    private GameObject InstantiateGameObject(string AssetName)
    {
        // 從Resrouce中載入
        UnityEngine.Object res = LoadGameObjectFromResourcePath(AssetName);
        if (res == null)
            return null;
        return UnityEngine.Object.Instantiate(res) as GameObject;
    }

    // 從Resrouce中載入
    public UnityEngine.Object LoadGameObjectFromResourcePath(string AssetPath)
    {
        UnityEngine.Object res = Resources.Load(AssetPath);
        if (res == null)
        {
            Debug.LogWarning("無法載入路徑[" + AssetPath + "]上的Asset");
            return null;
        }

        return res;
    }
}