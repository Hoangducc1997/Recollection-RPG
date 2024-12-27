using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private Button missionOpenButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject missionPanelBegin;
    private Animator animator;
    public virtual void Start()
    {
        // Lấy Animator từ con của missionPanel.
        animator = missionPanel.GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on a child of missionPanel!");
            return;
        }
        missionOpenButton.onClick.AddListener(OpenMissionPanel);
        closeButton.onClick.AddListener(CloseMissionPanel);
        missionPanel.SetActive(false);
        animator.SetBool("isOpen", false); // Bắt đầu với trạng thái đóng.
    }

    public void OpenMissionPanel()
    {
        missionPanel.SetActive(true);
        animator.SetBool("isOpen", true);
    }
    public void CloseMissionPanel()
    {
        StartCoroutine(DeactivatePanelAfterAnimation());
        missionPanelBegin.SetActive(false);
    }
    private IEnumerator DeactivatePanelAfterAnimation()
    {
        // Đặt trạng thái Animator để bắt đầu animation đóng
        animator.SetBool("isOpen", false);

        // Chờ thời gian hoàn thành của animation đóng
        yield return new WaitForSeconds(GetAnimationClipLength("BookClose"));

        // Sau khi animation đóng hoàn tất, tắt panel
        missionPanel.SetActive(false);
    }
    private float GetAnimationClipLength(string clipName)
    {
        // Tìm chiều dài của clip animation dựa trên tên clip.
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        Debug.LogWarning($"Animation clip '{clipName}' không tìm thấy!");
        return 0f;
    }
}
