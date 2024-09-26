using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [Header("Popup Name")]
    [SerializeField] PopupName _popupName;
    private object parament;

    #region Unity Function

    #endregion

    #region UI Function
    public virtual void OnShown(object parament = null)
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OnHide()
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