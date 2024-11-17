using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponChoose = new List<GameObject>();

    private int currentWeaponIndex = 0;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AddWeapon(GameObject newWeapon)
    {
        if (!weaponChoose.Contains(newWeapon))
        {
            weaponChoose.Add(newWeapon);
            newWeapon.SetActive(false); // Tắt vũ khí ban đầu khi thêm vào danh sách
        }
    }


    public void SwitchWeapon(int weaponIndex)
    {
        // Kiểm tra index hợp lệ
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count) return;

        // Vô hiệu hóa vũ khí hiện tại
        weaponChoose[currentWeaponIndex].SetActive(false);

        // Kích hoạt vũ khí mới
        currentWeaponIndex = weaponIndex;
        ActivateWeapon(currentWeaponIndex);
    }

    private void ActivateWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count) return;

        GameObject selectedWeapon = weaponChoose[weaponIndex];
        selectedWeapon.SetActive(true);

        Weapon currentWeapon = selectedWeapon.GetComponent<Weapon>();
        if (currentWeapon != null && animator != null)
        {
            int animationIndex = currentWeapon.GetAnimationIndex();
            Debug.Log("Current Weapon Animation Index: " + animationIndex);  // Kiểm tra giá trị animationIndex
            animator.SetInteger("isWeaponType", animationIndex); // Đảm bảo rằng SetInteger nhận đúng giá trị
        }
    }




    public Weapon GetCurrentWeapon()
    {
        if (weaponChoose.Count > 0 && currentWeaponIndex >= 0 && currentWeaponIndex < weaponChoose.Count)
        {
            Weapon weaponComponent = weaponChoose[currentWeaponIndex].GetComponent<Weapon>();
            return weaponComponent; // Trả về đối tượng Weapon thay vì WeaponStats
        }
        return null;
    }

}
