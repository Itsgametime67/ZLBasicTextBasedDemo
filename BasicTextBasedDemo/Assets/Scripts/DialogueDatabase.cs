using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Database")]

public class DialogueDatabase : ScriptableObject
{
    public List<MyDialogueNode> Nodes = new();

    private Dictionary<string, MyDialogueNode> _lookUpNode;

    private void BuildNodeDictionary()
    {
        if (_lookUpNode != null) return; //If the dictionary doesn't already exist, creates the dictionary.

        _lookUpNode = new Dictionary<string, MyDialogueNode>(); //creates a new dictionary.

        foreach (var node in Nodes) //For each node, add the id of each node.
        {
            _lookUpNode.Add(node.nodeID, node);
        }
    }

    public MyDialogueNode GetNode(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;//if the string is empty, return null.

        BuildNodeDictionary(); //Runs the build node dictionary method.

        _lookUpNode.TryGetValue(id, out var node); //tries to get the nodeID but wont break if it cant.

        return node; //returns the nodeID.
    }
}
