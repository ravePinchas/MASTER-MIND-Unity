using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatOfTheDeadPlate : MonoBehaviour
{
    public GameObject controller;

    GameObject hat = null;
    GameObject deadPiece = null;

    string player;

    // Board position not world position
    int matrixX;
    int matrixY;

    int matrixY2;
    int matrixX2;

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (controller.GetComponent<Game>().IsGameOver())
        {
            return;
        }

        if (hat.name.Contains("Blue"))
        {
            player = "blue";
        }
        else
        {
            player = "yellow";
        }

        controller.GetComponent<Game>().FindPieceAndDeleteIt(hat);
        deadPiece.GetComponent<PlayersPieces>().SetXBoard(matrixX2);
        deadPiece.GetComponent<PlayersPieces>().SetYBoard(matrixY2);
        deadPiece.GetComponent<PlayersPieces>().SetCoords();
        controller.GetComponent<Game>().RemoveDeadPieacesAndSetThemAlive(deadPiece, player);
        controller.GetComponent<Game>().SetPosition(deadPiece);
        Destroy(hat);

        



        deadPiece.GetComponent<PlayersPieces>().isAbility = false;
        deadPiece.GetComponent<PlayersPieces>().isAttack = false;
        deadPiece.GetComponent<PlayersPieces>().isMove = false;
        
        deadPiece.GetComponent<PlayersPieces>().DestroyMovePlates();
        deadPiece.GetComponent<PlayersPieces>().DestroyAttackPlates();
        deadPiece.GetComponent<PlayersPieces>().DestroyReplacerPlates();
        deadPiece.GetComponent<PlayersPieces>().DestroyHatPlates();
        deadPiece.GetComponent<PlayersPieces>().DestroyChaosPlates();
        deadPiece.GetComponent<PlayersPieces>().DestroyWallPlates();

    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetCoordsForDeadPiece(int x, int y)
    {
        matrixX2 = x;
        matrixY2 = y;
    }

    public void SetHat(GameObject obj)
    {
        hat = obj;
    }

    public GameObject GetHat()
    {
        return hat;
    }

    public void SetDeadPiece(GameObject obj)
    {
        deadPiece = obj;
    }
}
