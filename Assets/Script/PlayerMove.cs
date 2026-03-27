// <copyright file="PlayerMove.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This script handles the player's movement between the three lanes.
/// It uses two invisible buttons (left and right) to trigger the movement, and the player will move smoothly to the next lane when a button is pressed.
/// </summary>

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float xPos;
    [SerializeField] int trackNumber = 1;
    [SerializeField] int sideSpeed = 3;
    [SerializeField] bool currentMove;
    [SerializeField] int moveDirection; //1=left. 2=right
    void Update()
    {
        xPos = gameObject.transform.position.x;
        if (currentMove == true && moveDirection == 1)
        {
            transform.Translate(Vector3.left * Time.deltaTime * sideSpeed, Space.World);
            if (xPos <= trackNumber)
            {
                currentMove = false;
                moveDirection = 0;
                transform.position = new Vector3(trackNumber, 1, -5.48f);
            }
        }
        if (currentMove == true && moveDirection == 2)
        {
            transform.Translate(Vector3.right * Time.deltaTime * sideSpeed, Space.World);
            if (xPos >= trackNumber)
            {
                currentMove = false;
                moveDirection = 0;
                transform.position = new Vector3(trackNumber, 1, -5.48f);
            }
        }
    }
    public void MoveLeft()
    {
        if (trackNumber == 1)
        {
            currentMove = true;
            moveDirection = 1;
            trackNumber = 0;
        }
        else if (trackNumber == 2)
        {
            currentMove = true;
            moveDirection = 1;
            trackNumber = 1;
        }
    }
    public void MoveRight()
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
