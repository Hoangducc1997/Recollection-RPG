using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{
    [SerializeField] protected Slider _slider;
    [SerializeField] Image _icon;
    [SerializeField] Sprite _spriteOn, _spriteOff;

    protected bool _isMute = true;
    protected float _defaultValueWhenOn = 0.5f;

    private void OnEnable()
    {
        LoadDataFromSetting();
    }

    protected virtual void LoadDataFromSetting() { }

    public virtual void SaveSetting() { }

    public virtual void RevertSetting()
    {
        LoadDataFromSetting();
    }


    protected void HandleSlider(float value)
    {
        _slider.value = value;

        if (value == 0)
        {
            _isMute = true;
            _icon.sprite = _spriteOff;
        }
        else
        {
            _isMute = false;
            _icon.sprite = _spriteOn;
        }

        HandleOutPutVolume(_slider.value);
    }

    protected virtual void HandleOutPutVolume(float value) { }

    public void OnValueSliderChanged(float value)
    {
        if (value == 0)
        {
            _isMute = true;
            _icon.sprite = _spriteOff;
        }
        else
        {
            _isMute = false;
            _icon.sprite = _spriteOn;
        }

        HandleOutPutVolume(value);
    }

    public float GetSliderValue()
    {
        return _slider.value;
    }
}
