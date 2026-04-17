using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool invulernable = false;

    [Header("UI")]
    public Image healthBarFill;

    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        
    }


    public void TakeDamage(float damage)
    {
        if (invulernable) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        //Take knockback according to other enemies direction
        TakeKnockBack(movement.enemy.transform);

        //Set player to be invincible for a brief moment to prevent spam
        Invoke(nameof(ResetInvulernability), 0.3f);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    public void TakeKnockBack(Transform attacker)
    {
        //Disable player movement to allow RB to be manipulated here - Then re enable after a short time
        movement.isKnockedBack = true;
        Invoke(nameof(ResetKnockback), 0.3f);

        Vector2 direction = (transform.position - attacker.position).normalized;

        direction.y = Mathf.Clamp(direction.y + 0.5f, 0.3f, 1f);

        movement.rb.linearVelocity = Vector2.zero;
        movement.rb.AddForce(direction * 5f, ForceMode2D.Impulse);
    }

    //This function re enables the isKnockedBack to false. This allows the RB to be moved so movement is temporarily disabled
    public void ResetKnockback()
    {
        movement.isKnockedBack = false;
    }

    public void ResetInvulernability()
    {
        invulernable = false;
    }

    
}
