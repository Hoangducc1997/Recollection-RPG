using UnityEngine;

public class PlayerExpManager : MonoBehaviour
{
    [SerializeField] int maxExp = 100;
    int currentExp;
    public PlayerExpBar expBar;

    void Start()
    {
        InitializeExp();
    }

    private void InitializeExp()
    {
        currentExp = 0;
        expBar.UpdateExpBar(currentExp, maxExp);
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        if (currentExp > maxExp) currentExp = maxExp; // Giới hạn exp tối đa
        expBar.UpdateExpBar(currentExp, maxExp);

        // Kiểm tra nếu đạt tối đa kinh nghiệm
        if (currentExp == maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Debug.Log("Level Up!"); // Logic lên cấp
        currentExp = 0;         // Reset exp
        expBar.UpdateExpBar(currentExp, maxExp); // Cập nhật thanh exp
    }
}
