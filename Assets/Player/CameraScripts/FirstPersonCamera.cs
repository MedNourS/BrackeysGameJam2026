using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    
    private float xRot = 0f;

    private InputAction lookAction;

    // Actions need to be enabled and disabled
    private void OnEnable()
    {
        lookAction = InputManager.Actions.FindActionMap("Player").FindAction("Look");
        lookAction.Enable();
    }

    private void OnDisable() => lookAction.Disable();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Read Mouse Delta
        Vector2 mouseInput = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        xRot -= mouseInput.y;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseInput.x);
    }
}
