using UnityEngine;

/// <summary>
/// An interface is like a blueprint: you know that all objects with this interface
/// will contain the same methods included in the interface. This is useful because you can
/// make code that runs with the interface instead of specifying it for a specific script, so you can
/// swap out the functionality of that method in different scripts without having to modify the implementation
/// logic for whatever code needs to use those methods
/// 
/// Ex: if I have a bunch of animal objects/scripts that can each play a sound. Without interfaces, I would 
/// have to access every different method on every different animal to see how to play a sound.
/// With an interface, I can just do animal.MakeSound(); 
/// because the interface makes all the animal objects "promise" that they have a MakeSound() method that I can call
/// 
/// Note how interfaces are normally named with I at the start
/// </summary>
public interface ITemplateInterface
{
    // List of methods that objects using this interface must have
    // Although actual definitions of these functions can vary wildly from object to object
    // As long as they all have the same types for inputs
    void DoSomething(string idk);
    void DoFoobar();
}

public class TemplateInterface : MonoBehaviour
{
    // Interface typed object used for example
    private ITemplateInterface objectWithInterface;

    // Example use for interfaces: if the raycast hits an object with the interface,
    // It triggers some of the methods on that object in the interface
    // (highly useful for interaction/pickup/throwing systems)
    private void Update()
    {
        // Normally you'd have some kind of ray coming out of the camera, if it looks at an object
        // With the interactable interface, then some of the interactable logic triggers
        if (Physics.Raycast(new Ray(Vector3.zero, Vector3.forward), out RaycastHit hit, 5f))
        {
            // Hit object has interface (assigns null if not)
            ITemplateInterface obj = hit.collider.GetComponent<ITemplateInterface>();

            // If obj hasnt been assigned before
            if (objectWithInterface != obj && obj != null)
            {
                objectWithInterface?.DoSomething("Assigned objectWithInterface to an obj found with raycast!!");
                objectWithInterface = obj;
                objectWithInterface.DoSomething("Hi Im the new obj!");
            }
        }
    }
}
