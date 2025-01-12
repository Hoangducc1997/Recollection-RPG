using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private Button missionOpenButton;
    [SerializeField] private Button close1Button;
    [SerializeField] private Button close2Button;
    [SerializeField] private Button close3Button;
    [SerializeField] private GameObject missionPanelBegin;
    [SerializeField] private List<MissionPanelMapping> missionMappings; // Danh sách ánh xạ scene và panel
    private Dictionary<string, GameObject> missionPanelDictionary;
    private Animator animator;
    private GameObject currentMissionPanel;

    private void OnEnable()
    {
        // Đăng ký sự kiện khi chuyển scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị vô hiệu hóa
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected virtual void Start()
    {
        // Log tên scene hiện tại
        Debug.Log($"Current Scene: {SceneManager.GetActiveScene().name}");

        // Tạo dictionary từ danh sách ánh xạ
        missionPanelDictionary = new Dictionary<string, GameObject>();
        foreach (var mapping in missionMappings)
        {
            if (!missionPanelDictionary.ContainsKey(mapping.sceneName))
            {
                missionPanelDictionary.Add(mapping.sceneName, mapping.panel);
            }
        }

        InitializeMissionPanel();
        if (currentMissionPanel != null)
        {
            Debug.Log($"Active Mission Panel: {currentMissionPanel.name}");
        }
        else
        {
            Debug.LogError("No mission panel set for this scene.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on a child of missionPanel!");
            return;
        }

        missionOpenButton.onClick.AddListener(OpenMissionPanel);
        close1Button.onClick.AddListener(CloseMissionPanel);
        close2Button.onClick.AddListener(CloseMissionPanel);
        close3Button.onClick.AddListener(CloseMissionPanel);

        // Tắt tất cả các panel trước khi khởi tạo
        foreach (var mapping in missionMappings)
        {
            mapping.panel.SetActive(false);
        }

        if (currentMissionPanel != null)
        {
            currentMissionPanel.SetActive(false);
        }

        animator.SetBool("isOpen", false); // Bắt đầu với trạng thái đóng.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene changed to: {scene.name}");

        // Gọi lại hàm khởi tạo panel khi chuyển scene
        InitializeMissionPanel();

        if (currentMissionPanel != null)
        {
            Debug.Log($"Updated panel for scene: {scene.name} → {currentMissionPanel.name}");
        }
        else
        {
            Debug.LogWarning($"No mission panel assigned for the scene '{scene.name}'!");
        }
    }

    private void InitializeMissionPanel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (var mapping in missionMappings)
        {
            if (mapping.sceneName == currentSceneName)
            {
                currentMissionPanel = mapping.panel;
                currentMissionPanel.SetActive(true); // Chỉ bật panel của scene hiện tại
                animator = currentMissionPanel?.GetComponentInChildren<Animator>();
                if (animator == null)
                {
                    Debug.LogError("Animator component is missing!");
                }
            }
            else
            {
                mapping.panel.SetActive(false); // Tắt các panel không liên quan
            }
        }

        if (currentMissionPanel != null)
        {
            currentMissionPanel.SetActive(false); // Tắt panel sau khi gán để đảm bảo
            Debug.Log($"Initialized panel for scene: {currentSceneName} → {currentMissionPanel.name}");
        }
        else
        {
            Debug.LogWarning($"No mission panel assigned for the scene '{currentSceneName}'!");
        }
    }


    public void OpenMissionPanel()
    {
        if (currentMissionPanel != null)
        {
            currentMissionPanel.SetActive(true);
            animator.SetBool("isOpen", true);
        }
    }

    protected virtual void CloseMissionPanel()
    {
        if (currentMissionPanel != null)
        {
            StartCoroutine(DeactivatePanelAfterAnimation());
        }
        missionPanelBegin.SetActive(false);
    }

    private IEnumerator DeactivatePanelAfterAnimation()
    {
        if (animator != null)
        {
            // Đặt trạng thái Animator để bắt đầu animation đóng
            animator.SetBool("isOpen", false);

            // Chờ thời gian hoàn thành của animation đóng
            yield return new WaitForSeconds(GetAnimationClipLength("BookClose"));

            // Sau khi animation đóng hoàn tất, tắt panel
            currentMissionPanel.SetActive(false);
        }
    }

    private float GetAnimationClipLength(string clipName)
    {
        // Tìm chiều dài của clip animation dựa trên tên clip
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip.length;
                }
            }
        }
        Debug.LogWarning($"Animation clip '{clipName}' không tìm thấy!");
        return 0f;
    }
}

[System.Serializable]
public class MissionPanelMapping
{
    public string sceneName; // Tên scene
    public GameObject panel; // Panel tương ứng
}
