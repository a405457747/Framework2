using DG.Tweening;
using UnityEngine;

public abstract class PanelArgs
{
}

[RequireComponent(typeof(HaveEvents))]
public abstract class Panel : MonoBehaviour
{
    protected Tween openTween;

    public virtual void Initialize(PanelArgs arguments)
    {
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