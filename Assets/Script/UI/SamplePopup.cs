using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePopup : UIPopup
{
    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        if (_popupParament != null) { 
        
        }
        // Do Logic Something
    }

    public override void OnHide()
    {
        base.OnHide();

    }
}
