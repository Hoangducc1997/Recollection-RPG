using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager; // Quản lý kích hoạt vũ khí
    [SerializeField] private WeaponLevel weaponLevelManager; // Quản lý loại và cấp độ vũ khí
    [SerializeField] private Button chooseWeaponButton;
    [SerializeField] private Button weapon1Button;
    [SerializeField] private Button weapon2Button;
    [SerializeField] private Button weapon3Button;
    [SerializeField] private GameObject weaponObj; // UI chọn vũ khí

    private bool isWeaponObjActive = false;

    void Start()
    {
        weaponObj.SetActive(false); // Ẩn UI chọn vũ khí ban đầu
        chooseWeaponButton.onClick.AddListener(ToggleWeaponChoose);

        // Gán sự kiện nhấn vào các nút vũ khí
        weapon1Button.onClick.AddListener(() => SelectWeapon(0, "Sword"));
        weapon2Button.onClick.AddListener(() => SelectWeapon(1, "Bow"));
        weapon3Button.onClick.AddListener(() => SelectWeapon(2, "Magic"));
    }
    public void PickUpWeapon(int weaponIndex)
    {
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager chưa được gán!");
            return;
        }

        weaponManager.PickUpWeapon(weaponIndex); // Gọi hàm nhặt vũ khí trong WeaponManager
    }

    private void ToggleWeaponChoose()
    {
        isWeaponObjActive = !isWeaponObjActive; // Đảo trạng thái
        weaponObj.SetActive(isWeaponObjActive); // Hiển thị hoặc ẩn UI
    }

    public void SelectWeapon(int weaponIndex, string weaponType)
    {
        Debug.Log($"Đang chọn vũ khí: Index = {weaponIndex}, Type = {weaponType}");

        // Kiểm tra WeaponManager
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager chưa được gán!");
            return;
        }

        // Kiểm tra WeaponLevelManager
        if (weaponLevelManager == null)
        {
            Debug.LogError("WeaponLevelManager chưa được gán!");
            return;
        }

        // Kiểm tra Index hợp lệ
        if (weaponIndex < 0 || weaponIndex >= weaponManager.weaponChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        // Chuyển đổi vũ khí
        weaponManager.SwitchWeapon(weaponIndex);
        Debug.Log($"Đã kích hoạt vũ khí: {weaponType}");

        // Cập nhật loại vũ khí trong WeaponLevel
        weaponLevelManager.SetWeaponType(weaponType);

        // Thử cập nhật cấp độ vũ khí nếu cần (mặc định cấp 1)
        weaponLevelManager.SetWeaponLevel(1);

        // Tắt UI chọn vũ khí sau khi chọn
        isWeaponObjActive = false;
        weaponObj.SetActive(false);
    }
}
