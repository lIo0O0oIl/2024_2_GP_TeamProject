using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FlagInfo
{
    public string colorName;
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
    [SerializeField] private FlagInfo[] flagList;
    [SerializeField] private GameObject dustParticle;
    
    private int nowFlagCount = 0;
    private int maxFlagCount;

    private void Start()
    {
        maxFlagCount = flagList.Length;
    }

    public void SpawnFlag()
    {
        if (nowFlagCount < maxFlagCount)            // 생성 코드 다 바꾸기
        {
            GameObject flagObj = Instantiate(flagPrefab, new Vector2(flagList[nowFlagCount].position.x, Camera.main.transform.position.y + 7), Quaternion.identity, transform);
            flagObj.name = $"{flagList[nowFlagCount].colorName}Flag";
            Invoke("ParticleStart", 0.4f);

            Flag flag = flagObj.GetComponent<Flag>();
            flag.Init(flagList[nowFlagCount].colorName, flagList[nowFlagCount].color, flagList[nowFlagCount].position.y);
            FlagStateManager.Instance.SetFlag(flag);

            nowFlagCount++;
        }
    }

    private void ParticleStart()
    {
        Instantiate(dustParticle, new Vector2(flagList[nowFlagCount -1].position.x - 1.45f, flagList[nowFlagCount].position.y - 1.55f), Quaternion.identity, transform);
        var main = dustParticle.GetComponent<ParticleSystem>().main;
        main.startColor = flagList[nowFlagCount - 1].color;
    }
}
