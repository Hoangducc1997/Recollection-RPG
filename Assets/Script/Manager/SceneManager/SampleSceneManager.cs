using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowPopup(PopupName.SamplePopup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
