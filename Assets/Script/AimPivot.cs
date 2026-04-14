using UnityEngine;

public class AimPivot : MonoBehaviour
{
    public Transform player;
    public Vector2 pivotOffset = new Vector2(0, 0.5f); // shoulder position

    void Update()
    {
        // keep pivot anchored to player shoulder
        transform.position = (Vector2)player.position + pivotOffset;

        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // direction from pivot → mouse
        Vector2 direction = mousePos - transform.position;

        // rotate pivot
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}