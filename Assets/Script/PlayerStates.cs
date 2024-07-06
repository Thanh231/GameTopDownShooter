using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player Stats" , menuName = "Create Player Stats")]
public class PlayerStates : ActorStats
{
    [Header("Base Level")]
    public int currentlevel;
    public int maxLevel;
    public float xpRequireToUgrade;
    public float currentXp;

    [Header("LevelUp Quality")]
    public float addHpWhenLevelUp;
    public float qualityXpWhenLevelUp;

    public override bool IsMaxLevel()
    {
        return currentlevel >= maxLevel;
    }

    public override void Load()
    {
        if(!string.IsNullOrEmpty(Prefabs.playerData))
        {
            JsonUtility.FromJsonOverwrite(Prefabs.playerData, this);
        }
    }
    public override void Save()
    {
        Prefabs.playerData = JsonUtility.ToJson(this);
    }
    public override void Upgrade(Action onSuccess = null, Action onFailed = null)
    {
        float qualityLevelUp = 0.5f * currentlevel / (2 - 0.5f);
        if(currentXp >= xpRequireToUgrade && !IsMaxLevel())
        {
            hp += addHpWhenLevelUp * qualityLevelUp;
            currentlevel++;

            xpRequireToUgrade -= xpRequireToUgrade;
            xpRequireToUgrade += qualityXpWhenLevelUp * qualityLevelUp;

            Save();
            onSuccess?.Invoke();
        }
        else if(currentlevel < maxLevel || IsMaxLevel()) 
        {
            onFailed?.Invoke();
        }
    }
}
