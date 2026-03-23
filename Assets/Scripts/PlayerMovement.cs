// <copyright file="PlayerMovement.cs" company="Pamantasan ng Lungsod ng Maynila BSCS 4-1 2026">
// Copyright (c) Pamantasan ng Lungsod ng Maynila BSCS 4-1 2026. All rights reserved.
// </copyright>

using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// This program is responsible for player movement
    /// </summary>

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float xPos;
    [SerializeField] float zPos;
    [SerializeField] int trackNumber = 1;
    [SerializeField] int sideSpeed = 9;
    [SerializeField] bool currentMove;
    [SerializeField] int moveDirection; //1=left, 2=right
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        xPos = gameObject.transform.position.x;
        zPos = gameObject.transform.position.z;
        if (currentMove == true && moveDirection == 1) // moving left
        {
            transform.Translate(Vector3.left * Time.deltaTime * sideSpeed, Space.World);
            if (xPos <= trackNumber)
            {
                currentMove = false;
                moveDirection = 0;
                transform.position = new Vector3(trackNumber, 1, zPos);
            }
        }
        if (currentMove == true && moveDirection == 2) //moving right
        {
            transform.Translate(Vector3.right * Time.deltaTime * sideSpeed, Space.World);
            if (xPos >= trackNumber)
            {
                currentMove = false;
                moveDirection = 0;
                transform.position = new Vector3(trackNumber, 1, zPos);
            }
        }
    }

    public void LeftMove()
    {
        if (trackNumber == 1)
        {
            currentMove = true;
            moveDirection = 1;
            trackNumber = 0;
        }else if (trackNumber == 2)
        {
            currentMove = true;
            moveDirection = 1;
            trackNumber = 1;
        }
    }

    public void RightMove()
    {
        if (trackNumber == 0)
        {
            currentMove = true;
            moveDirection = 2;
            trackNumber = 1;
        }
        else if (trackNumber == 1)
        {
            currentMove = true;
            moveDirection = 2;
            trackNumber = 2;
        } 
    }
}
