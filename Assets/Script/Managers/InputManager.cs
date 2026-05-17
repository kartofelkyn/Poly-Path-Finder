using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

/// <summary>
/// Manages input handling for the game, including touch and mouse input for detecting swipes and taps. 
/// </summary>
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

    // Handles touch input for mobile devices
    void HandleTouch()
    {
        if (Touch.activeTouches.Count == 0) return;

        var touch = Touch.activeTouches[0];

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began) // When a touch begins, record the starting position of the touch
        {
            startTouch = touch.screenPosition; // Record the starting position of the touch when it begins
        }

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended) // When the touch ends, record the ending position and detect the swipe direction or tap
        {
            endTouch = touch.screenPosition; // Record the ending position of the touch when it ends
            DetectSwipe(); // After recording the end position, call the DetectSwipe method to determine if it was a swipe or a tap and handle the input accordingly
        }
    }

    // Handles mouse input for testing in the editor
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

    // Detects swipe direction and handles taps based on the start and end touch positions
    void DetectSwipe()
    {
        Vector2 delta = endTouch - startTouch;
        
        // Check if the swipe distance is less than the minimum threshold to consider it a tap. 
        // Example: If the player taps on the left half of the screen, it queues a left move; 
        // if they tap on the right half, it queues a right move.
        if (delta.magnitude < minSwipeDistance)
        {
            HandleTap();
            return;
        }

        /// If the swipe distance exceeds the minimum threshold, it determines the direction of the swipe. 
        /// If the horizontal component of the swipe is greater than the vertical component, it queues a 
        /// left or right move based on the direction of the swipe. If the vertical component is greater, it queues a jump.
        /// Example: A swipe to the right queues a right move, a swipe to the left queues a left move, and a swipe upwards queues a jump.
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0) player.QueueRight(); // delta.x > 0 indicates a swipe to the right, since the horizontal component is greater than the vertical component, it queues a right move for the player
            else player.QueueLeft();
        }
        else
        {
            if (delta.y > 0) player.QueueJump(); // delta.y > 0 indicates a swipe upwards, since the vertical component is greater than the horizontal component, it queues a jump for the player
        }
    }

    // Handles tap input by determining if the tap was on the left or right half of the screen and queues the appropriate move.
    void HandleTap()
    {
        if (endTouch.x < Screen.width / 2) // If the tap was on the left half of the screen, it queues a left move for the player
            player.QueueLeft();
        else
            player.QueueRight();
    }
}