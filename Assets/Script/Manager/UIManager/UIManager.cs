using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] List<UIPopup> _UIPopupList = new List<UIPopup>();
    private UIPopup _currentPopup;
    private UIPopup _lastPopup;
    public void ShowPopup(PopupName popupName, object popupParamenter = null, bool hideOther = false)
    {
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
                popup.OnShown(popupParamenter);
                
                if(hideOther)
                {
                    HidePopup(_currentPopup.GetPopupName());
                    _lastPopup = _currentPopup;
                }

                _currentPopup = popup;

                break;
            }
        }
    }

    public void HidePopup(PopupName popupName, bool isShowLast = false)
    {
        if (_currentPopup != null)
        {
            if (_currentPopup.GetPopupName() == popupName)
            {
                _currentPopup.OnHide();
                return;
            }
        }
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
                popup.OnHide();
            }
        }

        if(isShowLast)
        {
            _lastPopup.OnShown();
        }
    }
}
