using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneLevel
    {
        public string sceneName;
        public int level;
    }

    [SerializeField] private SceneLevel[] sceneLevels;  // Array of scene names with corresponding levels

    public int GetLevelForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (var sceneLevel in sceneLevels)
        {
            if (sceneLevel.sceneName == currentSceneName)
            {
                return sceneLevel.level;
            }
        }

        return 0;  // Default level if no match found
    }
}
