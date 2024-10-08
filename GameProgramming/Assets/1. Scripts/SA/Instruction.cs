using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum FLAG
{
    Blue = 0,
    White = 1,
    Red = 2,
    Yellow = 3,
    Black = 4,
    Green = 5,
    Purple = 6,
    Pink = 7,
    Orange = 8,
    Brown = 9,
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
    [SerializeField][Range(0, 10)] private float twoCommandRange = 7;
    [SerializeField][Range(0, 10)] private float eventFlagRange = 3;
    [SerializeField][Range(0, 10)] private float keepPercent = 3;
    private bool twoCommand = false;
    private bool oneFlagCheck = false;
    private bool beforeEventFlag = false;
    private bool is_stay = false;
    private bool is_nowFlagStateCommand = false;
    [SerializeField] int currentFlagNum;
    [SerializeField] int currentWave;
    //[SerializeField] bool isClear;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI instructionTxt;
    [SerializeField] TextMeshProUGUI timeTxt;

    [Header("Score")]
    [SerializeField] private TMP_Text scoreTxt;
    public int score = 0;


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

        flagSpawner.SpawnFlag();
        flagSpawner.SpawnFlag();

        EnterInstruction();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        timeTxt.text = (Mathf.Floor(currentTime * 10f) / 10f).ToString();

        if (currentTime < 0)
        {
            timer.SubtractTime();
            RemoveInstruction();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
#endif
    }

    private void EnterInstruction()
    {
        if (Random.Range(0, 10) < eventFlagRange)
        {
            if (beforeEventFlag == false)
            {
                ShowEventFlagCommand();
                beforeEventFlag = true;
                return;
            }
            beforeEventFlag = false;
        }
        else
        {
            beforeEventFlag = false;
        }

        if (Random.Range(0, 10) < twoCommandRange) twoCommand = true;
        else twoCommand = false;

        #region 깃발 선택
        FLAG firstFlagIndex = FLAG.Blue, secondFlagIndex = FLAG.Blue;
        if (twoCommand)
        {
            // 첫번째 깃발
            do firstFlagIndex = (FLAG)Random.Range(0, (int)FLAG.COUNT);
            while ((int)firstFlagIndex > currentFlagNum);
            movedFlagIndex.Add((int)firstFlagIndex);
        }

        // 두번째 깃발 (첫번째 것과 동일하지 않아야 함.)
        do secondFlagIndex = (FLAG)Random.Range(0, (int)FLAG.COUNT);
        while (firstFlagIndex == secondFlagIndex || (int)secondFlagIndex > currentFlagNum);
        movedFlagIndex.Add((int)secondFlagIndex);

        #endregion

        #region 명령 선택
        int firstCommandIndex = 0, secondCommandIndex = 0;
        List<bool> flagState = FlagStateManager.Instance.GetFlagState();

        // 첫번째 명령 (0, 1 일 때 현재와 달라야 함.)
        if (twoCommand)
        {
            while (true)
            {
                firstCommandIndex = Random.Range(0, firstCommandList.Length);
                if (firstCommandIndex != 2)
                {
                    if (System.Convert.ToBoolean(firstCommandIndex) == FlagStateManager.Instance.GetFlag((int)firstFlagIndex).is_up)
                    {
                        is_nowFlagStateCommand = true;
                    }
                     break;
                }
                else if (firstCommandIndex == 2)
                {
                    if (keepPercent > Random.Range(0, 10)) continue;
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
        }

        // 두번째 명령 (현재꺼랑 다르고 위에꺼랑 가만히 두고가 겹치면 안됨.)
        while (true)
        {
            secondCommandIndex = Random.Range(0, secondCommandList.Length);
            if (secondCommandIndex != 2)
            {
                if (is_nowFlagStateCommand || twoCommand == false)
                {
                    if (System.Convert.ToBoolean(secondCommandIndex) != FlagStateManager.Instance.GetFlag((int)secondFlagIndex).is_up) break;
                }
                else break;
            }
            else if (secondCommandIndex == 2)
            {
                if (twoCommand == false || is_nowFlagStateCommand) continue;
                
                if (firstCommandIndex != 2) break;

                if (secondCommandIndex == 2)
                {
                    if (keepPercent > Random.Range(0, 10))
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

        if (firstCommandIndex == 2 || secondCommandIndex == 2)
        {
            is_stay = true;
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
            instructionTxt.text = $"<color={ColorToHex(flagSpawner.flagInfoList[(int)firstFlagIndex].color)}>{FlagSpawner.Instance.flagInfoList[(int)firstFlagIndex].colorNameKR}</color> {firstCommandList[firstCommandIndex]} " +
                $"<color={ColorToHex(flagSpawner.flagInfoList[(int)secondFlagIndex].color)}>{FlagSpawner.Instance.flagInfoList[(int)secondFlagIndex].colorNameKR}</color> {secondCommandList[secondCommandIndex]}";
        }
        else
        {
            instructionTxt.text = $"<color={ColorToHex(flagSpawner.flagInfoList[(int)secondFlagIndex].color)}>{FlagSpawner.Instance.flagInfoList[(int)secondFlagIndex].colorNameKR}</color> {secondCommandList[secondCommandIndex]}";
        }

        if (currentWave % 2 == 0 && currentWave != 0)       // 두 턴마다 깃발 생성하기
        {
            currentFlagNum++;
            flagSpawner.SpawnFlag();
        }
        currentWave++;
    }

    private void ShowEventFlagCommand()
    {
        int index = Random.Range(0, FlagStateManager.Instance.eventFlag.Count);

        FlagStateManager.Instance.eventFlag[index].ShowEventFlag();
        instructionTxt.text = $"{FlagStateManager.Instance.eventFlag[index].flagNameKR} 누르기!";

        if (currentWave % 2 == 0 && currentWave != 0)       // 두 턴마다 깃발 생성하기
        {
            currentFlagNum++;
            flagSpawner.SpawnFlag();
        }
        currentWave++;
    }

    private void RemoveInstruction()
    {
        if (beforeEventFlag)
        {
            FlagStateManager.Instance.EventFlagDontShow();
        }

        instructionTxt.text = "";
        oneFlagCheck = false;
        is_stay = false;
        is_nowFlagStateCommand = false;

        movedFlagIndex.Clear();
        movedFlagState.Clear();

        currentTime = questionTime;

        whileBreaker = 0;
        timeCurrentCnt++;
        if (timeCurrentCnt > timeChangeCnt && questionTime >= 1.5f)
            questionTime -= 0.05f;

        EnterInstruction();
    }

    public void CheckCommand(List<bool> flagState)
    {
        if (beforeEventFlag)
        {
            timer.SubtractTime();
            FlagStateManager.Instance.EventFlagDontShow();
            RemoveInstruction();
            return;
        }

        // Moved Flag State 는 따로 계산하기
        int stateCnt = 0;
        if (twoCommand == false)
        {
            Debug.Log("엥?");
            stateCnt++;
        }
        foreach (var index in movedFlagIndex)
        {
            if (flagState[index] != movedFlagState[stateCnt])
            {
                if (is_stay)
                {
                    timer.SubtractTime();
                    RemoveInstruction();
                Debug.Log("가만히 있기가 있었음");
                    return;
                }
                if (twoCommand && oneFlagCheck == false)
                {
                    oneFlagCheck = true;
                Debug.Log("2개짜리 명령이라 하나는 무시");
                    return;
                }
                Debug.Log("그냥 틀림");
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

    public void EventFlagCheck()
    {
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
