using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    private Rigidbody2D rb;

    private Vector2 movement;

    public Animator anim;
    public SpriteRenderer bodySprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // animation
        bool isMoving = movement.magnitude > 0.01f;
        anim.SetBool("isMoving", isMoving);

        // mouse facing logic
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 dir = (mousePos - transform.position).normalized;

        bodySprite.flipX = dir.x > 0;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * PlayerSpeed;
    }
}