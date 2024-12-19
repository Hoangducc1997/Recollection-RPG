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

    private Image chooseWeaponImage;
    private Image weapon1Image;
    private Image weapon2Image;
    private Image weapon3Image;

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

        chooseWeaponImage = chooseWeaponButton.GetComponent<Image>();
        weapon1Image = weapon1Button.GetComponent<Image>();
        weapon2Image = weapon2Button.GetComponent<Image>();
        weapon3Image = weapon3Button.GetComponent<Image>();

        chooseWeaponButton.onClick.AddListener(ToggleWeaponMenu);
        weapon1Button.onClick.AddListener(() => SelectWeapon(0, WeaponType.Sword, weapon1Image.sprite));
        weapon2Button.onClick.AddListener(() => SelectWeapon(1, WeaponType.Bow, weapon2Image.sprite));
        weapon3Button.onClick.AddListener(() => SelectWeapon(2, WeaponType.Magic, weapon3Image.sprite));
    }

    private void ToggleWeaponMenu()
    {
        isWeaponMenuActive = !isWeaponMenuActive;
        weaponMenu.SetActive(isWeaponMenuActive);
    }

    private void SelectWeapon(int weaponIndex, WeaponType weaponType, Sprite selectedWeaponSprite)
    {
        // Thêm âm thanh khi đổi vũ khí
        AudioManager.Instance.PlayVFX("PickupItem");

        Debug.Log($"Switching to weapon index: {weaponIndex} with sprite: {selectedWeaponSprite}");
        if (selectedWeaponSprite == null)
        {
            Debug.LogError("Selected weapon sprite is null! Check weapon button images.");
            return;
        }

        weaponLevelsManager.SwitchWeapon(weaponIndex, weaponType);

        isWeaponMenuActive = false;
        weaponMenu.SetActive(false);

        UpdateWeaponButtons(weaponIndex);

        chooseWeaponImage.sprite = selectedWeaponSprite;
        Debug.Log("Updated chooseWeaponButton image successfully!");
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
