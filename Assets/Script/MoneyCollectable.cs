public class MoneyCollectable : Collectable
{
    public override void Trigger()
    {
        Prefabs.coin += bonus;

        GUIManager.Ins.UpdateCoinCounting(Prefabs.coin);

        AudioController.Ins.PlaySound(AudioController.Ins.coinPickup);
    }
}
