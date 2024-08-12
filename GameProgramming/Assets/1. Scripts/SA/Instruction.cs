using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Flag
{
    ����,
    ���,
    Ȳ��,
    û��,
    ���,
    ���,
    �ڱ�,
    �α�,
    �ֱ�,
    ����,
    �Ʊ�,
    �����,
    �ⷯ��,
    ���,
    COUNT
}

public class Instruction : MonoBehaviour
{
    [SerializeField]
    List<string> up = new List<string>();
    [SerializeField]
    List<string> down = new List<string>();

    [SerializeField] string upCommand, downCommand;

    [SerializeField] Flag upFlag, downFlag;

    [SerializeField] public float questionTime, currentTime;

    [SerializeField] int changeCnt, currentCnt;

    [SerializeField] bool isClear;

    [SerializeField]
    TextMeshProUGUI instructionTxt, timeTxt;

    Timer timer;

    private void Awake()
    {
        timer = GetComponent<Timer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isClear = true;

        currentTime -= Time.deltaTime;
        timeTxt.text = (Mathf.Floor(currentTime * 10f) / 10f).ToString();


        if (currentTime < 0)
        {
            RemoveInstruction();
            currentTime = questionTime;

            //questionTime -= 0.1f;

            currentCnt++;
            if (currentCnt > changeCnt && questionTime >= 1f)
                questionTime -= 0.1f;

            timer.SetTimer(isClear);
            isClear = false;
        }
    }

    public void EnterInstruction()
    {
        // ��ɾ� ���ϰ�
        upCommand = up[Random.Range(0, up.Count)];
        downCommand = down[Random.Range(0, down.Count)];

        // ��� ���ϰ�
        upFlag = (Flag)Random.Range(0, (int)Flag.COUNT);
        do downFlag = (Flag)Random.Range(0, (int)Flag.COUNT);
        while (upFlag == downFlag);

        instructionTxt.text = upFlag + " " + upCommand + " " + downFlag + " " + downCommand;
    }

    public void RemoveInstruction()
    {
        instructionTxt.text = "";
        EnterInstruction();
    }
}
