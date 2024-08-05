public class LifeCollectable : Collectable
{
    public override void Trigger()
    {
        GameManager.Ins.Currentlife += bonus;

        GUIManager.Ins.UpdateLifeInfo(GameManager.Ins.Currentlife);

        AudioController.Ins.PlaySound(AudioController.Ins.lifePickup);
    }
}
