using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
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
    
    private void Awake()
    {
        LoadStat();
        currentReloadTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ReduceReloadTime();
        ReduceFireRateTime();
    }

    private void ReduceReloadTime()
    {
        currentReloadTime -= Time.deltaTime;
        if (currentReloadTime > 0 ) return;

        LoadStat();
        OnReloadDone?.Invoke();
    }

    private void ReduceFireRateTime()
    {
        currentFirerate -= Time.deltaTime;
        if (currentFirerate > 0) return;
        OnShoot?.Invoke();
    }
    private void LoadStat()
    {
        currentBullet = weaponStats.bullet;
        currentFirerate = weaponStats.fireRate;
        currentReloadTime = weaponStats.reloadTime;
    }
    public void Shoot(Vector2 dir)
    {
        if (currentFirerate > 0||currentBullet < 0) return;
        if(muzzlePrefab != null)
        {
            Instantiate(muzzlePrefab, firePos.position, transform.rotation);
        }
        if(bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
            Bullet setBullet = bullet.GetComponent<Bullet>();
            if (setBullet != null)
            {
                setBullet.damage = weaponStats.damage;
                setBullet.SetDirection(dir);
            }
        }
        currentBullet--;
        currentFirerate = weaponStats.fireRate;
        if(currentBullet < 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        OnReload?.Invoke();
        currentReloadTime = weaponStats.reloadTime;
    }
    public void SetRotate(float rotate)
    {
        this.transform.eulerAngles = new Vector3(0,0,rotate);
    }
}
