using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum FLAG
{
    û��,
    ���,
    ����,
    �ֱ�,
    Ȳ��,
    ���,
    ���ñ�,
    ȫ��,
    ����,
    ���,
    �ⷯ��,
    ����,
    �����,
    ���,
    �����,
    ����,
    �ڱ�,
    ��ѱ�,
    COUNT
}

public class Instruction : MonoBehaviour
{
    static public Instruction Instance;

    [Header("Instruction List")]
    [SerializeField]
    List<string> up = new List<string>();
    [SerializeField]
    List<string> down = new List<string>();

    [Header("CommandAndFlag")]
    [SerializeField] string upCommand;
    [SerializeField] string downCommand;
    [SerializeField] FLAG upFlag;
    [SerializeField] FLAG downFlag;

    [Header("Timer")]
    public float questionTime;
    public float currentTime;
    [SerializeField] int timeChangeCnt;
    [SerializeField] int timeCurrentCnt;

    [Header("FlagAndWave")]
    [SerializeField] int currentFlagNum;
    [SerializeField] int currentWave;
    [SerializeField] bool isClear;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI instructionTxt;
    [SerializeField] TextMeshProUGUI timeTxt;

    Timer timer;
    FlagSpawner flagSpawner;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        timer = GetComponent<Timer>();
        flagSpawner = GameObject.Find("FlagSpawner").GetComponent<FlagSpawner>();

        currentFlagNum = 1;
        currentWave = 0;
    }

    private void Start()
    {
        flagSpawner.SpawnFlag();
        flagSpawner.SpawnFlag();
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

            timeCurrentCnt++;
            if (timeCurrentCnt > timeChangeCnt && questionTime >= 1f)
                questionTime -= 0.1f;

            timer.SetTimer(isClear);
            isClear = false;
        }
    }

    public void EnterInstruction()
    {
        // ��� ���ϰ�
        do upFlag = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while ((int)upFlag > currentFlagNum);
        do downFlag = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while (upFlag == downFlag || (int)downFlag > currentFlagNum);


        //// ��ɾ� ���ϰ�
        //int upIdx, downIdx;
        //do upIdx = Random.Range(0, up.Count);
        //while (upIdx == upFlag.bool)
        //// ���� ���¶� �ߺ� ����
        //do downIdx = Random.Range(0, down.Count);
        //while ((upIdx == 2 && upIdx == downIdx) || downIdx == downFlag.bool);
        //// ������ �ߺ� ���� ���� ���¶� �ߺ� ����

        //upCommand = up[upIdx];
        //downCommand = down[downIdx];

        // ���� ����
        //upCommand = up[Random.Range(0, up.Count)];
        //do downCommand = down[Random.Range(0, down.Count)];
        //while (upCommand == "������ ��" && upCommand == downCommand); // ������ �� ��ġ��
        //// if (upFlag.���� == �÷� �Ǵ� ������ ��� �ش� ���¸� ������ n + ������)

        instructionTxt.text = upFlag + " " + upCommand + " " + downFlag + " " + downCommand;

        if (currentWave % 2 == 0 && currentWave != 0)
        {
            flagSpawner.SpawnFlag();
            currentFlagNum++;
        }
        currentWave++;
    }

    public void RemoveInstruction()
    {
        instructionTxt.text = "";
        EnterInstruction();
    }
}
