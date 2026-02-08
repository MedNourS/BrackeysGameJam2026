using UnityEngine;

public class TemplateInterfacedObject : MonoBehaviour, ITemplateInterface
{
    // Methods of an interface MUST BE PUBLIC 

    // Also, ALL methods that are present in the interface
    // must exist in objects that use the interface even if the methods are empty
    public void DoSomething(string thing) { }
    public void DoFoobar() { }
}
