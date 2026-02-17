using UnityEngine;
using UnityEngine.UI;

public class Health1 : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBarFill;

    [Header("Debug Settings")]
    public KeyCode debugDamageKey = KeyCode.J;
    public float debugDamageAmount = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Debug damage for Player 2
        if (Input.GetKeyDown(debugDamageKey))
        {
            TakeDamage(debugDamageAmount);
            Debug.Log(gameObject.name + " took damage. Current Health: " + currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " has been defeated!");
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
