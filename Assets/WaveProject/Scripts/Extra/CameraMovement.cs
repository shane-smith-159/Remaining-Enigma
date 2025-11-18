using UnityEngine;

public class CameraMovement : MonoBehaviour
{
     GameObject player;
    [SerializeField] Collider2D boundBox; // can use tilemaps as well
    private float halfHeight;
    private float halfWidth;

    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        boundBox = GetComponent<Collider2D>();

    }
    private void Start()
    
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(player.transform.position.x, boundBox.bounds.min.x + halfWidth, boundBox.bounds.max.x - halfWidth);
        float clampedY = Mathf.Clamp(player.transform.position.y, boundBox.bounds.min.y + halfHeight, boundBox.bounds.max.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
