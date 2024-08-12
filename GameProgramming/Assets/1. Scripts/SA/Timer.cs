using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float currentTimer, maxTimer;
    [SerializeField]
    float plusTime, minusTime;

    [SerializeField]
    TextMeshProUGUI timerTxt;

    Instruction instruction;

    private void Awake()
    {
        instruction = GetComponent<Instruction>();
    }

    private void Start()
    {
        currentTimer = maxTimer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        timerTxt.text = Mathf.FloorToInt(currentTimer).ToString();

        if (Input.GetKeyDown(KeyCode.U))
            SetTimer(true);
        if (Input.GetKeyDown(KeyCode.D))
            SetTimer(false);
    }

    public void SetTimer(bool plus)
    {
        if (plus)
            currentTimer += instruction.questionTime + 1;
            //currentTimer += plusTime;
        else
            currentTimer -= instruction.questionTime - 1;
            //currentTimer -= minusTime;
    }
}
