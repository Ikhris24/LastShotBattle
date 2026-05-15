using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayers;

    [Header("Player")]
    public Movement player;

    // This function is called during the attack animation
    public string enemyTag;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            playerLayers);

        foreach (Collider2D target in hits)
        {
            // ONLY hit enemy
            if (!target.CompareTag(enemyTag))
                continue;

            Health health = target.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
    }