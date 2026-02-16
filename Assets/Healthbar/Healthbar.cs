using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public static Healthbar Instance { get; private set; }

    [SerializeField] private Slider slider;

    private Health Health;

    private void Awake()
    {
        Instance = this;
    }

    // private void Start() => Health = PlayerManager.Instance.Health;

    public void InitHealth()
    {
        float health = Health.health;

        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth()
    {
        float health = Health.health;

        slider.value = health;
    }
}
