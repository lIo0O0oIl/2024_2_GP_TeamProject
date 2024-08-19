using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FlagColor
{
    public string colorName;
    public Color color;
}

public class FlagSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] private FlagColor[] flagColorList;
    [SerializeField] private GameObject dustParticle;
    
    private int nowFlagCount = 0;
    private int maxFlagCount;

    private void Start()
    {
        maxFlagCount = flagColorList.Length;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (nowFlagCount < maxFlagCount)
            {
                GameObject flagObj = Instantiate(flagPrefab, new Vector2(-7 + (nowFlagCount * 2), Camera.main.transform.position.y + 7), Quaternion.identity);
                Invoke("ParticleStart", 0.4f);
                Flag flag = flagObj.GetComponent<Flag>();
                flag.Init(flagColorList[nowFlagCount].colorName, flagColorList[nowFlagCount].color);
                nowFlagCount++;
            }
        }
    }

    private void ParticleStart()
    {
        Instantiate(dustParticle, new Vector2(-7 + (nowFlagCount * 2) - 3.4f, -1.5f), Quaternion.identity);
    }
}
