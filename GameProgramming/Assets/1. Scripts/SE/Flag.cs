using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public string colorName;
    public Color color;

    [SerializeField] private Vector2 orginPos;
    [SerializeField] private Vector2 downPos;

    private bool is_up = false;
    private GameObject cloth;
    private SpriteRenderer clothSpriteRenderer;

    private void OnValidate()
    {
        if (cloth == null)
        {
            cloth = transform.GetChild(1).gameObject;
            if (clothSpriteRenderer == null)
            {
                clothSpriteRenderer = cloth.GetComponent<SpriteRenderer>();
                clothSpriteRenderer.color = color;
            }
            else
            {
                clothSpriteRenderer.color = color;
            }
        }
        else
        {
            if (clothSpriteRenderer == null)
            {
                clothSpriteRenderer = cloth.GetComponent<SpriteRenderer>();
                clothSpriteRenderer.color = color;
            }
            else
            {
                clothSpriteRenderer.color = color;
            }
        }
    }

    private void Start()
    {
        if (color == null)
        {
            cloth = transform.GetChild(1).gameObject;
        }
        orginPos = transform.position;
    }

    private void OnMouseDown()
    {
        Debug.Log($"{colorName}색 깃발 클릭");
        is_up = !is_up;
        if (is_up )
        {
            DOTween.Kill(cloth);
            cloth.transform.DOMove(downPos, 1f).SetEase(Ease.Linear);
        }
        else
        {
            DOTween.Kill(cloth);
            cloth.transform.DOMove(orginPos, 1f).SetEase(Ease.Linear);
        }
    }
}
