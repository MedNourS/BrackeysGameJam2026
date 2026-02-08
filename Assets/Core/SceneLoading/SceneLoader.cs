using UnityEngine;
using UnityEngine.SceneManagement;

/// !!IMPORTANT!!
/// Add all your scenes to the Build Profile by going to
/// File > Build Profiles > Scene List
/// And dragging/dropping all your scenes from the Assets folder into there

// This script exists so that scene loading can be controlled with unity events
public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    /// Instance loader (for events)
    // Scene loading is controlled by events attached to UI buttons outside of this script
    public void LoadScene() => SceneManager.LoadScene(sceneName);
}
