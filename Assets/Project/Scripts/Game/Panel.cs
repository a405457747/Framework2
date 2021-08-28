using DG.Tweening;
using UnityEngine;

public abstract class PanelArgs
{
}

[RequireComponent(typeof(HaveEvents))]
public abstract class Panel : MonoBehaviour
{
    protected Tween openTween;
    protected Game game;
    protected PanelTier tier;

    public virtual void Initialize(PanelArgs arguments)
    {
        game = FindObjectOfType<Game>();

        transform.SetParent(Game.CanvasTrans.Find(tier.ToString()), false);
    }

    public virtual void Open(PanelArgs arguments)
    {
    }

    public virtual void Close()
    {
    }

    public virtual void Release()
    {
    }

    public virtual void EachFrame()
    {
    }
}