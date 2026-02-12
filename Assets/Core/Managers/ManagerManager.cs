using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    public static ManagerManager Instance; // Universally accessible

    // On instantiation, ensure there is only one GameObject that has this manager attached
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            /// This is like the only reason we care about this script:
            /// Make sure that it is scene-persistent
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);
    }
    
}
