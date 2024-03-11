using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���� ��� ó�� �߰�
        Destroy(gameObject);
    }
}
