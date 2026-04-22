// <copyright file="ChoiceCollision.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This script is responsible for detecting when the player collides with a lane (which represents a quiz choice) and 
/// notifying the RenderQuizChoices script to check if the answer is correct or not.
/// </summary>

public class ChoiceCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<RenderQuizChoices>().OnLaneHit();
        }
    }
}
