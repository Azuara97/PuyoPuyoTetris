using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puyo : MonoBehaviour
{
    //PuyoVariables
    int color = -1;
    bool searched = false;

    //Other
    MyGameManager gameManager;
    enum colors { red, green, blue, yellow, purple, gray, bomb }

    void Start()
    {
        gameManager = FindObjectOfType<MyGameManager>();
        SetColor();
    }

    void Update()
    {
        
    }

    //----------StartFunctions----------
    void SetColor()
    {
        float probability = Random.Range(0f, 1f);
        float grayPuyoProbability = gameManager.GetGrayProbability() / 100f;
        float bombPuyoProbability = gameManager.GetBombProbability() / 100f;
        float maxProbability = grayPuyoProbability + bombPuyoProbability;
        if (probability >= 0f && probability <= grayPuyoProbability)
        {
            color = (int)colors.gray;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (probability > grayPuyoProbability && probability <= maxProbability)
        {
            color = (int)colors.bomb;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
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
    }

    //----------Fall----------
    public bool CheckPlaceBelow()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        if (y != 0 && !gameManager.CheckPlaceInGrid(x, y - 1))
        {
            return true;
        }
        else
        {
            CheckGameOver();
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
            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);

            gameManager.ClearPlaceInGrid(x, y);
            transform.position += new Vector3(0f, -1f, 0f);
            gameManager.AddPuyoToGrid(transform);
        }
        CheckGameOver();
    }

    //----------Count----------
    void SearchInX(int x, int y)
    {
        if (x == 0)
        {
            if (gameManager.CheckPlaceInGrid(x + 1, y))
            {
                var puyoRight = gameManager.GetPuyo(x + 1, y);
                if (puyoRight.color != (int)colors.gray && !puyoRight.searched &&
                    puyoRight.color == color || puyoRight.color == (int)colors.bomb)
                {
                    searched = true;
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
                if (puyoLeft.color != (int)colors.gray && !puyoLeft.searched &&
                    puyoLeft.color == color || puyoLeft.color == (int)colors.bomb)
                {
                    searched = true;
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
                if (puyoRight.color != (int)colors.gray && !puyoRight.searched &&
                    puyoRight.color == color || puyoRight.color == (int)colors.bomb)
                {
                    searched = true;
                    puyoRight.CountPuyo();
                    puyoRight.SearchNeighbours();
                }
            }

            if (gameManager.CheckPlaceInGrid(x - 1, y))
            {
                var puyoLeft = gameManager.GetPuyo(x - 1, y);
                if (puyoLeft.color != (int)colors.gray && !puyoLeft.searched &&
                    puyoLeft.color == color || puyoLeft.color == (int)colors.bomb)
                {
                    searched = true;
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
                if (puyoUp.color != (int)colors.gray && !puyoUp.searched &&
                    puyoUp.color == color || puyoUp.color == (int)colors.bomb)
                {
                    searched = true;
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
                if (puyoDown.color != (int)colors.gray && !puyoDown.searched &&
                    puyoDown.color == color || puyoDown.color == (int)colors.bomb)
                {
                    searched = true;
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
                if (puyoUp.color != (int)colors.gray && !puyoUp.searched &&
                    puyoUp.color == color || puyoUp.color == (int)colors.bomb)
                {
                    searched = true;
                    puyoUp.CountPuyo();
                    puyoUp.SearchNeighbours();
                }
            }

            if (gameManager.CheckPlaceInGrid(x, y - 1))
            {
                var puyoDown = gameManager.GetPuyo(x, y - 1);
                if (puyoDown.color != (int)colors.gray && !puyoDown.searched &&
                    puyoDown.color == color || puyoDown.color == (int)colors.bomb)
                {
                    searched = true;
                    puyoDown.CountPuyo();
                    puyoDown.SearchNeighbours();
                }
            }
        }
    }
    void SearchNeighbours()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);

        SearchInX(x, y);
        SearchInY(x, y);

        //Debug.Log(gameManager.GetTotalPuyosSameColor());
        gameManager.DeletePuyos();
    }
    void CountPuyo()
    {
        gameManager.CountPuyo(this);
    }
    public void CheckNeighbours()
    {
        CountPuyo();
        SearchNeighbours();
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
