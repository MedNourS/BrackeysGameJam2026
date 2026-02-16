using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    [SerializeField] private GameObject regular;
    [SerializeField] private GameObject strange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapModels(false);
    }

    public void SwapModels(bool strangified)
    {
        if (strangified)
        {
            regular.SetActive(false);
            strange.SetActive(true);
        } else
        {
            regular.SetActive(true);
            regular.SetActive(false);
        }
        Debug.Log(strangified);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount < 300)
        {
            SwapModels(true);
        }
    }
}
