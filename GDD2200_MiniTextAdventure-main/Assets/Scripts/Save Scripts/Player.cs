using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DialogueManager DM;
    public List<string> FlagList = new();

    public string CurrentNodeId;
    public string CurrentQuest;

    private HashSet<string> _flagHash = new();


    public PlayerData ToData()
    {
        return new PlayerData
        {
            PlayerFlags = FlagList,
            PlayerCurrentNodeId = CurrentNodeId,
            PlayerCurrentQuest = CurrentQuest
        };
    }

    public void FromData(PlayerData data)
    {
        FlagList = data.PlayerFlags; //Get all saved data
        ListToHash(FlagList); //Run through that list and add them to hashset
        CurrentQuest = data.PlayerCurrentQuest; //Set the current quest to the saved current quest
        CurrentNodeId = data.PlayerCurrentNodeId; //Set the current node to the saved node
        DM.GoToNode(CurrentNodeId); //Go to the saved node.
    }

    public bool HasFlag(string flag)
    {
        return _flagHash.Contains(flag);
    }

    public void AddFlag(string flag)
    {
        if (string.IsNullOrEmpty(flag)) return;


        _flagHash.Add(flag);
        FlagList.Add(flag);
    }

    public void ListToHash(List<string> list)
    {
        for (int i = 0; i < list.Count; i++) // for the length of the list
        {
            if (!HasFlag(list[i])) //Check the hashset for a flag with the same name
            {
                AddFlag(list[i]); //If this isn't a duplicate flag, add it to the Hashset.
            }
        }
    }

    
}
