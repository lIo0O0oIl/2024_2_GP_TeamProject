using System.Collections;
using System.Collections.Generic;
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
