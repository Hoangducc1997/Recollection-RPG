using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour
{
    [SerializeField] Image fillExpBar;
    [SerializeField] TextMeshProUGUI valueTextExp;

    public void UpdateExpBar(int currentExp, int maxExp)
    {
        fillExpBar.fillAmount = (float)currentExp / maxExp;
        valueTextExp.text = currentExp.ToString() + " / " + maxExp.ToString();
    }
}
