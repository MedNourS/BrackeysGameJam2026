using UnityEngine;
using UnityEngine.InputSystem;

// Blueprint that all interactable objects have which determines their functions
public interface IInteractable
{
    void OnFocus();     // Player looks at it
    void OnLoseFocus(); // Player looks away from it
    void OnInteract();  // Player interacts with it
}

/// TODO: Compatibility with new pc and third person cam
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private Camera cam;

    // For interactables but also terrain, interaaction checking will happen with the interface anyway
    [SerializeField] private LayerMask collisionMask;

    private InputAction interactAction;

    private IInteractable currInteracting;

    // Input action handling
    private void OnEnable()
    {
        // Find actions by path
        interactAction = actions.FindAction("Player/Interact");
        interactAction.Enable();

        // Subscribe to HandleInteract so it triggers on press
        interactAction.performed += HandleInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= HandleInteract;
        interactAction.Disable();
    }

    private void Update()
    {
        ///if (playerController.currentState == PlayerController.PlayerState.Default)
        ///    HandleLook(new Ray(cam.transform.position, cam.transform.forward));
    }

    // Look along a ray and check for interactables along it, if found trigger interact methods
    // First-person and third-person compatible depending on the inputted look ray
    private void HandleLook(Ray lookRay)
    {
        // Raycast check on terrain + interact layers b/c we dont want the raycast to phase thru walls
        if (Physics.Raycast(lookRay, out RaycastHit hit, interactRange, collisionMask))
        {
            // Hit is interactable
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            // If different interactable was found than current interactable
            if (currInteracting != interactable && interactable != null)
            {
                currInteracting?.OnLoseFocus(); // Only happens if somehow a new interactable moves in front of the old one
                currInteracting = interactable;
                currInteracting.OnFocus();
            }
        }
        // If look away (no raycast hit)
        else if (currInteracting != null)
        {
            currInteracting.OnLoseFocus();
            currInteracting = null;
        }
    }
 
    private void HandleInteract(InputAction.CallbackContext ctx)
    {
        ///if (currInteracting != null && playerController.currentState == PlayerController.PlayerState.Default)
        ///    currInteracting.OnInteract();
    }
}