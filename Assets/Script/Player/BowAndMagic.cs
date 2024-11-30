using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    [SerializeField] private WeaponRangedDatabase weaponDatabase; // Danh sách các vũ khí
    [SerializeField] private Transform shootPoint;  // Vị trí xuất phát mũi tên
    [SerializeField] private Animator bowAnimator;  // Animator của cây cung
    [SerializeField] private GameObject arrowPrefab; // Prefab mũi tên
    private WeaponRangedStats currentWeaponStats;   // Vũ khí hiện tại

    private float lastAttackTime;  // Thời gian bắn lần cuối

    // Cập nhật vũ khí hiện tại (Có thể thay đổi vũ khí theo cấp độ)
    public void SetWeapon(string weaponName, int level)
    {
        currentWeaponStats = weaponDatabase.weapons.Find(w => w.weaponName == weaponName && w.level == level);
        if (currentWeaponStats != null)
        {
            Debug.Log($"Vũ khí hiện tại: {currentWeaponStats.weaponName}, cấp {currentWeaponStats.level}, sát thương: {currentWeaponStats.damage}");
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy vũ khí '{weaponName}' cấp {level}");
        }
    }

    void Update()
    {
        if (currentWeaponStats == null) return;  // Nếu không có vũ khí, không làm gì cả

        // Kiểm tra thời gian hồi chiêu và bắn khi nhấn phím
        if (Time.time >= lastAttackTime + currentWeaponStats.cooldownTime && Input.GetButtonDown("Fire1"))
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        if (currentWeaponStats == null) return; // Kiểm tra vũ khí có tồn tại không

        // Kích hoạt animation bắn cung
        bowAnimator.SetTrigger("Shoot");

        // Tạo mũi tên
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        // Tính hướng bắn theo kẻ thù
        Vector2 direction = (FindEnemyDirection() - (Vector2)shootPoint.position).normalized;

        // Thêm lực vào mũi tên để nó bay
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * currentWeaponStats.rangeAtk;

        // Lưu thời gian bắn để tính thời gian hồi chiêu
        lastAttackTime = Time.time;

        // Áp dụng sát thương cho mũi tên
        ArrowAndMagicFly arrowScript = arrow.GetComponent<ArrowAndMagicFly>();
        if (arrowScript != null)
        {
            arrowScript.SetDamage(currentWeaponStats.damage);
        }
    }

    private Vector2 FindEnemyDirection()
    {
        GameObject enemy = GameObject.FindWithTag("Enemy"); // Lấy kẻ thù (có thể thay thế theo cách khác)
        if (enemy != null)
        {
            return enemy.transform.position;
        }
        else
        {
            // Nếu không có enemy, bắn theo hướng mặc định
            return transform.position + Vector3.right; // Ví dụ bắn sang phải
        }
    }
}
