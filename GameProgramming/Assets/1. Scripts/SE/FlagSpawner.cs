using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct FlagInfo
{
    public string colorNameEN;
    public string colorNameKR;
    public Color color;
    public Vector2 position;
}

[System.Serializable]
public struct FakeFlagInfo          // 나중에 다른 애들 있으면 사용하기
{
    public GameObject prefabs;
    public Vector2 position;
}

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] public FlagInfo[] flagInfoList;
    [SerializeField] private GameObject dustParticle;
    
    private int nowFlagCount = 2;
    private int maxFlagCount;

    private void Start()
    {
        maxFlagCount = flagInfoList.Length;
    }

    public void SpawnFlag()
    {
        if (nowFlagCount < maxFlagCount)            // 생성 코드 다 바꾸기
        {
            GameObject flagObj = Instantiate(flagPrefab, new Vector2(flagInfoList[nowFlagCount].position.x, Camera.main.transform.position.y + 7), Quaternion.identity, transform);
            flagObj.name = $"{flagInfoList[nowFlagCount].colorNameEN}Flag";

            GameObject particle =  Instantiate(dustParticle, new Vector2(flagInfoList[nowFlagCount].position.x - 1.45f, flagInfoList[nowFlagCount].position.y - 1.55f), Quaternion.identity, transform);
            var main = particle.GetComponent<ParticleSystem>().main;
            main.startColor = flagInfoList[nowFlagCount].color;

            Flag flag = flagObj.GetComponent<Flag>();
            flag.Init(flagInfoList[nowFlagCount].colorNameEN, flagInfoList[nowFlagCount].color, flagInfoList[nowFlagCount].position.y);
            FlagStateManager.Instance.SetFlag(flag);

            nowFlagCount++;

            if (nowFlagCount == 5)
            {
                FlagStateManager.Instance.FourFlagMovement();
            }
        }
    }
}
