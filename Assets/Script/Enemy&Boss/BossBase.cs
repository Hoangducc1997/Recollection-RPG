using UnityEngine;

public class BossBase : MonoBehaviour
{
    protected GameObject player;
    protected PlayerBarManager playerBarManager;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerBarManager = player.GetComponent<PlayerBarManager>();
            if (playerBarManager == null)
            {
                Debug.LogWarning("Player does not have a PlayerBarManager component.");
            }
        }
    }
}
