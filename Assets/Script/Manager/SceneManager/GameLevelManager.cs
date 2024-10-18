using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowPopup(PopupName.IntroGame);
    }

}
