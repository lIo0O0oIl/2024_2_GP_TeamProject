using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnim : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite, changeSprite;

    private float currentTime, changeTime;
    [SerializeField] private float minTime = 1.0f, maxTime = 3.0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        changeTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > changeTime)
        {
            spriteRenderer.sprite =  spriteRenderer.sprite == baseSprite ? changeSprite : baseSprite;
            currentTime = 0.0f;
            changeTime = Random.Range(minTime, maxTime);
        }
    }
}
