using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Player")]
public class PlayerMovementConfig : ScriptableObject
{
    public new string name;

    public string description;
    

    public float moveSpeed = 5f;

    public AnimationClip[] animations; // Array to hold different animations

    
}
