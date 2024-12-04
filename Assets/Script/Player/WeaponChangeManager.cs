using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeManager : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private WeaponLevelManager weaponLevelManager;
    [SerializeField] private Button chooseWeaponButton;
    [SerializeField] private Button weapon1Button;
    [SerializeField] private Button weapon2Button;
    [SerializeField] private Button weapon3Button;
    [SerializeField] private GameObject weaponObj;

    private bool isWeaponObjActive = false;

    void Start()
    {
        weaponObj.SetActive(false);
        chooseWeaponButton.onClick.AddListener(ToggleWeaponChoose);

        weapon1Button.onClick.AddListener(() => SelectWeapon(0, "Sword"));
        weapon2Button.onClick.AddListener(() => SelectWeapon(1, "Bow"));
        weapon3Button.onClick.AddListener(() => SelectWeapon(2, "Magic"));
    }

    private void ToggleWeaponChoose()
    {
        isWeaponObjActive = !isWeaponObjActive;
        weaponObj.SetActive(isWeaponObjActive);
    }

    public void SelectWeapon(int weaponIndex, string weaponType)
    {
        weaponLevelManager.SwitchWeapon(weaponIndex);
        weaponLevelManager.SetWeaponType(weaponType); // Gọi thông qua instance
        weaponLevelManager.SetWeaponLevel(1);

        isWeaponObjActive = false;
        weaponObj.SetActive(false);
    }

}
