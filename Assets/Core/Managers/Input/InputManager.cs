using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Used with something like:
///     InputAction moveAction = InputManager.Actions.FindActionMap("Player").FindAction("Move"); 
/// </summary>
public class InputManager : MonoBehaviour
{
    // Universally accessible
    public static InputManager Instance;
    public static InputActionAsset Actions;

    [SerializeField] private InputActionAsset inputActionsAsset;

    // On instantiation, ensure there is only one GameObject that has this manager attached
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;

            // Assign from editor
            Actions = inputActionsAsset;
        }
        else
            Destroy(gameObject);
    }
}
