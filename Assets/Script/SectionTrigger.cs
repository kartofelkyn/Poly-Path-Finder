// <copyright file="SectionTrigger.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This program is a procedural programming that generate track sections as the player moves forward.
/// It uses trigger method to detect when the player reacher a certain point and then spawn a next track section.
/// The next track section are predefined creating an endless illusion.
/// </summary>

public class SectionTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        // Spawn a randomized track when the trigger with tag "StartTrack" is reached
        // It added the position of the trigger + a new Vector of x, y, z to spawn the track in the right position
        // Else if the trigger with tag "SectionTrigger" is reached, it will spawn a new track section at the 
        // position of the trigger + a new Vector of x, y, z to spawn the track in the right position
        if(other.gameObject.CompareTag("StartTrack"))
        {
            Vector3 spawnPos = other.transform.position + new Vector3(1, -2, 30.11f);
            Instantiate(TrackManager.Instance.GetRandomTrackSection(), spawnPos, Quaternion.identity);
        }
        else if (other.gameObject.CompareTag("SectionTrigger"))
        {
            Debug.Log("Section Triggered other.GameObject: " + other.gameObject.name);
            Vector3 spawnPos = other.transform.position + new Vector3(0, 0, 111f);
            Instantiate(TrackManager.Instance.GetRandomTrackSection(), spawnPos, Quaternion.identity);
        }
    }
}
