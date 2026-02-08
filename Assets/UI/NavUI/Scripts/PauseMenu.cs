using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Universally accessible (useful for audio managers and stuff)
    public static bool gamePaused;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject confirmUI;

    // Toggle pause menu with [Esc] key
    private void Update()
    {
        bool foo = false;
        if (foo) // Input esc
        { 
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }

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
