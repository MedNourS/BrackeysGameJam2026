using UnityEngine;
using UnityEngine.UI;

public class LockLevels : MonoBehaviour
{
    [SerializeField] private GameObject levelContainer;
    private int levelCount;

    // Adjust lock whenever the menu is opened
    private void Start() => SetLockedLevels();

    public void SetLockedLevels()
    {
        // Get number of buttons
        levelCount = levelContainer.transform.childCount;

        // Access from persistent save storage
        int unlocked = SaveSystem.UnlockedLevels;
        Button[] buttons = levelContainer.GetComponentsInChildren<Button>();

        for (int i = 1; i <= levelCount; i++)
        {
            // Only set button to interactable if it is unlocked or previous level
            buttons[i].interactable = i <= unlocked;
        }
    }
}
