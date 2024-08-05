using System;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    private int currentBullet;
    private float currentReloadTime;
    private float currentFirerate;

    public WeaponStats weaponStats;

    public UnityEvent OnShoot;
    public UnityEvent OnReload;
    public UnityEvent OnReloadDone;

    public GameObject muzzlePrefab;
    public GameObject bulletPrefab;
    public Transform firePos;
    bool isReloading = true;
    
    private void Awake()
    {
        Reload();
        currentReloadTime = 0;
    }
    void Update()
    {
        ReduceReloadTime();
        ReduceFireRateTime();
    }

    private void ReduceReloadTime()
    {
        currentReloadTime -= Time.deltaTime;
        if (currentReloadTime < 0 && !isReloading)
        {
            LoadStat();
            OnReloadDone?.Invoke();
            isReloading = true;
        }
    }

    private void ReduceFireRateTime()
    {
        currentFirerate -= Time.deltaTime;
        if (currentFirerate > 0) return;
        //OnShoot?.Invoke();
    }
    private void LoadStat()
    {
        currentBullet = weaponStats.bullet;
        currentFirerate = weaponStats.fireRate;
        currentReloadTime = weaponStats.reloadTime;
    }
    public void Shoot(float dir)
    {
        if (currentFirerate > 0||currentBullet <= 0) 
        {
            CheckReLoad();
        }
        else
        {
            if (muzzlePrefab != null)
            {
                GameObject muzzle = Instantiate(muzzlePrefab, firePos.position, firePos.rotation);
                muzzle.transform.SetParent(firePos);
            }
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
                Bullet setBullet = bullet.GetComponent<Bullet>();
                if (setBullet != null)
                {
                    //AudioController.Ins.PlaySound(AudioController.Ins.bullet);
                    OnShoot?.Invoke();
                    setBullet.damage = weaponStats.damage;
                }
            }
            currentBullet--;
            Debug.Log(currentBullet);
            currentFirerate = weaponStats.fireRate;
        }
    }

    private void CheckReLoad()
    {
        if (currentBullet <= 0 && currentReloadTime < 0)
        {
            Reload();
            ReduceReloadTime();
        }
    }

    private void Reload()
    {
        OnReload?.Invoke();
        currentReloadTime = weaponStats.reloadTime;
        isReloading = false;
        ReduceReloadTime();

       // currentBullet = weaponStats.bullet;
    }
    public void SetRotate(float rotate)
    {
        this.transform.eulerAngles = new Vector3(0,0,rotate);
    }
}
