using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject newGameButton;

    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject confirmationMenu;
    [SerializeField] private GameObject mainPanel;

    // When the menu loads and when it is shown/unhidden
    private void Start() => ManageContinueButton();
    private void OnEnable() => ManageContinueButton();

    // If not a new game, show the continue button
    private void ManageContinueButton()
    {
        SaveSystem.Load();
        continueButton.SetActive(!SaveSystem.NewGame);
    }

    // NewGame only warns the player if they have an existing save file
    public void NewGame()
    {
        SaveSystem.Load();
        
        // Make it not a new game and change menus
        if (SaveSystem.NewGame) {
            SaveSystem.NewGame = false;
            SaveSystem.Save();

            // Change menus
            levelMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        else // Warn the player using a ConfirmationMenu
        {
            confirmationMenu.SetActive(true);
            mainPanel.SetActive(false);
        }
    }

    public void ConfirmNewGame(bool confirm)
    {
        if (confirm)
            SaveSystem.Reset();

        confirmationMenu.SetActive(false);
        mainPanel.SetActive(true);
    }
}
