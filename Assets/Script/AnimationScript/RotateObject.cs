using UnityEngine;

/// <summary>
/// Continuously rotates the object around its Y-axis at a defined speed.
/// </summary>
public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}