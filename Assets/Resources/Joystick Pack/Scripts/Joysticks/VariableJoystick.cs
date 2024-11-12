using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
    private Animator joystickAnimator;
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;

    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if (joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
        {
            background.gameObject.SetActive(true);
        }
    }

    protected override void Start()
    {
        base.Start();
        joystickAnimator = background.GetComponentInParent<Animator>();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);
    }

    void Update()
    {
        UpdateAnimationBasedOnDirection();
    }

    private void UpdateAnimationBasedOnDirection()
    {
        Vector2 direction = Direction;

        // Reset all animation bools to ensure only one direction is active at a time
        joystickAnimator.SetBool("JoystickUp", direction.y > 0.5f);
        joystickAnimator.SetBool("JoystickDown", direction.y < -0.5f);
        joystickAnimator.SetBool("JoystickRight", direction.x > 0.5f);
        joystickAnimator.SetBool("JoystickLeft", direction.x < -0.5f);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        }
        base.OnPointerDown(eventData);
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
        }

        // Reset tất cả các bool khi joystick được thả
        joystickAnimator.SetBool("JoystickUp", false);
        joystickAnimator.SetBool("JoystickDown", false);
        joystickAnimator.SetBool("JoystickLeft", false);
        joystickAnimator.SetBool("JoystickRight", false);

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
