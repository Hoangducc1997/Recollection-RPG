using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapBossAfterManager : MonoBehaviour
{
    public GameObject nextScene;
    public GameObject bossAppear;

    void Start()
    {
        nextScene.SetActive(false);
    }

    public void AppearObjNextScene()
    {
        if (nextScene != null)
        {
            nextScene.SetActive(true);
            Debug.Log("Next scene activated!");
        }
        else
        {
            Debug.LogWarning("NextScene is not assigned!");
        }

        if (bossAppear != null)
        {
            bossAppear.SetActive(false);
        }
    }

    public void AppearObjBoss()
    {
        if (bossAppear != null)
        {
            bossAppear.SetActive(true);
            Debug.Log("Boss appeared!");
        }
        else
        {
            Debug.LogWarning("BossAppear is not assigned!");
        }
    }

}
