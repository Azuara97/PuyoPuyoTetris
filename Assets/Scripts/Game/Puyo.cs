using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    //PuyoVariables
    int color = -1;
    bool searched = false;
    bool leftMatch = false;
    bool rightMatch = false;
    bool upMatch = false;
    bool downMatch = false;

    //Other
    MyGameManager gameManager;
    enum colors { red, green, blue, yellow, purple }

    void Start()
    {
        SetColor();
        gameManager = FindObjectOfType<MyGameManager>();
    }

    void Update()
    {
        
    }

    //----------StartFunctions----------
    void SetColor()
    {
        color = Random.Range(0, 5);
        switch (color)
        {
            case (int)colors.red:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case (int)colors.green:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case (int)colors.blue:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case (int)colors.yellow:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case (int)colors.purple:
                GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }
    }

    //----------Fall----------
    bool CheckPlaceBelow()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        if (y != 0 && !gameManager.CheckPlaceInGrid(x, y - 1))
        {
            return true;
        }
        else
        {
            gameManager.AddPuyoToGrid(transform);
            return false;
        }
    }
    void CheckGameOver()
    {
        int y = Mathf.RoundToInt(transform.position.y);
        if (y > gameManager.GetHeight())
        {
            gameManager.SetPauseGame(true);
            MenusManager menusManager = FindObjectOfType<MenusManager>();
            menusManager.gameOver();
        }
    }
    public void FallFromPiece()
    {
        while (CheckPlaceBelow())
        {
            transform.position += new Vector3(0f, -1f, 0f);
        }
        CheckInFallPiece();
        CheckGameOver();
    }

    //----------Count----------
    void ResetNeighbours()
    {
        leftMatch = false;
        rightMatch = false;
        upMatch = false;
        downMatch = false;
    }
    void SearchInX(int x, int y)
    {
        if(x == 0)
        {
            if (gameManager.CheckPlaceInGrid(x + 1, y))
            {
                var puyoRight = gameManager.GetPuyo(x + 1, y);
                if (puyoRight.GetColor() == color && !puyoRight.GetSearched())
                {
                    searched = true;
                    rightMatch = true;
                    puyoRight.CountPuyo();
                    puyoRight.SearchNeighbours();
                }
            }
        }
        else if (x == gameManager.GetWidth() - 1)
        {
            if (gameManager.CheckPlaceInGrid(x - 1, y))
            {
                var puyoLeft = gameManager.GetPuyo(x - 1, y);
                if (puyoLeft.GetColor() == color && !puyoLeft.GetSearched())
                {
                    searched = true;
                    leftMatch = true;
                    puyoLeft.CountPuyo();
                    puyoLeft.SearchNeighbours();
                }
            }
        }
        else
        {
            if (gameManager.CheckPlaceInGrid(x + 1, y))
            {
                var puyoRight = gameManager.GetPuyo(x + 1, y);
                if (puyoRight.GetColor() == color && !puyoRight.GetSearched())
                {
                    searched = true;
                    rightMatch = true;
                    puyoRight.CountPuyo();
                    puyoRight.SearchNeighbours();
                }
            }

            if (gameManager.CheckPlaceInGrid(x - 1, y))
            {
                var puyoLeft = gameManager.GetPuyo(x - 1, y);
                if (puyoLeft.GetColor() == color && !puyoLeft.GetSearched())
                {
                    searched = true;
                    leftMatch = true;
                    puyoLeft.CountPuyo();
                    puyoLeft.SearchNeighbours();
                }
            }
        }
    }
    void SearchInY(int x, int y)
    {
        if (y == 0)
        {
            if (gameManager.CheckPlaceInGrid(x, y + 1))
            {
                var puyoUp = gameManager.GetPuyo(x, y + 1);
                if (puyoUp.GetColor() == color && !puyoUp.GetSearched())
                {
                    searched = true;
                    upMatch = true;
                    puyoUp.CountPuyo();
                    puyoUp.SearchNeighbours();
                }
            }
        }
        else if (y == gameManager.GetHeight() - 1)
        {
            if (gameManager.CheckPlaceInGrid(x, y - 1))
            {
                var puyoDown = gameManager.GetPuyo(x, y - 1);
                if (puyoDown.GetColor() == color && !puyoDown.GetSearched())
                {
                    searched = true;
                    downMatch = true;
                    puyoDown.CountPuyo();
                    puyoDown.SearchNeighbours();
                }
            }
        }
        else
        {
            if (gameManager.CheckPlaceInGrid(x, y + 1))
            {
                var puyoUp = gameManager.GetPuyo(x, y + 1);
                if (puyoUp.GetColor() == color && !puyoUp.GetSearched())
                {
                    searched = true;
                    upMatch = true;
                    puyoUp.CountPuyo();
                    puyoUp.SearchNeighbours();
                }
            }

            if (gameManager.CheckPlaceInGrid(x, y - 1))
            {
                var puyoDown = gameManager.GetPuyo(x, y - 1);
                if (puyoDown.GetColor() == color && !puyoDown.GetSearched())
                {
                    searched = true;
                    downMatch = true;
                    puyoDown.CountPuyo();
                    puyoDown.SearchNeighbours();
                }
            }
        }
    }
    void CheckInFallPiece()
    {
        ResetNeighbours();
        CountPuyo();
        SearchNeighbours();
    }
    public void SearchNeighbours()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        SearchInX(x, y);
        SearchInY(x, y);

        if (!leftMatch && !rightMatch && !upMatch && !downMatch)
        {
            gameManager.DeletePuyos();
        }
    }
    public void CountPuyo()
    {
        gameManager.CountPuyo(transform);
    }

    //----------Set----------
    public void SetSearched(bool status)
    {
        searched = status;
    }
    //----------Get----------
    public bool GetSearched()
    {
        return searched;
    }
    public int GetColor()
    {
        return color;
    }
}
