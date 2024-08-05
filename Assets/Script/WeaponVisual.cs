using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponVisual : MonoBehaviour
{
    public void OnShoot()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.bullet);
    }

    public void OnReload()
    {
        GUIManager.Ins.ShowReloadTxt(true);
    }
    public void OnReloadDone()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.reload);

        GUIManager.Ins.ShowReloadTxt(false);
    }

}
