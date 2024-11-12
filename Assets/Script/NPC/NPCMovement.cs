using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed = 1f;

    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = pointA.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer từ đối tượng
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerNearby) // Chỉ di chuyển khi không có Player ở gần
        {
            NpcMovement();
        }
    }

    public void NpcMovement()
    {
        // Di chuyển NPC tới vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Kiểm tra nếu NPC đã đến gần vị trí mục tiêu
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Đổi mục tiêu nếu đến gần điểm A hoặc B
            targetPosition = targetPosition == pointA.transform.position ? pointB.transform.position : pointA.transform.position;

            // Flip Sprite dựa vào hướng di chuyển
            spriteRenderer.flipX = targetPosition == pointB.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu Player đến gần, NPC sẽ đứng im
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Nếu Player rời khỏi, NPC sẽ tiếp tục di chuyển
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
