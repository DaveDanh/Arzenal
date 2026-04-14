using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }        
    }

    void Shoot()
    {
        Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (Vector2)mousePos - (Vector2)rb.position;

        GameObject bullet = Instantiate(bulletPrefab, GameObject.Find("BulletSpawner").transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Launch(direction);
    }
}
