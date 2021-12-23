using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    //GameVariables
    bool pauseGame = true;
    static int width = 8;
    static int height = 16;
    Vector3 spawnPosition;
    Transform[,] grid = new Transform[width, height + 3];
    List<Puyo> puyos = new List<Puyo>();
    [SerializeField]
    float grayProbability = 5f;
    [SerializeField]
    float bombProbability = 5f;

    //Score
    int score = 0;
    int puyosDestroyed = 0;
    int pointsPerPuyo = 10;

    //Other
    enum colors { red, green, blue, yellow, purple, gray, bomb }

    void Start()
    {

    }

    void Update()
    {
        
    }

    //----------SpawnPuyo----------
    public void SpawnStartPuyo()
    {
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        Instantiate(Resources.Load<GameObject>("Piece"), spawnPosition, Quaternion.identity);
    }
    public void SpawnNewPuyo()
    {
        Instantiate(Resources.Load<GameObject>("Piece"), spawnPosition, Quaternion.identity);
    }

    //----------GridFunction----------
    public void AddPuyoToGrid(Transform puyo)
    {
        int x = Mathf.RoundToInt(puyo.transform.position.x);
        int y = Mathf.RoundToInt(puyo.transform.position.y);

        grid[x, y] = puyo;
    }
    public void ClearPlaceInGrid(int x, int y)
    {
        grid[x, y] = null;
    }
    public bool CheckPlaceInGrid(int x, int y)
    {
        if (grid[x, y] != null)
        {
            return true;
        }
        return false;
    }
    public Puyo GetPuyo(int x, int y)
    {
        return grid[x, y].GetComponent<Puyo>();
    }

    //----------Count----------
    void ClearPuyos()
    {
        puyos.Clear();
    }
    void FallPuyos()
    {
        for (int j = 0; j < width; j++)
        {
            for(int i = 0; i < height; i++)
            {
                if (grid[j, i] != null)
                {
                    var puyo = GetPuyo(j, i);
                    while (puyo.CheckPlaceBelow())
                    {
                        int x = Mathf.RoundToInt(puyo.transform.position.x);
                        int y = Mathf.RoundToInt(puyo.transform.position.y);

                        puyo.transform.position += new Vector3(0f, -1f, 0f);
                        grid[x, y - 1] = puyo.transform;
                        grid[x, y] = null;
                    }
                }
            }
        }
    }
    void BombDestroy(Puyo bomb)
    {
        int x = Mathf.RoundToInt(bomb.transform.position.x);
        int y = Mathf.RoundToInt(bomb.transform.position.y);

        if (x == 0)
        {
            if (y == 0)
            {
                Debug.Log("0,0");
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
                if (grid[x + 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y + 1).gameObject);
                    grid[x + 1, y + 1] = null;
                }
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
            }
            else if (y == height)
            {
                Debug.Log("0,h");
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
                if (grid[x + 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y - 1).gameObject);
                    grid[x + 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
            }
            else
            {
                Debug.Log("0,y");
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
                if (grid[x + 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y + 1).gameObject);
                    grid[x + 1, y + 1] = null;
                }
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
                if (grid[x + 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y - 1).gameObject);
                    grid[x + 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
            }
        }
        else if (x == width)
        {
            if (y == 0)
            {
                Debug.Log("w,0");
                if (grid[x - 1, y] != null)
                {
                    Destroy(GetPuyo(x - 1, y).gameObject);
                    grid[x - 1, y] = null;
                }
                if (grid[x - 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y + 1).gameObject);
                    grid[x - 1, y + 1] = null;
                }
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
            }
            else if (y == height)
            {
                Debug.Log("w,h");
                if (grid[x - 1, y] != null)
                {
                    Destroy(GetPuyo(x - 1, y).gameObject);
                    grid[x - 1, y] = null;
                }
                if (grid[x - 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y - 1).gameObject);
                    grid[x - 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
            }
            else
            {
                Debug.Log("w,y");
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
                if (grid[x - 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y + 1).gameObject);
                    grid[x - 1, y + 1] = null;
                }
                if (grid[x - 1, y] != null)
                {
                    Destroy(GetPuyo(x - 1, y).gameObject);
                    grid[x - 1, y] = null;
                }
                if (grid[x - 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y - 1).gameObject);
                    grid[x - 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
            }
        }
        else
        {
            if (y == 0)
            {
                Debug.Log("x,0");
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
                if (grid[x + 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y + 1).gameObject);
                    grid[x + 1, y + 1] = null;
                }
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
                if (grid[x - 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y + 1).gameObject);
                    grid[x - 1, y + 1] = null;
                }
                if (grid[x - 1, y] != null)
                {
                    Destroy(GetPuyo(x - 1, y).gameObject);
                    grid[x - 1, y] = null;
                }
            }
            else if (y == height)
            {
                Debug.Log("x,h");
                if (grid[x - 1, y] != null)
                {
                    Destroy(GetPuyo(x - 1, y).gameObject);
                    grid[x - 1, y] = null;
                }
                if (grid[x - 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y - 1).gameObject);
                    grid[x - 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
                if (grid[x + 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y - 1).gameObject);
                    grid[x + 1, y - 1] = null;
                }
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
            }
            else
            {
                Debug.Log("x,y");
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
                if (grid[x + 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y + 1).gameObject);
                    grid[x + 1, y + 1] = null;
                }
                if (grid[x, y + 1] != null)
                {
                    Destroy(GetPuyo(x, y + 1).gameObject);
                    grid[x, y + 1] = null;
                }
                if (grid[x + 1, y + 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y + 1).gameObject);
                    grid[x + 1, y + 1] = null;
                }
                if (grid[x + 1, y] != null)
                {
                    Destroy(GetPuyo(x + 1, y).gameObject);
                    grid[x + 1, y] = null;
                }
                if (grid[x + 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x + 1, y - 1).gameObject);
                    grid[x + 1, y - 1] = null;
                }
                if (grid[x, y - 1] != null)
                {
                    Destroy(GetPuyo(x, y - 1).gameObject);
                    grid[x, y - 1] = null;
                }
                if (grid[x - 1, y - 1] != null)
                {
                    Destroy(GetPuyo(x - 1, y - 1).gameObject);
                    grid[x - 1, y - 1] = null;
                }
            }
        }
    }
    public void CountPuyo(Puyo puyo)
    {
        puyos.Add(puyo);
    }
    public int GetTotalPuyosSameColor()
    {
        return puyos.Count;
    }
    public void DeletePuyos()
    {
        if(puyos.Count >= 4)
        {
            foreach (var puyo in puyos)
            {
                int x = Mathf.RoundToInt(puyo.transform.position.x);
                int y = Mathf.RoundToInt(puyo.transform.position.y);
                if (puyo.GetColor() == (int)colors.bomb)
                {
                    //BombDestroy(puyo);
                }
                Destroy(puyo.gameObject);
                grid[x, y] = null;
                puyosDestroyed++;
            }

            FallPuyos();

            score += puyosDestroyed * pointsPerPuyo;
            puyosDestroyed = 0;
        }

        foreach(var puyo in FindObjectsOfType<Puyo>())
        {
            puyo.SetSearched(false);
        }

        ClearPuyos();
    }

    //----------GameOver----------
    public void RestartGame()
    {
        foreach(var puyo in FindObjectsOfType<PlayerController>())
        {
            Destroy(puyo.gameObject);
        }
        grid.Initialize();
        SpawnStartPuyo();
        pauseGame = false;
        score = 0;
    }

    //----------Sets----------
    public void SetPauseGame(bool value)
    {
        pauseGame = value;
    }

    //----------Gets----------
    public bool IsPauseGame()
    {
        return pauseGame;
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public int GetScore()
    {
        return score;
    }
    public float GetGrayProbability()
    {
        return grayProbability;
    }
    public float GetBombProbability()
    {
        return bombProbability;
    }
}
