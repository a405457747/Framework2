public class PurchaseSystem : GameSystem
{
    private readonly IPurchase CurPurchase = new UnityIAP();

    public PurchaseSystem(Game game) : base(game)
    {
    }

//auto
    private void Awake()
    {
    }

    public void Buy(int i)
    {
        CurPurchase.Buy(i);
    }
}