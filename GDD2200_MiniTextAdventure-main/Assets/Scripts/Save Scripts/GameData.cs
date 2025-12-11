using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public string LastSaved;
    public PlayerData Player = new();  
}
