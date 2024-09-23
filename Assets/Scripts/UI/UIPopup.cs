using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour, IPopup
{
    [Header("Popup Name")]
    [SerializeField] PopupName _popupName;
    private object parament;

    #region Unity Function

    #endregion

    #region UI Function
    public void OnShown()
    {
        this.gameObject.SetActive(true);
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public PopupName GetPopupName() {return _popupName; }
    public void SetParamenter(object parament)
    {
        this.parament = parament;
    }
    #endregion


}

public interface IPopup
{
    public void OnShown();

    public void OnHide();
}