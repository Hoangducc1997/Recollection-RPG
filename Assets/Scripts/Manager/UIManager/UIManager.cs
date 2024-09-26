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
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] List<UIPopup> _UIPopupList = new List<UIPopup>();
    private UIPopup _currentPopup;
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
                }

                _currentPopup = popup;

                break;
            }
        }
    }

    public void HidePopup(PopupName popupName)
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
    }
}
