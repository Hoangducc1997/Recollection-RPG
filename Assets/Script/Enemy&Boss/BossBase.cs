using UnityEngine;

public class BossBase : MonoBehaviour
{
    protected UnityEngine.GameObject player;
    protected PlayerHealthManager playerBarManager;

    [SerializeField] protected float damageBossAttack;      // Sát thương của boss
    [SerializeField] protected float attackCooldown = 1f; // Thời gian chờ giữa các lần tấn công

    protected virtual void Start()
    {
        player = UnityEngine.GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerBarManager = player.GetComponent<PlayerHealthManager>();
            if (playerBarManager == null)
            {
                Debug.LogWarning("PlayerMovement does not have a PlayerBarManager component.");
            }
        }
    }
}
