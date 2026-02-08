using System;
using UnityEngine;

/// <summary>
/// C# actions (events) are much more powerful than UnityEvents
/// You can think of them similar to JS events
/// 
/// We create a custom event in this script and define the rules for running the event
/// Then every function that "listens" to the event (by subscribing in the OnEnable method)
/// can attach a function to run when the event is triggered
/// 
/// In JS: TemplateEventListener.gameObject.addEventListener("foobarHappened", handleFoobar);
/// 
/// See TemplateEventListener.cs for more details
/// 
/// </summary>
public class TemplateEvent : MonoBehaviour
{
    // C# Event with type
    public static Action<GameObject> foobarHappened;

    // This method runs when something enters the collider of the object this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Example trigger when an object tagged foobar enters
        // the isTrigger collider of the object this script is attached to
        if (other.CompareTag("foobar"))
        {
            // Run the event, and pass the gameObject to whatever functions are triggered by the event
            foobarHappened?.Invoke(other.gameObject);
        }
    }
}
