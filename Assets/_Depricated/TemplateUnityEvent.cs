using UnityEngine;
using UnityEngine.Events;

public class TemplateUnityEvent : MonoBehaviour
{
    // Note UnityEvents cant easily take inputs whereas C# events can
    public UnityEvent exampleUnityEvent;

    // This method runs when something enters the collider of the object this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Example trigger when an object tagged foobar enters
        // the isTrigger collider of the object this script is attached to
        if (other.CompareTag("foobar"))
        {
            exampleUnityEvent.Invoke();
        }
    }

    // Assign this to fire to the event in-editor
    public void UnityEventTestMethod()
    {
        Debug.Log("Woof");
    }
}
