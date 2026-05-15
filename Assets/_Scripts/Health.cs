using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool invulernable = false;

    [Header("Player")]
    public int playerNumber = 1; // 1 or 2

    [Header("FMOD Events")]
    [SerializeField] private EventReference hurtEvent;

    [Header("UI")]
    public Image healthBarFill;

    private Movement movement;
    private GameStart gameStart;

    void Start()
    {
        movement = GetComponent<Movement>();

        gameStart = FindObjectOfType<GameStart>();

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (invulernable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        RuntimeManager.PlayOneShot(hurtEvent);

        UpdateHealthBar();

        // Knockback
        if (movement.enemy != null)
        {
            TakeKnockBack(movement.enemy.transform);
        }

        // Temporary invulnerability
        invulernable = true;
        Invoke(nameof(ResetInvulernability), 0.3f);

        // KO Logic
        if (currentHealth <= 0)
        {
            if (playerNumber == 1)
            {
                gameStart.EndRound(2);
            }
            else
            {
                gameStart.EndRound(1);
            }
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        invulernable = false;
    }

    public void TakeKnockBack(Transform attacker)
    {
        movement.isKnockedBack = true;

        Invoke(nameof(ResetKnockback), 0.3f);

        Vector2 direction = (transform.position - attacker.position).normalized;

        direction.y = Mathf.Clamp(direction.y + 0.5f, 0.3f, 1f);

        movement.rb.linearVelocity = Vector2.zero;

        movement.rb.AddForce(direction * 5f, ForceMode2D.Impulse);
    }

    public void ResetKnockback()
    {
        movement.isKnockedBack = false;
    }

    public void ResetInvulernability()
    {
        invulernable = false;
    }
}