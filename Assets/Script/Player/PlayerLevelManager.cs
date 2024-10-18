using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string nextLevelTag;  // The tag of the object
        public int level;  // The level associated with this tag
    }

    [SerializeField] private Level[] nextLevels;  // List of levels with corresponding tags

    // Method to get level based on the tag
    public int GetLevelForTag(string tag)
    {
        foreach (var nextLevel in nextLevels)
        {
            if (nextLevel.nextLevelTag == tag)
            {
                return nextLevel.level;
            }
        }
        return 0;  // Return 0 if no match found
    }
}
