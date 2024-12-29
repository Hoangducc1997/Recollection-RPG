using System.Collections;
using UnityEngine;

public class MissionOvercomeMap : MissionManager
{
    public static MissionOvercomeMap Instance { get; private set; }

    [SerializeField] private GameObject missionComplete1;
    [SerializeField] private GameObject missionComplete2;
    [SerializeField] private GameObject missionComplete3;
    [SerializeField] private GameObject missionComplete4;
    [SerializeField] private GameObject nextMap;
    private Animator _animator;
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
        _animator = GetComponent<Animator>();
        base.Start();
        missionComplete1.SetActive(false);
        missionComplete2.SetActive(false);
        missionComplete3.SetActive(false);
        missionComplete4.SetActive(false);
        nextMap.SetActive(false);
        _animator.SetBool("isBookNotification", false);
    }
    protected override void CloseMissionPanel()
    {
        // Gọi base nếu cần giữ lại hành vi gốc
        base.CloseMissionPanel();

        // Tắt notification animation nếu cần
        if (_animator != null)
        {
            _animator.SetBool("isBookNotification", false);
        }
    }

    public void ShowMissionComplete1()
    {
        if (missionComplete1 != null)
        {
            missionComplete1.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }

    public void ShowMissionComplete2()
    {
        if (missionComplete2 != null)
        {
            missionComplete2.SetActive(true);
            StartCoroutine(ShowNextMapTemporarily());
            _animator.SetBool("isBookNotification", true);
        }
    }

    public void ShowMissionComplete3()
    {
        if (missionComplete3 != null)
        {
            missionComplete3.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }
    public void ShowMissionComplete4()
    {
        if (missionComplete4 != null)
        {
            missionComplete4.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }

    private IEnumerator ShowNextMapTemporarily()
    {
        // Hiển thị nextMap
        nextMap.SetActive(true);

        // Chờ 5 giây
        yield return new WaitForSeconds(5f);

        // Tắt nextMap
        nextMap.SetActive(false);
    }
}
