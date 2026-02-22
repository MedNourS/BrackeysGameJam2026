using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        PlayerController.Instance.Context.isInTutorial = true;
        gameObject.SetActive(true);
        gameUI.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public void StartGame()
    {
        PlayerController.Instance.Context.isInTutorial = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameUI.SetActive(true);
        PauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
