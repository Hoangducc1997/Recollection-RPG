using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.ShowPopup(PopupName.MainPlay);
    }
    public float moveSpeed = 5f;

    public AnimationClip[] animations; // Array to hold different animations
}
