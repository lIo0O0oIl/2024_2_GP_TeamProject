using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFlag : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite baseSprite, changeSprite;
    public string flagNameKR;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowEventFlag()
    {
        StopCoroutine(UseEventFlag());
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = changeSprite;
        StartCoroutine(UseEventFlag());
        Instruction.Instance.EventFlagCheck();
    }

    public IEnumerator UseEventFlag()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        spriteRenderer.sprite = baseSprite;
    }
}
