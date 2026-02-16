using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    [SerializeField] private GameObject regular;
    [SerializeField] private GameObject strange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            strange.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
