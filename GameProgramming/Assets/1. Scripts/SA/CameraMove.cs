using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject background;

    private Camera mainCamera;
    private float halfHeight;
    private float halfWidth;
    public float speed;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;

        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;

        SpriteRenderer backgroundSprite = background.GetComponent<SpriteRenderer>();
        float backgroundWidth = backgroundSprite.bounds.size.x;
        float backgroundHeight = backgroundSprite.bounds.size.y;

        minX = background.transform.position.x - backgroundWidth / 2f + halfWidth;
        maxX = background.transform.position.x + backgroundWidth / 2f - halfWidth;
        minY = background.transform.position.y - backgroundHeight / 2f + halfHeight;
        maxY = background.transform.position.y + backgroundHeight / 2f - halfHeight;
    }

    void LateUpdate()
    {
        MoveCamera();
        ClampCamera();
    }

    private void MoveCamera()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        mainCamera.transform.Translate(new Vector3(moveX, moveY, 0f));
    }

    private void ClampCamera()
    {
        Vector3 camera = mainCamera.transform.position;

        float clampedX = Mathf.Clamp(camera.x, minX, maxX);
        float clampedY = Mathf.Clamp(camera.y, minY, maxY);

        mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);
    }


    //[SerializeField] private float speed;

    //private void Update()
    //{
    //    float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    //    float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

    //    Camera.main.transform.Translate(new Vector3(moveX, moveY, 0f));
    //}

    //private void clamp()
    //{

    //}
}
