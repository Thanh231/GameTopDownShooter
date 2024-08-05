using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "Create Weapon Stats")]
public class WeaponStats : Stats
{
    [Header("Base Level")]
    public int bullet;
    public float damage;
    public float fireRate;
    public float reloadTime;
    public int currentLevel;
    public int levelMax;

    [Header("Level Up")]
    public int bulletUp;
    public float damageUp;
    public float fireRateUp;
    public float reloadTimeUp;

    [Header("Price")]
    public int priceToUp;
    public int qualityPriceWhenLevelUp;

    [Header("To Avoid Error")]
    public float minFireRate = 0.1f;
    public float minReloadTime = 0.01f;

    public int bulletUpInfo { get => bulletUp * (currentLevel + 1); }
    public float damageUpInfo { get =>  damageUp * Helper.GetQualityLevelUp(currentLevel + 1); }
    public float fireRateUpInfo { get => fireRateUp * Helper.GetQualityLevelUp(currentLevel + 1); }
    public float reloadTimeUpInfo { get => reloadTimeUp * Helper.GetQualityLevelUp(currentLevel + 1); }

    public override bool IsMaxLevel()
    {
        return currentLevel >= levelMax;
    }

    public override void Load()
    {
        if(!string.IsNullOrEmpty(Prefabs.playerWeapon))
        {
            JsonUtility.FromJsonOverwrite(Prefabs.playerWeapon,this);
        }
    }

    public override void Save()
    {
        JsonUtility.ToJson(this);
    }

    public override void Upgrade(Action onSuccess = null, Action onFailed = null)
    {
        if(Prefabs.ChecKEnoughCoin(priceToUp) && !IsMaxLevel())
        {
            Prefabs.coin -= priceToUp;
            currentLevel++;
            bullet += bulletUp * currentLevel;

            fireRate -= fireRateUp * Helper.GetQualityLevelUp(currentLevel);
            fireRate = Mathf.Clamp(fireRate, minFireRate, fireRate);

            reloadTime -= reloadTimeUp * Helper.GetQualityLevelUp(currentLevel);
            reloadTime = Mathf.Clamp(reloadTime, minReloadTime, reloadTime);

            damage += damageUp * Helper.GetQualityLevelUp(currentLevel);

            priceToUp += qualityPriceWhenLevelUp * currentLevel;

            Save();
            onSuccess?.Invoke();

            return;
        }
        onFailed?.Invoke();
    }
}
