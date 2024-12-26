using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private Button missionOpenButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject missionPanel;
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
        missionPanel.SetActive(false);
    }
    private IEnumerator DeactivatePanelAfterAnimation()
    {
        // Chờ cho đến khi animation đóng hoàn thành.
        yield return new WaitForSeconds(GetAnimationClipLength("CloseAnimation"));
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
