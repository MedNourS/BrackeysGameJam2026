using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Unity 6 uses the new Input Actions system for handling inputs
/// While less intuitive than the former Input.GetKey input system,
/// its worth it because it makes rebinding inputs way easier
/// </summary>
public class TemplateInputActions : MonoBehaviour
{
    private InputSystem_Actions controls;

    private InputAction moveAction;
    private InputAction jumpAction;

    // Instantiate
    private void Awake()
    {
        controls = new InputSystem_Actions();

        moveAction = controls.Player.Move;
        jumpAction = controls.Player.Jump;
    }

    // Actions need to be enabled and disabled
    private void OnEnable()
    {
        // I can enable player controls as a whole
        controls.Player.Enable();

        // Or I could do them one-by-one
        /// moveAction.Enable();
        /// jumpAction.Enable();
    }

    private void OnDisable() => controls.Player.Disable();

    private void Update()
    {
        // Use ReadValue<ValueType>() to continuously poll the value produced by the input action
        Vector2 input = moveAction.ReadValue<Vector2>();
        Debug.Log(input);

        // Use triggered for a one-frame "OnPress" pulse
        if (jumpAction.triggered) 
        {
            Debug.Log("triggered!");
        }

        /// There are also these methods for polling input (all return bools)
        /// WasPressedThisFrame()
        /// IsPressed()
        /// WasReleasedThisFrame()
    }
}
