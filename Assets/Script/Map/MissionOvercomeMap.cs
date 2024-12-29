using UnityEngine;

public class MissionOvercomeMap : MissionManager
{
    public static MissionOvercomeMap Instance { get; private set; }

    [SerializeField] private GameObject missionComplete1;
    [SerializeField] private GameObject missionComplete2;
    [SerializeField] private GameObject nextMap;
    private Animator animator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ object này không bị xóa khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một instance
        }
    }

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        missionComplete1.SetActive(false);
        missionComplete2.SetActive(false);
        nextMap.SetActive(false);
        animator.SetBool("isBookNotification", false);
    }

    public void ShowMissionComplete1()
    {
        if (missionComplete1 != null)
        {
            missionComplete1.SetActive(true);
            animator.SetBool("isBookNotification", true);
        }
    }

    public void ShowMissionComplete2()
    {
        if (missionComplete2 != null)
        {
            missionComplete2.SetActive(true);
            nextMap.SetActive(true);
            animator.SetBool("isBookNotification", true);
        }
    }
}
