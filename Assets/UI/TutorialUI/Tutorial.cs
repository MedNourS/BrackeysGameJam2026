using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    private InputSystem_Actions controls;
    private string[] allTextBoxes = {"mouse", "movement", "wallmovement", "capture"};
    private string currentTextBox = "mouse";
    Vector2 startMousePos;
    Vector2 startPlayerPos;
    [SerializeField] private TMP_Text movementAndCamera;
    [SerializeField] private TMP_Text capture;
    [SerializeField] private GameObject removableWall;
    [SerializeField] private GameObject plant;
    [SerializeField] private GameObject tutorialVictoryMenu;


    void Awake()
    {
        startMousePos = controls.Player.Look.ReadValue<Vector2>();
        startPlayerPos = controls.Player.Move.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTextBox == "mouse")
        {
            if ((startMousePos + controls.Player.Look.ReadValue<Vector2>()).magnitude > 10)
            {
                currentTextBox = "movement";
                movementAndCamera.text = "To move around, use WASD.";
            }
        }
        else if (currentTextBox == "movement")
        {
            if ((startPlayerPos + controls.Player.Move.ReadValue<Vector2>()).magnitude > 10)
            {
                movementAndCamera.transform.parent.gameObject.SetActive(false);
                removableWall.SetActive(false);
                currentTextBox = "capture";
            }
        }
        else
        {
            //Checking if the plant is flesh (if theres a better way, do that)
            if (plant.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                tutorialVictoryMenu.SetActive(true);
            }
        }
    }
}
