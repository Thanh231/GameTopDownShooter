using UnityEngine;

public static class Prefabs 
{
    public static int coin
    {
        get => PlayerPrefs.GetInt(PrefabsConstant.coin, 0);
        set => PlayerPrefs.SetInt(PrefabsConstant.coin, value);
    }
    public static string playerData
    {
        get => PlayerPrefs.GetString(PrefabsConstant.playerDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.playerDataKey, value);
    }
    public static string enemyData
    {
        get => PlayerPrefs.GetString(PrefabsConstant.enemyDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.enemyDataKey, value);
    }
    public static string playerWeapon
    {
        get => PlayerPrefs.GetString(PrefabsConstant.weaponDataKey);
        set => PlayerPrefs.SetString(PrefabsConstant.weaponDataKey, value);
    }
    public static bool ChecKEnoughCoin(int coinToCheck)
    {
        return coin >= coinToCheck;
    }
}
