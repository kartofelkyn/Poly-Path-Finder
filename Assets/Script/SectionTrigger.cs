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
        if(other.gameObject.CompareTag("StartTrack"))
        {
            Vector3 spawnPos = other.transform.position + new Vector3(1, -2, 30f);
            Instantiate(TrackManager.Instance.GetRandomTrackSection(), spawnPos, Quaternion.identity);
        }
        else if (other.gameObject.CompareTag("Trigger"))
        {
            Vector3 spawnPos = other.transform.position + new Vector3(0, 0, 111f);
            Instantiate(TrackManager.Instance.GetRandomTrackSection(), spawnPos, Quaternion.identity);
        }
    }
}
