using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : ActorVisual
{
    [SerializeField] private GameObject m_deathVfx;
    private Player player;
    private PlayerStates playerStates;
    private void Start()
    {
        player = (Player)m_actor;
        playerStates = player.PlayerStates;
    }
    public override void OnTakeDamage()
    {
        base.OnTakeDamage();
        GUIManager.Ins.UpdateHpInfo(m_actor.CurrentHP,m_actor.statsData.hp);
    }
    public void OnLostLife()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.lostLife);
        GUIManager.Ins.UpdateLifeInfo(GameManager.Ins.Currentlife);
    }
    public void OnDead()
    {
        if(m_deathVfx)
        {
            Instantiate(m_deathVfx,transform.position, Quaternion.identity);
        }

        AudioController.Ins.PlaySound(AudioController.Ins.playerDeath);
        GUIManager.Ins.ShowGameOverDialog();

    }
    public void OnAddXp()
    {
        if (playerStates == null) return;
        GUIManager.Ins.UpdateLevelInfo(playerStates.currentlevel, playerStates.currentXp, playerStates.xpRequireToUgrade);

    }
    public void OnLevelUp()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.levelUp);
    }
}
