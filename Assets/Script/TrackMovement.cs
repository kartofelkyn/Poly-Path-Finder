// <copyright file="TrackMovement.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This script moves the track pieces (toward the player) creating an illusion of forward movement.
/// It also destroy game object when it reach a certain point (trigger) to prevent memory overflow and keep the game running smoothly.
/// </summary>

public class TrackMovement : MonoBehaviour
{

    void Update()
    {
        float speedControl = GameManager.Instance.currentSpeed;
        transform.position -= new Vector3(0, 0, (Time.deltaTime * speedControl));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
