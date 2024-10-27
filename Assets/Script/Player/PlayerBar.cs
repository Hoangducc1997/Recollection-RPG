using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBar : MonoBehaviour
{
    [SerializeField] Image fillHealthBar;
    [SerializeField] TextMeshProUGUI valueTextHealth;
    [SerializeField] Image fillExpBar;
    [SerializeField] TextMeshProUGUI valueTextExp;

    public void UpdateHealthBar(int currentValueHealth, int maxValueHealth)
    {
        fillHealthBar.fillAmount = (float)currentValueHealth / maxValueHealth;
        if (currentValueHealth >= 0)
        {
            valueTextHealth.text = currentValueHealth.ToString() + " / " + maxValueHealth.ToString();
        }
    }

    public void UpdateExpBar(int currentExp, int maxExp)
    {
        fillExpBar.fillAmount = (float)currentExp / maxExp;
        valueTextExp.text = currentExp.ToString() + " / " + maxExp.ToString();
    }
}
