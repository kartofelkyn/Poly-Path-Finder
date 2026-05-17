// <copyright file="TrackMovement.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This script moves the track pieces (toward the player) creating an illusion of forward movement.
/// It also destroy game object when it reach a certain point (trigger) to prevent memory overflow and keep the game running smoothly.s
/// </summary>

public class TrackMovement : MonoBehaviour
{
    // Update is called once per frame
    // It checks if the intro is playing, if not it will return and do nothing
    // If the intro is not playing, it will move the track pieces toward the player 
    // by changing the position of the track pieces in the z-axis (forward direction) 
    // by a certain speed (currentSpeed) multiplied by Time.deltaTime to make it frame rate independent.
    void Update()
    {
        if (GameManager.Instance.state != GameState.Playing) return;
        float speedControl = GameManager.Instance.currentSpeed;
        transform.position -= new Vector3(0, 0, (Time.deltaTime * speedControl));
    }
    
    // It checks if the track pieces collide with a trigger with tag "Destroy", 
    // if it does, it will destroy the track pieces game object to prevent memory 
    // overflow and keep the game running smoothly.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
