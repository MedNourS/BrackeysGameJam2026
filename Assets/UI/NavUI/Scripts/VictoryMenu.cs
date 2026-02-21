using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void OpenMainMenu() => SceneManager.LoadScene("MenuScene");
	
	public void TryAgain() => SceneManager.LoadScene("GameScene");
}
