using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public string colorName;
    public Color color;
    public float speed = 3f;

    [SerializeField] private Vector2 orginPos;
    [SerializeField] private Vector2 movePos;

    public bool is_up = true;
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

    public void Init(string colorName, Color color, float yPos)
    {
        this.colorName = colorName;
        this.color = color;

        OnValidate();
        if (cloth == null)
        {
            cloth = transform.GetChild(1).gameObject;
        }
        orginPos = cloth.transform.localPosition;

        transform.DOMove(new Vector2(transform.position.x, yPos), 1).SetEase(Ease.OutBounce);
    }

    private void Update()
    {
        if (is_up == false)
        {
            cloth.transform.Translate(movePos * Time.deltaTime);
        }
        else
        {
            cloth.transform.Translate(-movePos * Time.deltaTime);
        }

        cloth.transform.localPosition = new Vector2
            (Mathf.Clamp(cloth.transform.localPosition.x, movePos.x, orginPos.x),
            Mathf.Clamp(cloth.transform.localPosition.y, movePos.y, orginPos.y));
    }

    private void OnMouseDown()
    {
        is_up = !is_up;
        FlagStateManager.Instance.GiveFlagStateToCommand();
    }
}
