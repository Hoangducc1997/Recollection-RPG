using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private Button touchNextScene;
    [SerializeField] private string sceneName;
    private void Start()
    {
        touchNextScene.onClick.AddListener(NextScene);
    }

    private void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
