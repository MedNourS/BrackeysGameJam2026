using UnityEngine;

public class InteractableTemplate : MonoBehaviour, IInteractable
{
    // Object is looked at
    public void OnFocus() { }

    // Object stops being looked at
    public void OnLoseFocus() { }

    // Interaction key triggered while looking at object
    public void OnInteract() { }
}
