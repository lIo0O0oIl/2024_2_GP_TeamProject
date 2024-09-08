using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public string userName = "";

    public float bestScore = 0;

    private static Data _instance = null;

    public static Data Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Data();
            }

            return _instance;
        }
    }

    void Start()
    {
        userName = inputField.GetComponent<TMP_InputField>().text;
    }

    public void NickName()
    {
        userName = inputField.text;

        PlayerPrefs.SetString("userName", userName);

        BackendLogin.Instance.UpdateNickname(userName);

        Debug.Log(userName);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public float BestSocre(float score)
    {
        Debug.Log("베스트 스코어 들어옴");
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        float curretScore = score;

        if (curretScore > bestScore)
        {
            bestScore = curretScore;

            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        Debug.Log("best " + bestScore);
        return bestScore;
    }

    public void ResetBestScore()
    {
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.SetFloat("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    public string LoadData()
    {
        userName = PlayerPrefs.GetString("userName");
        return userName;
    }
}