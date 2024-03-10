using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform weaponPoint;

    void Update()
    {
        PlayerFlip();
        WeaponRotate();
    }

    private void PlayerFlip() // �÷��̾ ���콺 ��ġ�� ���� �¿�� �ٶ󺸵��� ����
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < PlayerTransform.position.x)
        {
            PlayerTransform.localScale = new Vector2(-1, 1);
        }
        else
        {
            PlayerTransform.localScale = new Vector2(1, 1);
        }
    }

    private void WeaponRotate()  // ���콺 ��ġ�� ���󰡴� ���� ����
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector2 direction = mouseWorldPosition - weaponPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (PlayerTransform.localScale.x < 0)
        {
            angle -= 180f;
        }
        weaponPoint.rotation = Quaternion.Euler(0, 0, angle);
    }
}
