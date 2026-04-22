using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour
{
    public PlayerMove player;

    private Vector2 startTouch;
    private Vector2 endTouch;

    private float minSwipeDistance = 50f;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        HandleTouch();
        HandleMouse();
    }

    void HandleTouch()
    {
        if (Touch.activeTouches.Count == 0) return;

        var touch = Touch.activeTouches[0];

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startTouch = touch.screenPosition;
        }

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            endTouch = touch.screenPosition;
            DetectSwipe();
        }
    }

    void HandleMouse()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startTouch = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            endTouch = Mouse.current.position.ReadValue();
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endTouch - startTouch;

        if (delta.magnitude < minSwipeDistance)
        {
            HandleTap();
            return;
        }

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0) player.QueueRight();
            else player.QueueLeft();
        }
        else
        {
            if (delta.y > 0) player.QueueJump();
        }
    }

    void HandleTap()
    {
        if (endTouch.x < Screen.width / 2)
            player.QueueLeft();
        else
            player.QueueRight();
    }
}