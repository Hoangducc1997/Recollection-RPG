using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VariableJoystick : Joystick
{
    [SerializeField] private Animator joystickAnimator;
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;

    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if (joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition; // Vị trí cố định
            background.gameObject.SetActive(true); // Luôn hiện joystick khi ở chế độ Fixed
        }
        else
        {
            background.gameObject.SetActive(true); // Luôn hiện joystick ở các chế độ khác
        }
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition; // Lưu lại vị trí ban đầu
        SetMode(joystickType);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // Joystick chỉ di chuyển khi nhấn vào màn hình
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position); // Cập nhật vị trí khi người chơi chạm vào
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Đưa joystick về vị trí ban đầu khi thả tay
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition; // Trả về vị trí ban đầu
        }
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}

public enum JoystickType { Fixed, Floating, Dynamic }
