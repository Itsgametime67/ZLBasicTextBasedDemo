using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Data")] 
    public DialogueDatabase Database;

    [Header("UI Sections")]
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _questUI;

    private bool showingQuests = false;

    public Player player;

    public delegate void DialogueUpdated(string speakerName, string locationName, string dialogueText, List<DialogueChoice> choices);
    public event DialogueUpdated OnDialogueUpdated;
    
    private DialogueNode _currentDialogueNode;

    private void Start()
    {               
        GoToNode(player.CurrentNodeId);
    }

    private void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private bool IsChoiceAvailable(DialogueChoice choice)
    {
        foreach (var required in choice.RequiredFlags)
        {
            if (!player.HasFlag(required)) return false;
        }

        foreach (var forbidden in choice.ForbiddenFlags)
        {
            if (player.HasFlag(forbidden)) return false;
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
        var filtered = FilterChoices(_currentDialogueNode.Choices);
        var choice = filtered[index];

        foreach (var flag in choice.GrantFlags)
        {
            player.AddFlag(flag);
        }

        if (choice.GrantQuest != "")
        {
            player.CurrentQuest = choice.GrantQuest;
        }

        if (choice.ReloadScene)
        {
            ReloadScene();
            return;
        }
        
        player.CurrentNodeId = choice.NextNodeId;
        GoToNode(choice.NextNodeId);
    }

    public void GoToNode(string nodeId)
    {
        _currentDialogueNode = Database.GetNode(nodeId);

        if (_currentDialogueNode == null)
        {
            OnDialogueUpdated?.Invoke("", "", "[Dialogue Ended]", null);
            return;
        }
        
        var filtered = FilterChoices(_currentDialogueNode.Choices);
        {
            OnDialogueUpdated?.Invoke(_currentDialogueNode.SpeakerName, _currentDialogueNode.LocationName, _currentDialogueNode.DialogueText, filtered);
        }

        if(showingQuests == true) // if the quest menu is open, close it after moving to whichever node.
        {
            ShowQuestUI();
        }
    }

    public void ShowQuestUI()
    {
        if (showingQuests == false)
        {
            _questUI.SetActive(true);
            _gameplayUI.SetActive(false);
            showingQuests = true;
        }
        else
        {
            _questUI.SetActive(false);
            _gameplayUI.SetActive(true);
            showingQuests = false;
        }
    }
}
