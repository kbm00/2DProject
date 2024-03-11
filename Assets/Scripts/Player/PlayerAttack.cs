using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(attackDamage);
        }

        // �浹�� ������Ʈ�� ���̳� �������� Ȯ��
        if (collision.CompareTag("Wall") || collision.CompareTag("Monster"))
        {
            // �Ѿ� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
    

}
