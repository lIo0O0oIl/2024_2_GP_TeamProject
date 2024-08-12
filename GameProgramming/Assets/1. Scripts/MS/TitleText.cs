using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private float timer = 0.1f;

    private Color[] rainbowColors;
    private int currentColorIndex = 0;
    private float timeElapsed = 0f;

    private void Start()
    {
        rainbowColors = new Color[]
        {
            Color.red,
            new Color(1f, 0.5f, 0f), 
            Color.yellow,
            Color.green,
            Color.blue,
            new Color(0.58f, 0f, 0.83f) 
        };
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timer)
        {
            currentColorIndex = (currentColorIndex + 1) % rainbowColors.Length;
            titleText.color = rainbowColors[currentColorIndex];

            timeElapsed = 0f;
        }
    }
}
