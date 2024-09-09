using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagStateManager : MonoBehaviour
{
    public static FlagStateManager Instance;

    public List<Flag> flags = new List<Flag>();

    public List<EventFlag> eventFlag = new List<EventFlag>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetFlag(Flag flag)
    {
        flags.Add(flag);
    }

    public Flag GetFlag(string name)
    {
        foreach (Flag flag in flags)
        {
            if (flag.colorName == name)
                return flag;
        }

        return null;
    }

    public Flag GetFlag(int index)
    {
        return flags[index];
    }

    public void GiveFlagStateToCommand()
    {
        List<bool> flagState = new List<bool>();
        foreach (Flag flag in flags)
        {
            flagState.Add(flag.is_up);
        }
        Instruction.Instance.CheckCommand(flagState);
    }

    public List<bool> GetFlagState()
    {
        List<bool> flagState = new List<bool>();
        foreach (Flag flag in flags)
        {
            flagState.Add(flag.is_up);
        }
        return flagState;
    }

    public void FourFlagMovement()
    {
        flags[0].transform.DOLocalMoveY(-2, 0.75f);
        flags[1].transform.DOLocalMoveY(-2, 0.75f);
        flags[2].transform.DOLocalMoveY(-2, 0.75f);
        flags[3].transform.DOLocalMoveY(-2, 0.75f);
    }

    public void EventFlagDontShow()
    {
        foreach (EventFlag flag in eventFlag)
        {
            flag.StopCoroutine(flag.UseEventFlag());
            flag.spriteRenderer.sprite = flag.baseSprite;
            flag.gameObject.SetActive(false);
        }
    }
}
