using UnityEngine;

/// <summary>
/// This script controls the animation of the cop chasing the player in the intro sequence.
/// The cop moves forward until it reaches a certain point, at which it stops and bounces up and down. 
/// The bounce effect is desynchronized between multiple cop instances by using a random time offset, 
/// creating a more natural and dynamic animation. Once the cop reaches the stop point, it will be destroyed 
/// after a short delay, allowing the intro sequence to transition smoothly into the gameplay. This script adds an 
/// engaging visual element to the intro, setting the tone for the game and creating anticipation for the player as 
/// they start their journey.  
/// </summary>
public class CopChaseAnimation : MonoBehaviour
{
    [Header("Follow")]
    public float runSpeed = 4.5f;
    public float stopZ = -6.48f;

    [Header("Bounce")]
    public float bounceHeight = 0.2f;
    public float bounceSpeed = 7f;

    private float baseY;
    private float timeOffset;
    bool hasStopped = false;

    void Start()
    {
        baseY = transform.position.y;

        // THIS is what desyncs them
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Move forward until stop point
        if (transform.position.z < stopZ)
        {
            transform.position += new Vector3(0, 0, runSpeed * Time.deltaTime);
        }
        else if (!hasStopped)
        {
            hasStopped = true;

            // destroy after delay
            Destroy(gameObject, 2f);
        }

        // Bounce (with offset so not synchronized)
        float bounce = Mathf.Abs(
            Mathf.Sin((Time.time + timeOffset) * bounceSpeed)
        ) * bounceHeight;

        transform.position = new Vector3(
            transform.position.x,
            baseY + bounce,
            transform.position.z
        );
    }
}
