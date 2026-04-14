using UnityEngine;

public class AimPivot : MonoBehaviour
{
    public Transform player;
    public float radius = 1f;

    public Vector2 pivotOffset = new Vector2(0, 0.5f); // 👈 height offset

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - player.position).normalized;

        Vector2 basePos = (Vector2)player.position + pivotOffset;

        transform.position = basePos + direction * radius;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}