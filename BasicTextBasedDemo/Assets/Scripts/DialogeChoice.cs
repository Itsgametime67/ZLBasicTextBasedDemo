using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogeChoice : MonoBehaviour
{
    [Header("UI")]
    public string ChoiceText;// The choices available. 

    [Header("Flow")]
    public string NextNodeID; //Which node should follow this one.
    public bool ReloadScene;

    [Header("Conditions")]
    public List<string> RequiredFlags = new(); //The flags that need to be present for this node to appear.
    public List<string> ForbiddenFlags = new(); //If the player has these flags, then this node wont appear.

    [Header("Flags On Select")]
    public List<string> GrantFlag = new(); //adds the inputed flags on press
}
