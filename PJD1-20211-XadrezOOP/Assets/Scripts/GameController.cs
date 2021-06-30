using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject piecePrefab { get; set; }
    public string player { get; set; }

    public GameObject[,] allPieces = new GameObject[8, 8];
    
    public GameObject controller { get; set; }
    public GameObject movePlate { get; set; }
    public int xBoard { get { return (-1); } set { ; } }
    public int yBoard { get { return (-1); } set { ; } }

    public string currentPlayer = "white";
    public bool gameOver = false;

    protected virtual void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoords();
    }

    protected virtual GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(piecePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        GameController cm = obj.GetComponent<GameController>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    protected virtual void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    protected virtual int GetXBoard()
    {
        return xBoard;
    }

    protected virtual int GetYBoard()
    {
        return yBoard;
    }

    protected virtual void SetXBoard(int x)
    {
        xBoard = x;
    }

    protected virtual void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void SetPosition(GameObject obj)
    {
        GameController cm = obj.GetComponent<GameController>();
        allPieces[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        allPieces[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return allPieces[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= allPieces.GetLength(0) || y >= allPieces.GetLength(1)) return false;
        return true;
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    private void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("MobileChess");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
