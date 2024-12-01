using UnityEngine;

public class SkillPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] skillPanels; // Mảng các GameObject tương ứng với các level (Level2, Level3, ...)
    private int currentLevel = 0; // Level hiện tại của nhân vật

    public void UpdateSkillPanel(int playerLevel)
    {
        currentLevel = playerLevel;

        for (int i = 0; i < skillPanels.Length; i++)
        {
            // Chỉ mở các panel nếu level >= 2 và panel phù hợp với level
            if (currentLevel >= 1 && i < currentLevel)
            {
                skillPanels[i].SetActive(true);
            }
            else
            {
                skillPanels[i].SetActive(false);
            }
        }
    }


    private void Start()
    {
        // Ẩn tất cả skillPanels khi bắt đầu chơi
        foreach (var panel in skillPanels)
        {
            panel.SetActive(false);
        }
    }
}
