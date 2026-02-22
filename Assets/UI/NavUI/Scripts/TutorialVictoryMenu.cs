using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialVictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuButton;
    
    public void OpenMainMenu() => SceneManager.LoadScene("MenuScene");
    
}
