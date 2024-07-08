using System;
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
       
        while(currentXp >= xpRequireToUgrade && !IsMaxLevel())
        {
            hp += addHpWhenLevelUp * Helper.GetQualityLevelUp(currentlevel);
            currentlevel++;

            xpRequireToUgrade -= xpRequireToUgrade;
            xpRequireToUgrade += qualityXpWhenLevelUp * Helper.GetQualityLevelUp(currentlevel);

            Save();
            onSuccess?.Invoke();
        }
        if(currentlevel < maxLevel || IsMaxLevel()) 
        {
            onFailed?.Invoke();
        }
    }
}
