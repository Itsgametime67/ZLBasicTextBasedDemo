using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public DialogueManager DM;
    public TextMeshProUGUI QuestTextDisplay;
    public Player player;

    public void OnEnable()
    {
        if (player.CurrentQuest == "" || player.CurrentQuest == "Complete" || player.CurrentQuest == null)
        {
            QuestTextDisplay.text = "No Current Quest."; //if the player doesn't have a quest, or their quest was finished, say they don't have one.
        }
        else
        {
            QuestTextDisplay.text = player.CurrentQuest; //display the player's current quest.
        }
    }
}
