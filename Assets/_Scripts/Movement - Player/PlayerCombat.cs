using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Attack
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask playerLayers;

    //Movement
    public Movement player;


    //This function is called at a certain frame of the attack.
    public void Attack()
    {
        Collider2D[] hitPlayeres = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach(Collider2D player in  hitPlayeres)
        {
            player.GetComponent<Health>().TakeDamage(10);
        }
    }


    //This is purely for showing off the circle of the player attack range in the Scene view. 
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
