using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLevel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GamePlay"))
        {
            NextScene();
        }
    }
    private void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
