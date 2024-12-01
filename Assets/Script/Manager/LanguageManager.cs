using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] public Button buttonVie;
    [SerializeField] public Button buttonEng;

    private void Awake()
    {
        LocalizationManager.Read();
    }

    private void Start()
    {
        buttonVie.onClick.AddListener(ChangeToVie);
        buttonEng.onClick.AddListener(ChangeToEng);
    }

    public void ChangeToVie()
    {
        LocalizationManager.Language = "Vietnamese";
    }

    public void ChangeToEng()
    {
        LocalizationManager.Language = "English";
    }
}
