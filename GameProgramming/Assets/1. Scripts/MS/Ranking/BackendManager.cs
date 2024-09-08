using UnityEngine;
using System.Collections;

// �ڳ� SDK namespace �߰�
using BackEnd;

public class BackendManager : MonoBehaviour
{
    int num;

    void Start()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("Backend initialized: " + bro);
        }
        else
        {
            Debug.LogError("Backend initialization failed: " + bro);
        }

        StartCoroutine(SignUpAndLoginCoroutine());


        Data.Instance.ResetBestScore();
    }


    private IEnumerator SignUpAndLoginCoroutine()
    {
        num = Random.Range(0, 9999);

        #region �α��� ȸ������
        BackendLogin.Instance.CustomSignUp("user" + num.ToString(), "1234");
        BackendLogin.Instance.CustomLogin("user" + num.ToString(), "1234");
        #endregion

        #region ������
        BackendGameData.Instance.GameDataGet();

        if (BackendGameData.userData == null)
        {
            BackendGameData.Instance.GameDataInsert();
        }

        BackendGameData.Instance.GameDataUpdate();
        #endregion

        yield return new WaitForSeconds(2f);

        Debug.Log("��");
    }
}