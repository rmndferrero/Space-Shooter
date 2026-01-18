using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject destructionFX;
    public static Player instance;

    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;

    // --- NEW VARIABLES ---
    public float invulnerabilityDuration = 0.5f; // How long to stay safe (1 second)
    private bool isInvulnerable = false;         // Are we currently safe?
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void GetDamage(int damage)
    {
        // 1. STOP if we are currently immune!
        if (isInvulnerable) return;

        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth > 0)
        {
            // 2. Start the immunity timer and flashing
            StartCoroutine(InvulnerabilityRoutine());
        }
        else
        {
            Destruction();
        }
    }

    // --- NEW: Handle the blinking and timer ---
    IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        // Visual Feedback: First flash RED to show the hit
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;

        // Visual Feedback: Now BLINK for the rest of the duration
        // We divide the time into small chunks to create the flicker effect
        float timer = 0;
        float blinkSpeed = 0.1f; // How fast to blink

        while (timer < invulnerabilityDuration)
        {
            // Toggle visibility (Classic arcade flashing)
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            timer += blinkSpeed;
        }

        // 3. Reset everything back to normal
        spriteRenderer.enabled = true; // Make sure ship is visible
        isInvulnerable = false;        // Player can get hit again
    }

    void Destruction()
    {
        // 1. Create the explosion effect
        Instantiate(destructionFX, transform.position, Quaternion.identity);

        // 2. Tell the Game Over Manager that we died
        if (GameOverManager.instance != null)
        {
            GameOverManager.instance.TriggerGameOver();
        }

        // 3. Destroy the player object
        Destroy(gameObject);
    }
}