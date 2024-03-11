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
        // 몬스터 사망 처리 추가
        Destroy(gameObject);
    }
}
