using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : BasePieces
{
    GameObject reference = null;
    public string namePiece;

    int matrixX;
    int matrixY;

    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    protected override void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp = controller.GetComponent<GameController>().GetPosition(matrixX, matrixY);
            if (cp.name == "white_king") controller.GetComponent<GameController>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<GameController>().Winner("white");
            Destroy(cp);
        }

        controller.GetComponent<GameController>().SetPositionEmpty(reference.GetComponent<MovePlate>().GetXBoard(),
            reference.GetComponent<MovePlate>().GetYBoard());

        reference.GetComponent<MovePlate>().SetXBoard(matrixX);
        reference.GetComponent<MovePlate>().SetYBoard(matrixY);
        reference.GetComponent<MovePlate>().SetCoords();

        controller.GetComponent<GameController>().SetPosition(reference);
        controller.GetComponent<GameController>().NextTurn();
        reference.GetComponent<GameController>().DestroyMovePlates();
    }

    protected void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
