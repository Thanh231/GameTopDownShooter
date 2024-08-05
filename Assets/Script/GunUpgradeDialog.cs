using UnityEngine;
using UnityEngine.UI;

public class GunUpgradeDialog : Dialog
{
    [SerializeField] private GunStatsUI bulletStatsUI;
    [SerializeField] private GunStatsUI damageStatsUI;
    [SerializeField] private GunStatsUI firerateStatsUI;
    [SerializeField] private GunStatsUI reloadStatsUI;

    [SerializeField] private Text upgradeButtonTxt;

    private Weapon weapon;
    private WeaponStats weaponStats;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0f;
        weapon = GameManager.Ins.Player.weapon;
        weaponStats = weapon.weaponStats;
        UpdateUI();
    }
    private void UpdateUI()
    {
        if(weapon == null || weaponStats == null) return;

        if (titleTxt) titleTxt.text = $"LEVEL {weaponStats.currentLevel.ToString("00")}";
        if (upgradeButtonTxt) upgradeButtonTxt.text = $"UP[${weaponStats.priceToUp.ToString("n0")}]";

        if(bulletStatsUI)
        {
            bulletStatsUI.UpdateStat(
                "Bullets : ",
                weaponStats.bullet.ToString("n0"),
                $"( +{weaponStats.bulletUpInfo.ToString("n0")})"
            );
        }
        if (damageStatsUI)
        {
            damageStatsUI.UpdateStat(
                "Damage : ",
                weaponStats.damage.ToString("F2"),
                $"( +{weaponStats.damageUpInfo.ToString("F3")})"
            );
        }
        if (firerateStatsUI)
        {
            firerateStatsUI.UpdateStat(
                "Firerate : ",
                weaponStats.fireRate.ToString("F2"),
                $"( -{weaponStats.fireRateUpInfo.ToString("F3")})"
            );
        }
        if (reloadStatsUI)
        {
            reloadStatsUI.UpdateStat(
                "Reload : ",
                weaponStats.reloadTime.ToString("F2"),
                $"( -{weaponStats.reloadTimeUpInfo.ToString("F3")})"
            );
        }
    }
    public void Upgrade()
    {
        if(weapon == null) return;
        weaponStats.Upgrade(OnUpgradeSuccess,OnUpgradeFail);
    }
    private void OnUpgradeSuccess()
    {
        UpdateUI();
        GUIManager.Ins.UpdateCoinCounting(Prefabs.coin);
        AudioController.Ins.PlaySound(AudioController.Ins.upgradeSuccess);
    }
    private void OnUpgradeFail()
    {
        Debug.Log("Failed!!!");
    }
    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }
}
