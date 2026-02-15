using UnityEngine;

/// <summary>
/// This manager is for managing Saves and GameState
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Universally accessible

    // On instantiation, ensure there is only one GameObject that has this manager attached
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Setup save system
        SaveSystem.Initialize();
    }
}
