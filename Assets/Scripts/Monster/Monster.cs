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
        // ���� ��� ó�� �߰�
        Destroy(gameObject);
    }
    protected void FlipDirection(float moveDirection) //���� Flip ��ȯ
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
