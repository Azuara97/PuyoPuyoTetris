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
    List<Transform> puyos = new List<Transform>();

    //Score
    int score = 0;
    int puyosDestroyed = 0;
    int pointsPerPuyo = 10;

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
        puyos.Clear();
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
    public void CountPuyo(Transform puyo)
    {
        puyos.Add(puyo);
    }
    public void DeletePuyos()
    {
        if(puyos.Count >= 4)
        {
            foreach(var puyo in puyos)
            {
                int x = Mathf.RoundToInt(puyo.position.x);
                int y = Mathf.RoundToInt(puyo.position.y);
                Destroy(puyo.gameObject);
                grid[x, y] = null;
                puyosDestroyed++;
            }

            foreach(var puyo in puyos)
            {
                int x = Mathf.RoundToInt(puyo.position.x);
                int y = Mathf.RoundToInt(puyo.position.y);
                int actualX = -1;
                if (actualX != x)
                {
                    actualX = x;
                    for (int i = y; i < height; i++)
                    {
                        if (grid[x, i])
                        {
                            grid[x, i].GetComponent<Puyo>().FallFromPiece();
                            grid[x, i] = null;
                        }
                    }
                }
            }

            score += puyosDestroyed * pointsPerPuyo;
            puyosDestroyed = 0;
        }
        foreach(var puyo in FindObjectsOfType<Puyo>())
        {
            puyo.SetSearched(false);
        }
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
}
