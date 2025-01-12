using System.Collections;
using UnityEngine;

public class MissionOvercomeMap : MissionManager
{
    public static MissionOvercomeMap Instance { get; private set; }

    [SerializeField] private GameObject missionComplete1; //Map OverCome1
    [SerializeField] private GameObject missionComplete2; //Map OverCome1
    [SerializeField] private GameObject missionComplete3; //Map Boss1
    [SerializeField] private GameObject missionComplete4; //Map Boss1
    [SerializeField] private GameObject missionComplete5; //Map OverCome2
    [SerializeField] private GameObject missionComplete6; //Map OverCome2
    [SerializeField] private GameObject missionComplete7; //Map OverCome2
    [SerializeField] private GameObject missionComplete8; //Map Boss2
    [SerializeField] private GameObject missionComplete9; //Map Boss2
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
        missionComplete5.SetActive(false);
        missionComplete6.SetActive(false);
        missionComplete7.SetActive(false);
        missionComplete8.SetActive(false);
        missionComplete9.SetActive(false);
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

    public void ShowMissionComplete5()
    {
        if (missionComplete5 != null)
        {
            missionComplete5.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }
    public void ShowMissionComplete6()
    {
        if (missionComplete6 != null)
        {
            missionComplete6.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }

    public void ShowMissionComplete7()
    {
        if (missionComplete7 != null)
        {
            missionComplete7.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }
    public void ShowMissionComplete8()
    {
        if (missionComplete8 != null)
        {
            missionComplete8.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }

    public void ShowMissionComplete9()
    {
        if (missionComplete9 != null)
        {
            missionComplete9.SetActive(true);
            _animator.SetBool("isBookNotification", true);
        }
    }
    private IEnumerator ShowNextMapTemporarily()
    {
        // Hiển thị nextMap
        nextMap.SetActive(true);

        // Chờ 3 giây
        yield return new WaitForSeconds(3f);

        // Tắt nextMap
        nextMap.SetActive(false);
    }
}
