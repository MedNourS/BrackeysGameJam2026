using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Universally accessible (useful for audio managers and stuff)
    public static bool gamePaused;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject confirmUI;

    private InputAction cancelAction;

    // Actions need to be enabled and disabled
    private void OnEnable()
    {
        cancelAction = InputManager.Actions.FindActionMap("UI").FindAction("Cancel");
        cancelAction.Enable();
    }

    private void OnDisable() => cancelAction.Disable();

    // Toggle pause menu with [Esc] key
    private void Update()
    {
        if (cancelAction.triggered) // Input esc
        { 
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }

    // Methods are public so they can be accessed with UnityEvents attached to buttons
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    
    public void OpenSettings()
    {
        Debug.Log("Opened settings");
    }

    // Pull up the "Are you sure you want to quit?" menu,
    // or return to the original pause menu from the confirmation menu
    public void PromptMainMenu(bool isReturning)
    {
        pauseUI.SetActive(isReturning);
        confirmUI.SetActive(!isReturning);
    }

    // Accessed by the confirmation menu
    public void OpenMainMenu() => SceneManager.LoadScene("MenuScene");
}
