using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Experimental.Rendering;

public class Weapon : MonoBehaviour
{
    internal Animator anim;
    public bool isActiveWeapon;
    public bool isAimDownSight;

    [Header("Shooting")]
    private bool isShooting;
    private bool readyToShoot;
    public float shootingDelay = 0.2f;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 500f;
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private Transform bulletSpawn;
    private float burstBulletLeft;

    [Header("Burst")]
    public int bulletPerBurst;
    public int magazineSize;
    public int bulletLefts;


    [Header("Spread")]
    public float spreadIntensity;
    public float hipSpreadIntensity;
    public float addSpreadIntensity;

    [Header("Effect")]
    [SerializeField] private GameObject muzzleEffect;

    [Header("Reload")]
    public float reloadTime;
    private bool isReloading;

    [Header("SpawnTransform")]
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto,
    }
    public ShootingMode currentShootingMode;

    public enum WeaponModel
    {
        M249,
        M1911,
    }
    public WeaponModel thisWeaponModel;

    void Awake()
    {
        anim = GetComponent<Animator>();
        bulletLefts = magazineSize;
        burstBulletLeft = bulletPerBurst;
        readyToShoot = true;
        spreadIntensity = hipSpreadIntensity;
    }


    void Update()
    {

        if (isActiveWeapon)
        {
            //Aim Down Sight
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (!isAimDownSight)
                {
                    EnterAds();
                }

                else
                {
                    ExitAds();
                }
            }

            // ShootingMode
            HandleInputShooting();

            // Empty Sound
            if (isShooting && bulletLefts == 0)
            {
                SoundManager.Instance.emptyClip.Play();
            }

            //Fire called
            if (readyToShoot && isShooting && bulletLefts > 0 && !isReloading)
            {
                Fire();
                readyToShoot = false;
                isShooting = false;
            }

            //Reload
            if (Input.GetKey(KeyCode.R) && bulletLefts < magazineSize && !isReloading && WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) > 0)
            {
                Reload();
                SoundManager.Instance.PlayReloadSound(thisWeaponModel);
            }
        }

    }

    private void EnterAds()
    {
        anim.SetTrigger("ENTERADS");
        isAimDownSight = true;
        HUDManager.Instance.midPoint.enabled = false;
        spreadIntensity = addSpreadIntensity;
    }

    private void ExitAds()
    {
        anim.SetTrigger("EXITADS");
        isAimDownSight = false;
        HUDManager.Instance.midPoint.enabled = true;
        spreadIntensity = hipSpreadIntensity;
    }

    void HandleInputShooting()
    {
        if (currentShootingMode == ShootingMode.Burst || currentShootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
    }

    private void Reload()
    {
        isReloading = true;
        if (isAimDownSight)
        {
            anim.SetTrigger("RELOADADS");

        }
        else
        {
            anim.SetTrigger("RELOAD");
        }

        Invoke("ReloadComplete", reloadTime);
    }

    private void ReloadComplete()
    {
        if (WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel) + bulletLefts > magazineSize)
        {
            int bulletToDecrease = magazineSize - bulletLefts;
            bulletLefts = magazineSize;

            WeaponManager.Instance.DecreaseTotalAmmo(bulletToDecrease, thisWeaponModel);
        }
        else
        {
            int bulletToDecrease = WeaponManager.Instance.CheckAmmoLeftFor(thisWeaponModel);
            bulletLefts += bulletToDecrease;

            WeaponManager.Instance.DecreaseTotalAmmo(bulletToDecrease, thisWeaponModel);
        }

        isReloading = false;
    }

    private void Fire()
    {
        bulletLefts--;

        if (isAimDownSight)
        {
            anim.SetTrigger("RECOILADS");
        }
        else
        {
            anim.SetTrigger("RECOIL");
        }

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CaculateDirectionAndSpread().normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterLifeTime(bullet, bulletLifeTime));

        if (currentShootingMode == ShootingMode.Burst)
        {
            burstBulletLeft--;
            if (burstBulletLeft > 0)
            {
                Fire();
            }
            else
            {
                burstBulletLeft = bulletPerBurst;
                Invoke("ResetShoot", shootingDelay);
            }
        }
        else
        {
            Invoke("ResetShoot", shootingDelay);
        }

    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    public Vector3 CaculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float z = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(0, y, z);
    }

    IEnumerator DestroyBulletAfterLifeTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
