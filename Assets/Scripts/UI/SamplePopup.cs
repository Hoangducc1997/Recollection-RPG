using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePopup : UIPopup
{
    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        if (parament != null) { 
        
        }
        // Do Logic Something
    }

    public override void OnHide()
    {
        base.OnHide();

    }
}
