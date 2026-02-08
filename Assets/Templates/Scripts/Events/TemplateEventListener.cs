using UnityEngine;

/// This pairs up with TemplateEvent.cs to listen for the event sent by TemplateEvent
/// (In this case, "foobarHappened")
public class TemplateEventListener : MonoBehaviour
{
    // Subscribe to the event ("listen" for it)
    private void OnEnable()
    {
        // Add the method that you want this event to trigger
        TemplateEvent.foobarHappened += ReactToFoobar;
    }

    // Unsubscribe (when the object is inactive/destroyed/disabled)
    private void OnDisable()
    {
        TemplateEvent.foobarHappened -= ReactToFoobar;
    }

    // This runs when the foobarHappened event is triggered
    private void ReactToFoobar(GameObject obj)
    {
        Debug.Log("Woof");
    }
}
