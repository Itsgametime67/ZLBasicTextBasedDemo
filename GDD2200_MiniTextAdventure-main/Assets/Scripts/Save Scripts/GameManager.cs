using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;
    private GameData _currentData = new();

    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _gameScreen;


    
    public void SaveGame()
    {
        _currentData.Player = Player.ToData();
        SaveManager.Save(_currentData);
        Debug.Log("Saved!");
    }

    public void LoadGame()
    {
        _menuScreen.SetActive(false);//disable the menu screen
        _gameScreen.SetActive(true);
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

    public void StartNewGame()
    {
        _gameScreen.SetActive(true);
        _menuScreen.SetActive(false);
        
        SaveGame();
    }

     
    
}
