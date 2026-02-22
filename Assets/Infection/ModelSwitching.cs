using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    [SerializeField] private GameObject regular;
    [SerializeField] private GameObject strange;
    /// <summary>
    /// Swaps normal model with the strange model depending on the field
    /// </summary>
    /// <param name="strangified">represents whether thse model should be strange or not</param>
    public void SwapModels()
    {
        regular.SetActive(false);
        strange.SetActive(true);
    }

}
