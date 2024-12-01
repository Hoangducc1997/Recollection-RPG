using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    private int playerSpeedLevel = 0; // Cấp độ hiện tại của kỹ năng tốc độ
    private int weaponLevel = 0; // Cấp độ hiện tại của kỹ năng vũ khí

    public PlayerMovement playerMovement; // Tham chiếu đến script quản lý nhân vật
    public PlayerAction playerAction;
    public WeaponManager weaponManager; // Tham chiếu đến script quản lý vũ khí

    public void IncreaseSpeed()
    {
        playerSpeedLevel++;
       // playerMovement.SetSpeed(playerSpeedLevel); // Gọi hàm thay đổi tốc độ nhân vật
        Debug.Log($"Speed Level Upgraded to: {playerSpeedLevel}");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        //weaponManager.UpgradeWeaponLevel(weaponLevel); // Gọi hàm nâng cấp vũ khí
        Debug.Log($"Weapon Level Upgraded to: {weaponLevel}");
    }
}
