using UnityEngine;

public class ProgressManager : MonoBehaviour
{

    public static ProgressManager Instance;
    private float progress;

    void Start()
    {
        if(Instance != null) return;
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void UpdateProgress()
    {
        CalculateProgress();
    }

    private void CalculateProgress()
    {
        float normalVolume = 0f;
        float infectedVolume = 0f;

        GameObject[] normalObjects = GameObject.FindGameObjectsWithTag("NormalObject");
        GameObject[] infectedObjects = GameObject.FindGameObjectsWithTag("InfectedObject");

        foreach(GameObject normalObject in normalObjects)
        {
            Vector3 size = normalObject.GetComponent<MeshFilter>().mesh.bounds.size;
            normalVolume += size.x * size.y * size.z;
        }
        foreach(GameObject infectedObject in infectedObjects)
        {
            Vector3 size = infectedObject.GetComponent<MeshFilter>().mesh.bounds.size;
            infectedVolume += size.x * size.y * size.z;
        }

        if(infectedVolume == 0) progress = 0;
        else progress = (normalVolume + infectedVolume) / infectedVolume;
    }

    public float getProgress()
    {
        return progress;
    }

    public void resetProgress()
    {
        progress = 0;
    }
}
