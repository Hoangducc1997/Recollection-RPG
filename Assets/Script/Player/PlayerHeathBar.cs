using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBar : MonoBehaviour
{
    [SerializeField] Image fillHealthBar;
    [SerializeField] TextMeshProUGUI valueTextHealth;

    public void UpdateHealthBar(int currentValueHealth, int maxValueHealth)
    {
        fillHealthBar.fillAmount = (float)currentValueHealth / maxValueHealth;
        if (currentValueHealth >= 0)
        {
            valueTextHealth.text = currentValueHealth.ToString() + " / " + maxValueHealth.ToString();
        }
    }
}
