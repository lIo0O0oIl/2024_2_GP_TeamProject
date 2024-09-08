using UnityEngine;

public class FishMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction; 

    private Camera camera;
    private float objWidth; 

    void Start()
    {
        camera = Camera.main;

        direction = new Vector2(Random.Range(-1f, 1f), 0).normalized;

        objWidth = transform.localScale.x / 2f; 
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        CheckBounds();
    }

    void CheckBounds()
    {
        Vector2 viewportPos = camera.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0 + (objWidth / camera.orthographicSize) ||
            viewportPos.x > 1 - (objWidth / camera.orthographicSize))
        {
            direction.x = -direction.x;
        }
    }
}
