using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float currentTimer, maxTimer;
    [SerializeField]
    private float plusTime, minusTime = 3;

    [SerializeField]
    private TextMeshProUGUI timerTxt;

    [SerializeField]
    private GameObject gameover;

    private void Start()
    {
        currentTimer = maxTimer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        timerTxt.text = Mathf.FloorToInt(currentTimer).ToString();

        Gameover();
    }

    public void Gameover()
    {
        if(currentTimer < 0)
        {
            BackendRank.Instance.RankInsert(Instruction.Instance.score);
            gameover.SetActive(true);

            Debug.Log("Gameover");
        }
    }

    public void AddTime()
    {
        Debug.Log("성공함! 시간 추가해줘");
        SoundManager.Instance.PlaySFX("Clear");
        currentTimer += plusTime;
    }

    public void SubtractTime()
    {
        Debug.Log("실패함! 시간 빼줘");
        SoundManager.Instance.PlaySFX("Fail");
        currentTimer -= minusTime;
    }

    // 얘는 모든 시간 타이머임.
}
