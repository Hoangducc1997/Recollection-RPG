using UnityEngine;
using System.Collections;

public class GamePlayPopup : UIPopup
{
    public GameObject findBossText;
    public float displayDuration = 3f; // Thời gian hiển thị (đơn vị: giây)

    void Start()
    {
        findBossText.SetActive(false); // Ẩn findBossText khi bắt đầu
    }

    public void ShowFindBossText()
    {
        findBossText.SetActive(true); // Hiển thị findBossText
        StartCoroutine(HideFindBossTextAfterDelay());
    }

    private IEnumerator HideFindBossTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // Đợi một khoảng thời gian
        findBossText.SetActive(false); // Ẩn findBossText
    }
}
