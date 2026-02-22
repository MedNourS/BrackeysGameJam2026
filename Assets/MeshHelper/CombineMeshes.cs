using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateMegaMesh();
    }

    private void CreateMegaMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        for(int i = 0; i < meshFilters.Length; i++)
        {
            if(meshFilters[i].gameObject.tag == "IgnoreMesh") continue;

            meshFilters[i].gameObject.AddComponent<MeshCollider>();
        }
    }
}
