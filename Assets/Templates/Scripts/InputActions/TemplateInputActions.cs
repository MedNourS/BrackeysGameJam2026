using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Unity 6 uses the new Input Actions system for handling inputs
/// While less intuitive than the former Input.GetKey input system,
/// its worth it because it makes rebinding inputs way easier
/// </summary>
public class TemplateInputActions : MonoBehaviour
{
    private InputAction moveAction;

    // Actions need to be enabled and disabled
    private void OnEnable()
    {
        moveAction = InputManager.Actions.FindActionMap("Player").FindAction("Move");
        moveAction.Enable();
    }

    private void OnDisable() => moveAction.Disable();

    private void Update()
    {
        // Use ReadValue<ValueType>() to continuously poll the value produced by the input action
        Vector2 input = moveAction.ReadValue<Vector2>();
        Debug.Log(input);

        // Use triggered for a one-frame "OnPress" pulse
        if (moveAction.triggered) 
        {
            Debug.Log("triggered!");
        }

        /// There are also these methods for polling input (all return bools)
        /// WasPressedThisFrame()
        /// IsPressed()
        /// WasReleasedThisFrame()
    }
}
