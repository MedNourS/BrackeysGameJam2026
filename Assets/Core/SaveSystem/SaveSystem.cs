/// <summary>
/// This is not at all scaleable but data is gonna be tiny so who tf cares
/// 
/// Stores all game data in one JSON blob. Game data is accessible through individual variable methods
/// The data saved is structured based on SaveData
/// 
/// Please run SaveSystem.Save() whenever data needs to be stored (ex at the end of levels), as setting variables DOES NOT AUTOSAVE
/// </summary>

using UnityEngine;

public class SaveSystem
{
    private const string SaveKey = "GameSave";
    private static SaveData currentSave; // Save exists as part of the class

    // PlayerPrefs access data through keys
    public static void Initialize()
    {
        // Don't make new save if save already exists
        if (PlayerPrefs.HasKey(SaveKey)) 
        {
            Load();
        }
        else
        {
            currentSave = new SaveData();
            Save();
        }
    }

    // Convert SaveData type to json and save it in playerPrefs
    public static void Save()
    {
        string json = JsonUtility.ToJson(currentSave);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }
    
    // Load from playerPrefs into SaveData
    public static void Load()
    {
        string json = PlayerPrefs.GetString(SaveKey);
        currentSave = JsonUtility.FromJson<SaveData>(json);
    }

    public static void Reset()
    {
        // Remove the key to reset the stored saved data
        PlayerPrefs.DeleteKey(SaveKey);
        currentSave = new SaveData();
        Save();
    }

    // Add more of these methods for whatever variables need to be accessed
    // Accessed like SaveSystem.UnlockedLevels

    public static bool NewGame
    {
        get => currentSave.newGame;
        set => currentSave.newGame = value;
    }

    public static int UnlockedLevels
    {
        get => currentSave.unlockedLevels;
        set { 
            currentSave.unlockedLevels = value;
            // Optional:
            //Save();
        }
    }
}
