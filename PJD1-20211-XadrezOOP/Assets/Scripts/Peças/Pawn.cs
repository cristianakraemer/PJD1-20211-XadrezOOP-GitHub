using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : BasePieces
{
    protected override void Activate()
    {
        base.Activate();

        switch (this.name)
        {
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    protected override void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        GameController sc = controller.GetComponent<GameController>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                sc.GetPosition(x + 1, y).GetComponent<GameController>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<GameController>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
}
