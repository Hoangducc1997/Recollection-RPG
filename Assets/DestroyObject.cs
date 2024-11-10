using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Animator animator;  // Animator để theo dõi trạng thái animation

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator từ đối tượng
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu animation kết thúc (tức là thời gian trong animation đã xong)
        if (animator != null)
        {
            // Lấy trạng thái animation hiện tại
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Kiểm tra nếu animation đã hoàn thành (chúng ta sử dụng normalizedTime, giá trị 1 có nghĩa là kết thúc)
            if (stateInfo.normalizedTime >= 1f && !animator.IsInTransition(0))
            {
                Destroy(gameObject); // Hủy đối tượng
            }
        }
    }
}
