using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public static Healthbar Instance { get; private set; }

    [SerializeField] private Slider slider;

    private Health Health;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    //private void Start() => Health.health = 100f;
    private void Start() => slider.value = 100f;

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.isPressed) //Change this to the variable of in or out of zone, basically just adding the condition for it to go down
        {
            //Debug.Log("going");
            //Debug.Log(slider.value);
            slider.value -= 0.15f; //Add the health variable to this eventually
            //Debug.Log(slider.value);
        }
    }

    public void InitHealth()
    {
        float health = Health.health;

    }

    public void SetHealth()
    {
        float health = Health.health;

        slider.value = health;
    }
}
