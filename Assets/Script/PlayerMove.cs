using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This script manages the player's movement in the game, including lane switching, 
/// jumping, and running bounce effects.
/// It uses a combination of smooth damping for lane movement, sine wave calculations 
/// for bounce effects, and a simple state machine for handling jump and lane change states. 
/// The script also includes an input buffering system for left and right movements, allowing 
/// for responsive controls even when the player is in the middle of a lane change. 
/// Additionally, it handles camera movement during the intro sequence and applies tilt effects 
/// when changing lanes for added visual feedback. By centralizing all player movement logic in 
/// this script, it simplifies the management of player actions and ensures a cohesive movement 
/// experience throughout the game.
/// </summary>

public class PlayerMove : MonoBehaviour
{
    [Header("Lane Settings")]
    [SerializeField] float laneY = 1f;
    [SerializeField] float laneZ = -5.48f;
    [SerializeField] float laneOffset = 1f;
    [SerializeField] int trackNumber = 1;

    [Header("Movement")]
    [SerializeField] float moveSmoothTime = 0.08f;

    [Header("Running Bounce")]
    [SerializeField] float runSpeed = 5f;

    [SerializeField] float runBounceHeight = 0.2f;
    [SerializeField] float runBounceSpeed = 7f;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float jumpDuration = 0.6f;

    [Header("Lane Bounce")]
    [SerializeField] float laneJumpHeight = 0.2f;
    [SerializeField] float laneJumpDuration = 0.2f;

    [Header("Tilt")]
    [SerializeField] float tiltAmount = 15f;
    [SerializeField] float tiltSpeed = 10f;
    [SerializeField] float forwardTilt = 10f;

    [Header("Camera")]
    [SerializeField] Transform mainCamera;
    [SerializeField] float introCameraSpeedY = 1f;
    [SerializeField] float introCameraSpeedZ = 1f;

    private Vector3 targetPosition;
    private Vector3 velocity;
    private float baseY;
    private float timeCounter;

    // Jump
    private bool isGrounded = true;
    private bool isAirJumping = false;
    private float airJumpTimer = 0f;

    // Lane bounce
    private bool isLaneJumping = false;
    private float laneJumpTimer = 0f;

    // Tilt
    private float currentTilt = 0f;
    private float targetTilt = 0f;

    // Input buffer (ONLY for left/right)
    private Queue<System.Action> inputQueue = new Queue<System.Action>();
    private bool isChangingLane = false;


    void Start()
    {
        Debug.Log("Player move starty");
        baseY = laneY;
        targetPosition = new Vector3(trackNumber * laneOffset, laneY, laneZ);
    }

    void Update()
    {
        if (GameManager.Instance.state == GameState.Intro)
        {
            IntroMovement();
            return;
        }

        if (GameManager.Instance.state != GameState.Playing)
        {
            return;
        }

        timeCounter += Time.deltaTime;

        HandleBufferedInput();
        MovePlayer();
        HandleJumpAndBounce();
        HandleTilt();
    }

    /// <summary>
    /// Handles the player's movement during the intro sequence, where the player runs forward with a bounce effect while the 
    /// camera moves upwards and backwards. Once the player reaches a certain position, the game state changes to Playing and 
    /// the camera is set to its final position and rotation for gameplay. This creates an engaging introduction to the game, 
    /// setting the tone and atmosphere before the player starts controlling their character.
    /// </summary>
    void IntroMovement()
    {
        timeCounter += Time.deltaTime;

        float runBounce = Mathf.Abs(Mathf.Sin(timeCounter * runBounceSpeed)) * runBounceHeight;

        transform.position += new Vector3(0, 0, runSpeed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x,
            1f + runBounce,
            transform.position.z
        );
        mainCamera.position += new Vector3(0, runSpeed * Time.deltaTime * introCameraSpeedY, Time.deltaTime * introCameraSpeedZ);
        if (transform.position.z >= -5.48f) // Once the player reaches the target Z position, switch to Playing state and set the camera to its final position and rotation for gameplay
        {
            GameManager.Instance.state = GameState.Playing;
            mainCamera.position = new Vector3(1f, 3.4f, -9.43f);
            mainCamera.rotation = Quaternion.Euler(25.51f, 0f, 0f);
        }
    }

    // INPUT BUFFER
    void HandleBufferedInput()
    {
        if (inputQueue.Count == 0) return;
        if (isChangingLane) return;

        var action = inputQueue.Dequeue();
        action.Invoke();
    }

    public void QueueLeft()
    {
        if (GameManager.Instance.state != GameState.Playing) return;
        inputQueue.Enqueue(MoveLeft);
    }

    public void QueueRight()
    {
        if (GameManager.Instance.state != GameState.Playing) return;
        inputQueue.Enqueue(MoveRight);
    }

    // Jump is NOT buffered
    public void QueueJump()
    {
        if (GameManager.Instance.state != GameState.Playing) return;

        if (!isGrounded) return;
        Jump();
    }

    // MOVEMENT
    void MovePlayer()
    {
        Vector3 pos = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            moveSmoothTime
        );

        transform.position = new Vector3(pos.x, transform.position.y, pos.z);

        if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.01f)
        {
            isChangingLane = false;
        }
    }

    void MoveLeft()
    {
        if (trackNumber <= 0) return;

        trackNumber--;
        targetTilt = tiltAmount;
        UpdateTarget();
    }

    void MoveRight()
    {
        if (trackNumber >= 2) return;

        trackNumber++;
        targetTilt = -tiltAmount;
        UpdateTarget();
    }

    void UpdateTarget()
    {
        targetPosition = new Vector3(trackNumber * laneOffset, laneY, laneZ);

        isChangingLane = true;

        // Lane bounce
        isLaneJumping = true;
        laneJumpTimer = 0f;

        Invoke(nameof(ResetTilt), 0.2f);
    }

    // JUMP + BOUNCE SYSTEM
    void HandleJumpAndBounce()
    {
        float yOffset = 0f;

        // RUNNING BOUNCE
        float runBounce = Mathf.Abs(Mathf.Sin(timeCounter * runBounceSpeed)) * runBounceHeight;
        yOffset += runBounce;

        // REAL JUMP
        if (isAirJumping)
        {

            airJumpTimer += Time.deltaTime;
            float progress = airJumpTimer / jumpDuration;

            if (progress >= 1f)
            {
                progress = 1f;
                isAirJumping = false;
                isGrounded = true;
            }

            yOffset += Mathf.Sin(progress * Mathf.PI) * jumpHeight;
        }

        // LANE BOUNCE
        if (isLaneJumping)
        {
            laneJumpTimer += Time.deltaTime;
            float progress = laneJumpTimer / laneJumpDuration;

            if (progress >= 1f)
            {
                progress = 1f;
                isLaneJumping = false;
            }

            yOffset += Mathf.Sin(progress * Mathf.PI) * laneJumpHeight;
        }

        transform.position = new Vector3(
            transform.position.x,
            baseY + yOffset,
            transform.position.z
        );
    }

    public void Jump()
    {
        if (!isGrounded) return;

        isAirJumping = true;
        isGrounded = false;
        airJumpTimer = 0f;

        GamePlaySound.instance.Jump();
    }

    // TILT
    void HandleTilt()
    {
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
        transform.rotation = Quaternion.Euler(forwardTilt, 0, currentTilt);
    }

    void ResetTilt()
    {
        targetTilt = 0f;
    }
}