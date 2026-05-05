using UnityEngine;

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
