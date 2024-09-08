
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

// �ڳ� SDK namespace �߰�
using BackEnd;
using System;

public class BackendRank
{
    private static BackendRank _instance = null;

    public static BackendRank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }

    public void RankInsert(int score)
    {
        string rankUUID = "60d1f100-6b4d-11ef-808f-7be0f20936a0"; 

        string tableName = "KingFlag";
        string rowInDate = string.Empty;

        // 랭킹을 삽입하기 위해서는 게임 데이터에서 사용하는 데이터의 inDate값이 필요합니다.  
        // 따라서 데이터를 불러온 후, 해당 데이터의 inDate값을 추출하는 작업을 해야합니다.  
        Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("데이터 조회 중 문제가 발생했습니다 : " + bro);
            return;
        }

        Debug.Log("데이터 조회에 성공했습니다 : " + bro);

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }

            Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        Debug.Log("내 게임 정보의 rowInDate : " + rowInDate); // 추출된 rowIndate의 값은 다음과 같습니다.  

        Param param = new Param();
        param.Add("score", score);

        // 추출된 rowIndate를 가진 데이터에 param값으로 수정을 진행하고 랭킹에 데이터를 업데이트합니다.  
        Debug.Log("랭킹 삽입을 시도합니다.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
            return;
        }

        Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
    }

    public void RankGet(TextMeshProUGUI _1st, TextMeshProUGUI _2st, TextMeshProUGUI _3st, TextMeshProUGUI _1stS, TextMeshProUGUI _2stS, TextMeshProUGUI _3stS, TextMeshProUGUI _MyR, TextMeshProUGUI _MyRS)
    {
        string rankUUID = "60d1f100-6b4d-11ef-808f-7be0f20936a0";
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);


        for (int i = 0; i < bro.FlattenRows().Count; i++)
        {
            LitJson.JsonData jsonData = bro.FlattenRows()[i];

            // "nickname" 키가 존재하는지 확인
            if (jsonData.ContainsKey("nickname"))
            {
                StringBuilder info = new StringBuilder();
                StringBuilder info_s = new StringBuilder();

                info.AppendLine($"{jsonData["rank"]}    #{jsonData["nickname"]}");
                info_s.AppendLine($"{(int)jsonData["score"]} 점");

                if (i == 0)
                {
                    _1st.text = info.ToString();
                    _1stS.text = info_s.ToString();
                }
                else if (i == 1)
                {
                    _2st.text = info.ToString();
                    _2stS.text = info_s.ToString();
                }
                else if (i == 2)
                {
                    _3st.text = info.ToString();
                    _3stS.text = info_s.ToString();
                }

                // 사용자 랭킹 업데이트
                if (jsonData["nickname"].ToString() == Data.Instance.LoadData())
                {
                    _MyR.text = info.ToString();
                    _MyRS.text = info_s.ToString();
                }
            }
            else
            {
                Debug.LogError($"랭킹 {i}에 'nickname' 데이터가 없습니다.");
            }
        }

    }
}