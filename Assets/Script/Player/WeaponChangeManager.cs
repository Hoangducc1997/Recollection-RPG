using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    // Các button của vũ khí
    [SerializeField] private Button weapon1Button;
    [SerializeField] private Button weapon2Button;
    [SerializeField] private Button weapon3Button;
    [SerializeField] private Button weapon4Button;
    [SerializeField] private RectTransform backgroundBox;
    // Thêm các button cho các vũ khí khác nếu cần

    private Animator playerAnimator;  // Khai báo animator của Player

    void Start()
    {
        // Gán sự kiện khi nhấn vào các nút
        weapon1Button.onClick.AddListener(() => SwitchWeapon(0)); // Khi nhấn nút 1, sẽ truyền giá trị 0
        weapon2Button.onClick.AddListener(() => SwitchWeapon(1)); // Khi nhấn nút 2, sẽ truyền giá trị 1
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        // Lấy Animator từ đối tượng Player (hoặc tên đối tượng của bạn nếu cần)
        playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>(); // Lấy Animator từ đối tượng có tag "Player"

        if (playerAnimator == null)
        {
            Debug.LogError("Player Animator not found! Make sure Player has an Animator.");
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        Debug.Log("Weapon Index: " + weaponIndex); // Kiểm tra giá trị trong console
        weaponManager.SwitchWeapon(weaponIndex);

        // Lấy Weapon từ WeaponManager
        Weapon selectedWeapon = weaponManager.GetCurrentWeapon(); // Lấy đối tượng Weapon
        if (selectedWeapon != null)
        {
            // Lấy Animation Index từ Weapon component
            int animationIndex = selectedWeapon.GetAnimationIndex(); // Lấy Animation Index từ Weapon component
            Debug.Log("Selected Weapon Animation Index: " + animationIndex); // Kiểm tra giá trị animationIndex

            // Kiểm tra xem animator có tồn tại không trước khi gọi SetInteger
            if (playerAnimator != null)
            {
                playerAnimator.SetInteger("isWeaponType", animationIndex); // Truyền giá trị animationIndex vào Animator
            }
            else
            {
                Debug.LogWarning("Player Animator is missing, cannot set weapon type!");
            }
        }
    }
    //backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo(); // Ẩn hộp thoại
}
