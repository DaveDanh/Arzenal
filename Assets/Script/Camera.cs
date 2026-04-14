using UnityEngine;

public class Camera : MonoBehaviour
{

    private GameObject player;
    private int offset = -10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, offset);
    }
}
