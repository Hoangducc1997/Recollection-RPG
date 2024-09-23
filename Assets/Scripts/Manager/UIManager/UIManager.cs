using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<UIPopup> _UIPopupList = new List<UIPopup>();
    
    public void ShowPopup(PopupName popupName, object popupParamenter= null)
    {
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
                popup.SetParamenter(popupParamenter);
                popup.OnShown();

            }
        }
    }

    public void HidePopup(PopupName popupName)
    {
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
               
                popup.OnHide();

            }
        }
    }
}
