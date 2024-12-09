using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeManager : MonoBehaviour
{
    [SerializeField] private WeaponLevelManager weaponLevelsManager;
    [SerializeField] private Button chooseWeaponButton;
    [SerializeField] private Button weapon1Button;
    [SerializeField] private Button weapon2Button;
    [SerializeField] private Button weapon3Button;
    [SerializeField] private GameObject weaponMenu;
    [SerializeField] private AudioSource weaponSwitchSound;

    private bool isWeaponMenuActive;

    void Start()
    {
        if (weaponMenu == null || chooseWeaponButton == null || weapon1Button == null ||
            weapon2Button == null || weapon3Button == null)
        {
            Debug.LogError("Các thành phần UI chưa được gán trong Inspector!");
            return;
        }

        weaponMenu.SetActive(false);
        chooseWeaponButton.onClick.AddListener(ToggleWeaponMenu);
        weapon1Button.onClick.AddListener(() => SelectWeapon(0, WeaponType.Sword));
        weapon2Button.onClick.AddListener(() => SelectWeapon(1, WeaponType.Bow));
        weapon3Button.onClick.AddListener(() => SelectWeapon(2, WeaponType.Magic));
    }

    private void ToggleWeaponMenu()
    {
        isWeaponMenuActive = !isWeaponMenuActive;
        weaponMenu.SetActive(isWeaponMenuActive);
    }

    private void SelectWeapon(int weaponIndex, WeaponType weaponType)
    {
        if (weaponSwitchSound != null) weaponSwitchSound.Play();

        weaponLevelsManager.SwitchWeapon(weaponIndex, weaponType);

        isWeaponMenuActive = false;
        weaponMenu.SetActive(false);

        UpdateWeaponButtons(weaponIndex);
    }

    private void UpdateWeaponButtons(int activeWeaponIndex)
    {
        Button[] weaponButtons = { weapon1Button, weapon2Button, weapon3Button };
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            weaponButtons[i].interactable = i != activeWeaponIndex;
        }
    }
}
