using UnityEngine;

public class BugMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    private Camera camera;
    private float objWidth;  
    private float objHeight; 

    void Start()
    {
        camera = Camera.main;

        direction = Random.insideUnitCircle.normalized;

        objWidth = transform.localScale.x / 2f;  
        objHeight = transform.localScale.y / 2f; 
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
            RandomDir();
        }

        if (viewportPos.y < 0 + (objHeight / camera.orthographicSize) ||
            viewportPos.y > 1 - (objHeight / camera.orthographicSize))
        {
            direction.y = -direction.y;
            RandomDir();
        }
    }

    void RandomDir()
    {
        float angle = Random.Range(-30f, 30f);
        direction = Quaternion.Euler(0, 0, angle) * direction;
        direction.Normalize();
    }
}
