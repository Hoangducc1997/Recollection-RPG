using UnityEngine;
using UnityEngine.UI;


public class WeaponChangeManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Button chooseWeaponButton;
    // Các button của vũ khí
    [SerializeField] private Button weapon1Button;
    [SerializeField] private Button weapon2Button;
    [SerializeField] private Button weapon3Button;
    [SerializeField] private GameObject weaponObj;
    private bool isWeaponObjActive = false;
    void Start()
    {
        weaponObj.SetActive(false);
        chooseWeaponButton.onClick.AddListener(ToggleWeaponChoose);
        // Gán sự kiện khi nhấn vào các nút
        weapon1Button.onClick.AddListener(() => weaponManager.SwitchWeapon(0)); // Chọn vũ khí 1
        weapon2Button.onClick.AddListener(() => weaponManager.SwitchWeapon(1)); // Chọn vũ khí 2
        weapon3Button.onClick.AddListener(() => weaponManager.SwitchWeapon(2)); // Chọn vũ khí 3
    }

    private void ToggleWeaponChoose()
    {
        isWeaponObjActive = !isWeaponObjActive; // Đảo trạng thái
        weaponObj.SetActive(isWeaponObjActive); // Thay đổi trạng thái active của weaponObj
    }
}
