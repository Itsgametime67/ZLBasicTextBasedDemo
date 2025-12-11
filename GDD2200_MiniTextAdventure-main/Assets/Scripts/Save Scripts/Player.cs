using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DialogueManager DM;
    public List<string> Flags = new();
    public string CurrentNodeId;

    
    public PlayerData ToData()
    {
        return new PlayerData
        {
            PlayerFlags = Flags,
            PlayerCurrentNodeId = CurrentNodeId
        };
    }

    public void FromData(PlayerData data)
    {
        Flags = data.PlayerFlags;
        CurrentNodeId = data.PlayerCurrentNodeId;
        DM.GoToNode(CurrentNodeId);
    }
}
