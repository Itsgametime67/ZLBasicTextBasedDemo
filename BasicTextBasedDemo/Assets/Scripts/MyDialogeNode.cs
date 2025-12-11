using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Dialoge Node")]
public class MyDialogeNode : ScriptableObject
{
    [Header("Identity")]
    public string nodeID; //a unique identification for the node

    [Header("Location")]
    public string LocationName; //A name for the room

    [Header("Dialoge")]
    public string SpeakerName; //A name for the NPC
    [TextArea(2, 5)]
    public string DialogueText; //The dialoge to be displayed

    [Header("Dialoge Choices")]
    public List<DialogeChoice> Choices = new();
}
