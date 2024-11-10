using UnityEngine;

public class BossBase : MonoBehaviour
{
    protected GameObject player;
    protected PlayerBarManager playerBarManager;

    [SerializeField] protected int damageBossAttack;      // Sát thương của boss
    [SerializeField] protected float attackCooldown = 1f; // Thời gian chờ giữa các lần tấn công

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
