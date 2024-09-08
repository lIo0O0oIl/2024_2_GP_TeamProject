using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum FLAG
{
    Blue      = 0,
    White    = 1,
    Red       = 2,
    Yellow   = 3,
    Black     = 4,
    Green   = 5,
    Purple   = 6,
    Pink      = 7,
    Orange = 8,
    Brown   = 9,
    COUNT
}                           // 나중에 딕셔너리 쓰거나 배열사용해서 색깔들 표시하게 해주기.

public class Instruction : MonoBehaviour
{
    static public Instruction Instance;

    [Header("Instruction List")]
    [SerializeField]
    string[] firstCommandList = new string[3];
    [SerializeField]
    string[] secondCommandList = new string[3];

    // CommandAndFlag
    [SerializeField] private List<int> movedFlagIndex = new List<int>();
    [SerializeField] private List<bool> movedFlagState = new List<bool>();

    [Header("CommandTimer")]
    public float questionTime;
    public float currentTime;
    [SerializeField] int timeChangeCnt;
    [SerializeField] int timeCurrentCnt;

    [Header("FlagAndWave")]
    [SerializeField] [Range(0, 10)] private float twoCommandRange = 7;
    private bool twoCommand = false;
    private bool oneFlagCheck = false;
    [SerializeField] int currentFlagNum;
    [SerializeField] int currentWave;
    [SerializeField] int keepPercent;
    //[SerializeField] bool isClear;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI instructionTxt;
    [SerializeField] TextMeshProUGUI timeTxt;

    [Header("Score")]
    [SerializeField] private TMP_Text scoreTxt;
    private int score = 0;


    private Timer timer;
    private FlagSpawner flagSpawner;

    private int whileBreaker = 0;

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
        currentTime -= Time.deltaTime;
        timeTxt.text = (Mathf.Floor(currentTime * 10f) / 10f).ToString();

        if (currentTime < 0)
        {
            RemoveInstruction();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }

    private void EnterInstruction()
    {
        if (Random.Range(0, 10) < twoCommandRange)
        {
            twoCommand = true;
        }

        #region 깃발 선택
        FLAG firstFlagIndex, secondFlagIndex = FLAG.Blue;
        // 첫번째 깃발
        do firstFlagIndex = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while ((int)firstFlagIndex > currentFlagNum);
        movedFlagIndex.Add((int)firstFlagIndex);

        if (twoCommand)
        {
            // 두번째 깃발 (첫번째 것과 동일하지 않아야 함.)
            do secondFlagIndex = (FLAG)Random.Range(0, (int)FLAG.COUNT);
            while (firstFlagIndex == secondFlagIndex || (int)secondFlagIndex > currentFlagNum);
            movedFlagIndex.Add((int)secondFlagIndex);
        }

        #endregion

        #region 명령 선택
        int firstCommandIndex = 0, secondCommandIndex = 0;
        List<bool> flagState = FlagStateManager.Instance.GetFlagState();
        
        // 첫번째 명령 (0, 1 일 때 현재와 달라야 함.)
        while (true)
        {
            firstCommandIndex = Random.Range(0, firstCommandList.Length);
            if (firstCommandIndex != 2)
            {
                if (System.Convert.ToBoolean(firstCommandIndex) != FlagStateManager.Instance.GetFlag((int)firstFlagIndex).is_up) break;
            }
            else if (firstCommandIndex == 2)
            {
                if (twoCommand == false) continue;
                if (keepPercent > Random.Range(0, 100)) continue;
            }
            //else break;

            whileBreaker++;
            if (whileBreaker > 1000)
            {
                whileBreaker = 0;
                Debug.Log("와일문 잘못만듦");
                break;
            }
        }

        if (twoCommand)
        {
            // 두번째 명령 (현재꺼랑 다르고 위에꺼랑 가만히 두고가 겹치면 안됨.)
            while (true)
            {
                secondCommandIndex = Random.Range(0, secondCommandList.Length);
                if (secondCommandIndex != 2)
                {
                    if (System.Convert.ToBoolean(secondCommandIndex) != FlagStateManager.Instance.GetFlag((int)secondFlagIndex).is_up) break;
                }
                else if (secondCommandIndex == 2)
                {
                    if (firstCommandIndex != 2) break;

                    if (secondCommandIndex == 2)
                    {
                        if (keepPercent > Random.Range(0, 100))
                            continue;
                    }
                }

                whileBreaker++;
                if (whileBreaker > 1000)
                {
                    whileBreaker = 0;
                    Debug.Log("와일문 잘못만듦");
                    break;
                }
            }
        }

        // 명령어 들어오는게 이상함. 고치기!
        if (firstCommandIndex == 2 || secondCommandIndex == 2)
        {
            if (firstCommandIndex == 2)
            {
                movedFlagState.Add(FlagStateManager.Instance.GetFlag((int)firstFlagIndex).is_up);
                movedFlagState.Add(System.Convert.ToBoolean(secondCommandIndex));
            }
            else if (secondCommandIndex == 2)
            {
                movedFlagState.Add(System.Convert.ToBoolean(firstCommandIndex));
                movedFlagState.Add(FlagStateManager.Instance.GetFlag((int)secondFlagIndex).is_up);
            }
        }
        else
        {
            movedFlagState.Add(System.Convert.ToBoolean(firstCommandIndex));
            movedFlagState.Add(System.Convert.ToBoolean(secondCommandIndex));
        }
        #endregion

        if (twoCommand)
        {
            instructionTxt.text = $"<color={ColorToHex(flagSpawner.flagInfoList[(int)firstFlagIndex].color)}>{firstFlagIndex}</color> {firstCommandList[firstCommandIndex]} " +
                $"<color={ColorToHex(flagSpawner.flagInfoList[(int)secondFlagIndex].color)}>{secondFlagIndex}</color> {secondCommandList[secondCommandIndex]}";
        }
        else
        {
            instructionTxt.text = $"<color={ColorToHex(flagSpawner.flagInfoList[(int)firstFlagIndex].color)}>{firstFlagIndex}</color> {firstCommandList[firstCommandIndex]}";
        }

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
        oneFlagCheck = false;

        movedFlagIndex.Clear();
        movedFlagState.Clear();

        currentTime = questionTime;

        timeCurrentCnt++;
        if (timeCurrentCnt > timeChangeCnt && questionTime >= 1.5f)
            questionTime -= 0.1f;

        EnterInstruction();
    }

    public void CheckCommand(List<bool> flagState)
    {
        // Moved Flag State 는 따로 계산하기
        int stateCnt = 0;
        foreach (var index in movedFlagIndex)
        {
            if (flagState[index] != movedFlagState[stateCnt])
            {
                if (twoCommand && oneFlagCheck == false)
                {
                    oneFlagCheck = true;
                    continue;
                }
                timer.SubtractTime();
                RemoveInstruction();
                return;
            }
            stateCnt++;
        }
        score += 1;
        scoreTxt.text = score.ToString();
        timer.AddTime();
        RemoveInstruction();
    }

    string ColorToHex(Color color)
    {
        // RGB 값을 0~255 범위로 변환
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        // 헥사 코드로 변환 (RRGGBB 형식)
        return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
    }
}
