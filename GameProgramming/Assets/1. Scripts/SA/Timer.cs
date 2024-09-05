using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float currentTimer, maxTimer;
    [SerializeField]
    private float plusTime;

    [SerializeField]
    private TextMeshProUGUI timerTxt;

    private void Start()
    {
        currentTimer = maxTimer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        timerTxt.text = Mathf.FloorToInt(currentTimer).ToString();
    }

    public void AddTime()
    {
        Debug.Log("성공함! 시간 추가해줘");
        currentTimer += plusTime;
    }

    // 얘는 모든 시간 타이머임.
}
