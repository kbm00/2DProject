using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(attackDamage);
        }
    }
}
