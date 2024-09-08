using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFlag : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite, changeSprite;
    public string flagNameKR;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowEventFlag()
    {
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = changeSprite;
        StartCoroutine(UseEventFlag());
        Instruction.Instance.EventFlagCheck();
    }

    private IEnumerator UseEventFlag()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        spriteRenderer.sprite = baseSprite;
    }
}
