using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public enum FLAG
{
    Blue,
    White,
    Red,
    Orange,
    Yellow,
    Green,
    Purple,
    Pink,
    Brown,
    Black,
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
        currentTime = questionTime;
        EnterInstruction();
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.Space))
            isClear = true;*/

        currentTime -= Time.deltaTime;
        timeTxt.text = (Mathf.Floor(currentTime * 10f) / 10f).ToString();

        if (currentTime < 0)
        {
            RemoveInstruction();
            currentTime = questionTime;

            timeCurrentCnt++;
            if (timeCurrentCnt > timeChangeCnt && questionTime >= 1f)
                questionTime -= 0.1f;

            timer.SetTimer(isClear);
            isClear = false;
        }
    }

    private void EnterInstruction()
    {
        // 첫번째 깃발
        do upFlag = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while ((int)upFlag > currentFlagNum);

        // 두번째 깃발
        do downFlag = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while (upFlag == downFlag || (int)downFlag > currentFlagNum);

        int upIdx, downIdx;

        // 첫번째 명령
        do upIdx = Random.Range(0, up.Count);
        while (upIdx.ToString() == FlagStateManager.Instance.GetFlag(upFlag.ToString()).is_up.ToString());

        // 두번째 명령
        do downIdx = Random.Range(0, down.Count);
        while ((upIdx == 2 && upIdx == downIdx) ||
            downIdx.ToString() == (FlagStateManager.Instance.GetFlag(downFlag.ToString()).is_up).ToString());

        upCommand = up[upIdx];
        downCommand = down[downIdx];

        instructionTxt.text = upFlag + " " + upCommand + " " + downFlag + " " + downCommand;

        if (currentWave % 2 == 0 && currentWave != 0)       // 두 턴마다 깃발 생성하기
        {
            currentFlagNum++;
            flagSpawner.SpawnFlag();
        }
        currentWave++;
    }

    private void RemoveInstruction()
    {
        instructionTxt.text = "";
        EnterInstruction();
    }

    public void CheckCommand(List<bool> flagState)
    {
        // 깃발들 확인하기!
    }
}
