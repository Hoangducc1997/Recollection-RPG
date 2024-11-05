using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject nextScene;
    public GameObject bossAppear;

    void Start()
    {
        nextScene.SetActive(false);
    }

    public void AppearObjNextScene()
    {
        nextScene.SetActive(true);
        bossAppear.SetActive(false);
    }

    public void AppearObjBoss()
    {
        bossAppear.SetActive(true);
    }
}
