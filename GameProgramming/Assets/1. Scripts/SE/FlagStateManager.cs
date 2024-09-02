using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagStateManager : MonoBehaviour
{
    public static FlagStateManager Instance;

    public List<Flag> flags = new List<Flag>();
    private List<bool> flagState = new List<bool>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetFlag(Flag flag)
    {
        Debug.Log(flag.colorName + " �߰�");
        flags.Add(flag);
    }

    public Flag GetFlag(string name)
    {
        foreach (Flag flag in flags)
        {
            if (flag.name == name)
                return flag;
        }

        return null;
    }

    public void GiveFlagStateToCommand()
    {
        flagState.Clear();
        foreach (Flag flag in flags)
        {
            flagState.Add(flag.is_up);
        }
        //Instruction.Instance.CheckCommand(flagState);
    }
}
