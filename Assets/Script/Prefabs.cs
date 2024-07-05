using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs 
{
    public int coin
    {
        get => PlayerPrefs.GetInt(PrefabsConstant.coin, 0);
        set => PlayerPrefs.SetInt(PrefabsConstant.coin, value);
    }
    public string playerData
    {
        get => PlayerPrefs.GetString(PrefabsConstant.playerDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.playerDataKey, value);
    }
    public string enemyData
    {
        get => PlayerPrefs.GetString(PrefabsConstant.enemyDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.enemyDataKey, value);
    }
    public string playerWeapon
    {
        get => PlayerPrefs.GetString(PrefabsConstant.weaponDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.weaponDataKey, value);
    }
    public bool ChecKEnoughCoin(int coinToCheck)
    {
        return coin >= coinToCheck;
    }
}
