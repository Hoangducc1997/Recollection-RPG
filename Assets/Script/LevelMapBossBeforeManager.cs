using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapBossBeforeManager : MonoBehaviour
{
    public GameObject nextScene;

    void Start()
    {
        nextScene.SetActive(false);
    }

    public void AppearObjNextScene()
    {
        nextScene.SetActive(true);
        Debug.Log("Next scene object activated!");
    }

}
