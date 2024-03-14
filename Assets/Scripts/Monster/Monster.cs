using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    public float health;

    public void TakeDamage(float damage)
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
    protected void FlipDirection(float moveDirection) //몬스터 Flip 전환
    {
        if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    } 
}
