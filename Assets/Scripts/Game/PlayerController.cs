using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TimeVariables
    float previousTime;
    float fallTime = 1f;

    //Rotation
    Vector3 rotationPoint = Vector3.zero;

    //Others
    MyGameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<MyGameManager>();
    }

    void Update()
    {
        if (!gameManager.IsPauseGame())
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1f, 0f, 0f);
                CheckLeftMove();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1f, 0f, 0f);
                CheckRightMove();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0f, 0f, 1f), 90);
                CheckRotation();
            }

            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0f, -1f, 0f);
                CheckBottomMove();
                previousTime = Time.time;
            }
        }
    }

    //----------Collision----------
    bool IsValidMove()
    {
        foreach(Transform puyo in transform)
        {
            int x = Mathf.RoundToInt(puyo.transform.position.x);
            int y = Mathf.RoundToInt(puyo.transform.position.y);
            if(x < 0 || x >= gameManager.GetWidth()|| y < 0)
            {
                return false;
            }

            if (gameManager.CheckPlaceInGrid(x, y))
            {
                return false;
            }
        }
        return true;
    }
    void StartPuyosFall()
    {
        foreach(Transform puyo in transform)
        {
            puyo.GetComponent<Puyo>().FallFromPiece();
        }
    }
    void CheckLeftMove()
    {
        if (!IsValidMove())
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
    }
    void CheckRightMove()
    {
        if (!IsValidMove())
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
    }
    void CheckBottomMove()
    {
        if (!IsValidMove())
        {
            transform.position += new Vector3(0f, 1f, 0f);
            StartPuyosFall();
            gameManager.SpawnNewPuyo();
            enabled = false;
        }
    }
    void CheckRotation()
    {
        if (!IsValidMove())
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0f, 0f, 1f), -90);
        }
    }
}
