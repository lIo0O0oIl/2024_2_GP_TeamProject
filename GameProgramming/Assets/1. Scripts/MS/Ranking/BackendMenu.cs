using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackendMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _1st;
    [SerializeField] private TextMeshProUGUI _1stScore;
    [SerializeField] private TextMeshProUGUI _2st;
    [SerializeField] private TextMeshProUGUI _2stSocre;
    [SerializeField] private TextMeshProUGUI _3st;
    [SerializeField] private TextMeshProUGUI _3stScore;
    [SerializeField] private TextMeshProUGUI _myRanking;
    [SerializeField] private TextMeshProUGUI _myRankingSocre;

    private void Start()
    {
        Data.Instance.LoadData();
        _myRanking.text = $"-    #{Data.Instance.LoadData()}";
        _myRankingSocre.text = "0.00 √ ";
        GetRanking();
    }

    public void GetRanking()
    {
        BackendRank.Instance.RankGet(_1st, _2st, _3st, _1stScore, _2stSocre, _3stScore, _myRanking, _myRankingSocre);
    }
}
