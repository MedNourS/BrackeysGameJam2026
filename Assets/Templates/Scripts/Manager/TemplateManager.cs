using UnityEngine;

public class TemplateManager : MonoBehaviour
{
    // Static means that this instance BELONGS TO THE CLASS
    // and can be referenced globally without risk of reassignment
    public static TemplateManager Instance { get; private set; }

    // On instantiation, ensure there is only one GameObject that has this manager attached
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DoSomething(int paramExample)
    {
        // Do something in another class with
        // TemplateManager.Instance.DoSomething(foo);
    }
}
