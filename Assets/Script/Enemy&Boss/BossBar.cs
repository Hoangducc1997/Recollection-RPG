using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossBar : MonoBehaviour
{
    [SerializeField] Image fillHealthBar;
    [SerializeField] TextMeshProUGUI valueTextHealth;
    [SerializeField] Image fillAngryBar;
    [SerializeField] TextMeshProUGUI valueTextAngry;

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
        fillAngryBar.fillAmount = (float)currentExp / maxExp;
        valueTextAngry.text = currentExp.ToString() + " / " + maxExp.ToString();
    }
}
