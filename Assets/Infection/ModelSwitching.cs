using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    [SerializeField] private GameObject regular;
    [SerializeField] private GameObject strange;
    [SerializeField] private bool isStrange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    /// <summary>
    /// Swaps normal model with the strange model depending on the field
    /// </summary>
    /// <param name="strangified">represents whether thse model should be strange or not</param>
    public void SwapModels(bool strangified)
    {
        regular.SetActive(!strangified);
        strange.SetActive(strangified);
    }

    // Update is called once per frame
    void Update()
    {
        SwapModels(isStrange);
    }
}
