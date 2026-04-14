using UnityEngine;

public class Hand : MonoBehaviour
{
    private Transform playerTrans;
    private float offsetDistance = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        playerTrans = playerObj.transform;

        // gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (Vector2)mousePos - (Vector2)playerTrans.position;
        
        gameObject.transform.position = (Vector2)playerTrans.position + direction.normalized * offsetDistance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log("Angle: " + angle);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }

}
