using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ValueSliderUI : MonoBehaviour
{
    // Start is called before the first frame update
    public string valuetxt = "";
    public Slider slider;
    //public Gradient gradient;
    public Image ImageFill;
    [SerializeField] TextMeshProUGUI _valueText;
    public void SetMaxValueBar(uint maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
        //ImageFill.color = gradient.Evaluate(1f);
    }
    public void SetCurrentValueBar(int Value)
    {
        slider.value = Value;
        if (_valueText != null)
        {
            _valueText.text = Value.ToString() + "/" + slider.maxValue.ToString();
            if (!string.IsNullOrEmpty(valuetxt))
            {
                _valueText.text = valuetxt + " " + _valueText.text;
            }


        }
        //ImageFill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void UpdateSliderValue(float currentValue, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
        //ImageFill.color = gradient.Evaluate(currentValue / maxValue);
        if (_valueText != null)
        {
            _valueText.text = currentValue.ToString() + " / " + maxValue.ToString();
            if (!string.IsNullOrEmpty(valuetxt))
            {
                _valueText.text = valuetxt + " " + _valueText.text;
            }
        }

    }
    public void UpdateSliderValue(float minValue, float currentValue, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        slider.value = currentValue;
        //ImageFill.color = gradient.Evaluate(currentValue / maxValue);
        if (_valueText != null)
        {
            _valueText.text = currentValue.ToString() + " / " + maxValue.ToString();
            if (!string.IsNullOrEmpty(valuetxt))
            {
                _valueText.text = valuetxt + " " + _valueText.text;
            }
        }
    }
}