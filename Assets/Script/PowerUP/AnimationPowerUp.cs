using UnityEngine;

public class AnimationPowerUp : MonoBehaviour
{
    private float rotateSpeed = 100f;

    private float bounceHeight = 0.2f;
    private float bounceSpeed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(
            0,
            rotateSpeed * Time.deltaTime,
            0
        );

        float newY =
            startPosition.y +
            Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}
