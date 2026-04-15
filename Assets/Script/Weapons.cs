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

    [Header("Weapon Stats")]
    public FireMode fireMode = FireMode.Single;
    public float fireRate = 5f;
    public float bulletSpeed = 12f;

    private float nextFireTime;

    void Update()
    {
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
    }
}