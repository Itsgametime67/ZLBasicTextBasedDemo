using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;
    private GameData _currentData = new();

    public void SaveGame()
    {
        _currentData.Player = Player.ToData();
        SaveManager.Save(_currentData);
        Debug.Log("Saved!");
    }

    public void LoadGame()
    {
        if (SaveManager.TryLoad(out _currentData))
        {
            Player.FromData(_currentData.Player);
            Debug.Log("Loaded!");
        }
        else
        {
            Debug.LogWarning("No save file found. Starting new one.");
            SaveGame();
        }
    }

    private void Start()
    {
        LoadGame();
    }
}
