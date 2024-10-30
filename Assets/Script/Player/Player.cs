using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharaterStats[] characterStats; // Thông số nhân vật
    [SerializeField] private Joystick joystick; // Joystick cho di chuyển
    [SerializeField] private WeaponManager weaponManager; // Quản lý vũ khí
    [SerializeField] private string enemyTag = "Enemy"; // Tag cho kẻ thù

    private Animator animator; // Animator cho GamePlay
    private Vector2 movement;
    private bool isFacingRight = true;
    private int currentLevel;
    private float lastAttackTime;
    private bool isAttacking; // Biến trạng thái tấn công
    private bool isRunning; // Biến trạng thái chạy

    void Start()
    {
        animator = GetComponent<Animator>();
        currentLevel = 1;
    }

    void Update()
    {
        PlayerMove(new Vector2(joystick.Direction.x, joystick.Direction.y));
        FlipCharacter();

        // Cập nhật trạng thái di chuyển
        isRunning = movement.magnitude > 0.2f; // Kiểm tra xem có đang di chuyển hay không
        animator.SetBool("isRunning", isRunning); // Cập nhật animator

        // Chuyển đổi vũ khí với các phím số
        if (!isAttacking) // Chỉ cho phép chuyển đổi vũ khí nếu không đang tấn công
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
            // Thêm các phím số khác nếu cần
        }

        // Tự động tấn công nếu có kẻ thù trong phạm vi tấn công và không đang tấn công
        if (!isAttacking)
        {
            float weaponRange = weaponManager.GetCurrentWeapon()?.rangeAtk ?? 0;
            GameObject[] enemiesInRange = FindEnemiesInRange(weaponRange);

            if (enemiesInRange.Length > 0)
            {
                Attack(); // Gọi hàm tấn công khi có kẻ thù trong phạm vi
            }
        }
    }


    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            float moveSpeed = characterStats[currentLevel - 1].moveSpeed;
            transform.position += (Vector3)(movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void PlayerMove(Vector2 newJoystickPosition)
    {
        movement.x = newJoystickPosition.x;
        movement.y = newJoystickPosition.y;

        if (movement.magnitude < 0.2f)
        {
            movement = Vector2.zero; // Dừng di chuyển khi không có tác động
        }
    }

    private void FlipCharacter()
    {
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void SwitchWeapon(int weaponIndex)
    {
        weaponManager.SwitchWeapon(weaponIndex); // Đổi vũ khí hiện tại
    }

    private void Attack()
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            // Lấy tất cả kẻ thù trong phạm vi tấn công
            GameObject[] enemies = FindEnemiesInRange(currentWeapon.rangeAtk);

            // Kiểm tra nếu có kẻ thù trong vùng
            if (enemies.Length > 0)
            {
                // Kiểm tra thời gian hồi chiêu
                if (Time.time >= lastAttackTime + currentWeapon.cooldownTime)
                {
                    // Trigger attack animation and set attacking status
                    isAttacking = true;
                    animator.SetBool("isAttacking", true);

                    // Tấn công tất cả kẻ thù trong vùng
                    foreach (GameObject enemy in enemies)
                    {
                        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(currentWeapon.damage);
                            Debug.Log("Attacked " + enemy.name + " dealing " + currentWeapon.damage + " damage.");
                        }
                    }

                    lastAttackTime = Time.time;

                    // Khôi phục trạng thái không tấn công sau attackDuration
                    Invoke("ResetAttack", currentWeapon.attackDuration);
                }
            }
        }
    }

    private GameObject[] FindEnemiesInRange(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange.ToArray();
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        Debug.Log("Attack reset. isAttacking: " + isAttacking);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        float rangeToDraw = currentWeapon != null ? currentWeapon.rangeAtk : 0; // Default to 0 if no weapon

        // Thêm kiểm tra để đảm bảo giá trị rangeToDraw không bằng 0
        if (rangeToDraw > 0)
        {
            Gizmos.DrawWireSphere(transform.position, rangeToDraw);
        }
    }

}
