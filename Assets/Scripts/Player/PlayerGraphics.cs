using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGraphics : MonoBehaviour
{
   
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    

    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;
    

    void Update()
    {
        

        PlayerFlip();
        WeaponRotate();
    }

    private void PlayerFlip() // �÷��̾ ���콺 ��ġ�� ���� �¿�� �ٶ󺸵��� ����
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < playerTransform.position.x)
        {
            playerTransform.localScale = new Vector2(-1, 1);
        }
        else
        {
            playerTransform.localScale = new Vector2(1, 1);
        }
    }

    private void WeaponRotate()  // ���콺 ��ġ�� ���󰡴� ���� ����
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector2 direction = mouseWorldPosition - weaponPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerTransform.localScale.x < 0)
        {
            angle -= 180f;
        }
        weaponPoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Fire()
    {
       

        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // �÷��̾ ������ �ٶ󺸰� ���� ��
        if (playerTransform.localScale.x < 0)
        {
            // �߻�ü�� ȸ���� �����Ͽ� ��������
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 180);
        }
        else
        {
            // �÷��̾ �������� �ٶ󺸰� ���� �� �߻�ü�� ���� ȸ���� ����
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        rb.velocity = direction * bulletSpeed;
    }
    private void OnAttack(InputValue value)
    {
        Fire();
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // �߻�ü ����
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z�� ���� �����մϴ�.

        Vector2 fireDirection = mousePosition - firePoint.position;
        float fireAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        // �߻�ü�� ȸ���� �����մϴ�. playerTransform�� ���� ���¿� ������� �۵��մϴ�.
        projectile.transform.rotation = Quaternion.Euler(0, 0, fireAngle);

        // �߻�ü�� ���콺�� ���� ��Ȯ�� ���ư����� �ӵ��� �����մϴ�.
        rb.velocity = fireDirection.normalized * bulletSpeed;
    }
}

