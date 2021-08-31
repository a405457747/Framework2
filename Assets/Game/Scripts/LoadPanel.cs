public class LoadPanel : Panel
{
    //auto
    private void Awake()
    {
    }

    public override void EachFrame()
    {
        base.EachFrame();
        Log.LogPrint("loading per ");
    }
}