using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public enum FireMode
    {
        Single,
        Auto
    }

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Animator animator;

    [Header("Muzzle Flash")]
    public GameObject muzzleFlash;
    public float muzzleFlashDuration = 0.05f;

    [Header("Weapon Stats")]
    public FireMode fireMode = FireMode.Single;
    public float fireRate = 5f;
    public float bulletSpeed = 12f;

    private float nextFireTime;
    private Coroutine muzzleFlashRoutine;

    [Header("Ammo && Reload")]
    public int magazineSize = 15;
    public int reloadTime = 2;
    public int currentAmmo;
    public static bool isReloading = false;
    public bool isAmmoInfinity = true;
    public int totalAmmo;


    void Start(){
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (isReloading){
            return;
        }
        if ((Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize) || (currentAmmo == 0))
        {
            StartCoroutine(ReloadRoutine());
            return;
        }
        bool wantsToFire =
            fireMode == FireMode.Auto
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (wantsToFire)
        {
            TryFire();
        }
    }

    void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Weapons: bulletPrefab is missing.");
            return;
        }

        if (bulletSpawn == null)
        {
            Debug.LogWarning("Weapons: bulletSpawn is missing.");
            return;
        }

        Fire();
        nextFireTime = Time.time + (1f / fireRate);
    }

    void Fire()
    {

        currentAmmo--;

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("Weapons: No Main Camera found.");
            return;
        }

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = ((Vector2)mousePos - (Vector2)bulletSpawn.position).normalized;

        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Launch(direction * bulletSpeed);
        }
        else
        {
            Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("Weapons: spawned bullet has no Bullet script and no Rigidbody2D.");
            }
        }

        if (animator != null)
        {
            animator.SetTrigger("Recoil");
        }

        ShowMuzzleFlash(direction);
    }

    void ShowMuzzleFlash(Vector2 direction)
    {
        if (muzzleFlash == null)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        muzzleFlash.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (muzzleFlashRoutine != null)
        {
            StopCoroutine(muzzleFlashRoutine);
        }

        muzzleFlashRoutine = StartCoroutine(MuzzleFlashRoutine());
    }

    IEnumerator MuzzleFlashRoutine()
    {
        muzzleFlash.SetActive(true);

        yield return new WaitForSeconds(muzzleFlashDuration);

        muzzleFlash.SetActive(false);
        muzzleFlashRoutine = null;
    }

    IEnumerator ReloadRoutine(){
        isReloading = true;
        float elapsedTime = 0;

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            
            //animation reload
            
            yield return null;
        }

        if (isAmmoInfinity || totalAmmo >= magazineSize){
            currentAmmo = magazineSize;
            isReloading = false;
        }
        else {
        currentAmmo = totalAmmo;
        isReloading = false;
        }
    }
}