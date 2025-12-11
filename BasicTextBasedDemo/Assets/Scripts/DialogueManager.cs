using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Data")]
    public DialogueDatabase DataBase;
    public FlagManager FlagManager;
    public string StartNodeID;
    public delegate void DialogueUpdated(string speakerName, string roomName, string dialogueText, List<DialogueChoice> choices);
    public event DialogueUpdated OnDialogueUpdated;

    private MyDialogueNode _currentNode;

    private void Start()
    {
        GoToNode(StartNodeID);
    }

    void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private bool IsChoiceAvailable(DialogueChoice choice)
    {
        foreach (var required in choice.RequiredFlags) //checks each choice for if they need a flag
        {
            if (!FlagManager.HasFlag(required)) return false;
        }

        foreach (var forbidden in choice.ForbiddenFlags) //checks each choice for if they shouldn't show up if a player has a flag
        {
            if (FlagManager.HasFlag(forbidden)) return false;
        }

        return true;
    }

    private List<DialogueChoice> FilterChoices(List<DialogueChoice> choices)
    {
        var result = new List<DialogueChoice>();

        foreach (var choice in choices)
        {
            if (IsChoiceAvailable(choice))
            {
                result.Add(choice);
            }
        }

        return result;
    }

    public void SelectChoice(int index)
    {
        var filtered = FilterChoices((_currentNode.Choices));
        var choice = filtered[index];

        //apply any flags
        foreach (var flag in choice.GrantFlag)
        {
            FlagManager.AddFlag(flag);
        }

        if (choice.ReloadScene)
        {
            ReloadScene();
            return;
        }

        GoToNode(choice.NextNodeID);
    }

    public void GoToNode(string nodeID)
    {
        _currentNode = DataBase.GetNode(nodeID);

        if (_currentNode == null)
        {
            OnDialogueUpdated?.Invoke("", "", "[DIALOGUE ENDED]", null);
            return;
        }

        var filtered = FilterChoices((_currentNode.Choices));
        OnDialogueUpdated?.Invoke(_currentNode.SpeakerName, _currentNode.LocationName, _currentNode.DialogueText, _currentNode.Choices);
    }
}

