using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 10f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     mousePos.z = 0f;
        //     Vector2 direction = (Vector2)mousePos - rb.position;
        //     SetDirection(direction);
        //     Destroy(gameObject, 5f);
        // }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
