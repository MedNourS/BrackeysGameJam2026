using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health;

    public UnityEvent OnStart;
    public UnityEvent OnHurt;
    public UnityEvent OnFinalHit; // Before the flash-red occurs
    public UnityEvent OnDeath; // After the flash

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float halfFlashTime = 0.5f;

    [SerializeField] private Color defaultColor = new Color(0.259f, 0.620f, 0.522f, 1.0f);
    [SerializeField] private Color hurtColor = new Color(0.773f, 0.318f, 0.267f, 1.0f);
    [SerializeField] private SpriteRenderer sprite;

    private Coroutine hurtFlash;

    private void Start()
    {
        sprite.color = defaultColor;

        health = maxHealth;
        OnStart?.Invoke();
    }

    // Deal damage (or kill) the attached enemy/player
    public void Damage(float damage)
    {
        health -= damage;

    }

    // Flash the enemy red for a moment, before either returning them back to normal or killing them completely
    
}
