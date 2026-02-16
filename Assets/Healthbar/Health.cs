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
        OnHurt?.Invoke();

        if (hurtFlash != null)
            StopCoroutine(hurtFlash);

        if (health <= 0f)
        {
            OnFinalHit?.Invoke(); // Remove hitbox

            hurtFlash = StartCoroutine(FlashDamage(false)); // Just flash to red

            OnDeath?.Invoke(); // Destroy self
        }
        else
            hurtFlash = StartCoroutine(FlashDamage(true)); // Flash green -> red -> green
    }

    // Flash the enemy red for a moment, before either returning them back to normal or killing them completely
    private IEnumerator FlashDamage(bool fullAnimation)
    {
        float elapsed = 0f;
        float flashPeriod = halfFlashTime;

        if (fullAnimation)
            flashPeriod *= 2f;

        // Either just run the transition to red, or run the full animation
        while (elapsed < flashPeriod)
        {
            // Smoothly go from default to hurt then back to default
            elapsed += Time.deltaTime;
            float phase = Mathf.SmoothStep(0f, 1f, (1f - Mathf.Abs(elapsed / halfFlashTime - 1f)));
            
            sprite.color = Color.Lerp(defaultColor, hurtColor, phase);

            yield return null;
        }
    }
}
