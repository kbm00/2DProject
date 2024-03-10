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

    private void PlayerFlip() // 플레이어가 마우스 위치에 따라 좌우로 바라보도록 적용
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

    private void WeaponRotate()  // 마우스 위치를 따라가는 무기 적용
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
