using UnityEngine;
using System;
using System.IO;

public static class SaveManager 
{
    private static readonly string PathToFile = Application.persistentDataPath + "/GameSaveFile.json";

    public static void Save(GameData data)
    {
        data.LastSaved = DateTime.UtcNow.ToString("o");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PathToFile, json);
        Debug.Log($"[Save Manager] saved game data to {PathToFile}");
    }

    public static GameData Load()
    {
        TryLoad(out GameData data);
        return data;
    }


    public static bool TryLoad(out GameData data)
    {
        try
        {
            if (!File.Exists(PathToFile))
            {
                data = new GameData();
                return false;
            }

            string json = File.ReadAllText(PathToFile);
            
            data = JsonUtility.FromJson<GameData>(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SaveManager] Load failed: {ex.Message}");
            data = new GameData();
            return false;
        }
    }
}
