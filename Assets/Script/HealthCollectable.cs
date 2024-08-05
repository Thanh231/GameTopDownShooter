using UnityEngine;

public class HealthCollectable : Collectable
{
    public override void Trigger()
    {
        if(player != null)
        {
            player.CurrentHP += bonus;

            player.CurrentHP = Mathf.Clamp(player.CurrentHP,0,player.statsData.hp);

            GUIManager.Ins.UpdateHpInfo(player.CurrentHP,player.PlayerStates.hp);

            AudioController.Ins.PlaySound(AudioController.Ins.healthPickup);
        }
    }
}
