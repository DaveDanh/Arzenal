using UnityEngine;

public class AimPivot : MonoBehaviour
{
    public Transform player;
    public Vector2 pivotOffset = new Vector2(0, 0.5f);

    public Transform armVisual;

    void Update()
    {
        // keep pivot anchored to player shoulder
        transform.position = (Vector2)player.position + pivotOffset;

        // mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // direction
        Vector2 direction = mousePos - transform.position;

        // angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate pivot
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 🔥 FIX: flip when aiming left
        if (angle > 90f || angle < -90f)
        {
            armVisual.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            armVisual.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}