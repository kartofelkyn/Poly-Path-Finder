// <copyright file="PlayerMovement.cs" company="Pamantasan ng Lungsod ng Maynila BSCS 4-1 2026">
// Copyright (c) Pamantasan ng Lungsod ng Maynila BSCS 4-1 2026. All rights reserved.
// </copyright>
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// This program is responsible for player movement
    /// </summary>
    [SerializeField] float moveSpeed = 4f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
    }
}
