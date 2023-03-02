using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacerPlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    GameObject player2Piece = null;

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

        reference.GetComponent<PlayersPieces>().SetXBoard(matrixX);
        reference.GetComponent<PlayersPieces>().SetYBoard(matrixY);
        reference.GetComponent<PlayersPieces>().SetCoords();

        player2Piece.GetComponent<PlayersPieces>().SetXBoard(matrixX2);
        player2Piece.GetComponent<PlayersPieces>().SetYBoard(matrixY2);
        player2Piece.GetComponent<PlayersPieces>().SetCoords();

        //controller.GetComponent<Game>().SwitchPosition(reference, player2Piece);
        controller.GetComponent<Game>().SetPosition(reference);
        controller.GetComponent<Game>().SetPosition(player2Piece);

        if (reference.GetComponent<PlayersPieces>().player == "blue" && matrixY == 7)
        {
            controller.GetComponent<Game>().Winner("blue");
        }
        else if (reference.GetComponent<PlayersPieces>().player == "yellow" && matrixY == 0)
        {
            controller.GetComponent<Game>().Winner("yellow");
        }
        else
        {
            reference.GetComponent<PlayersPieces>().isAbility = true;
            reference.GetComponent<PlayersPieces>().DestroyMovePlates();
            reference.GetComponent<PlayersPieces>().DestroyAttackPlates();
            reference.GetComponent<PlayersPieces>().DestroyReplacerPlates();
            reference.GetComponent<PlayersPieces>().DestroyHatPlates();
            reference.GetComponent<PlayersPieces>().DestroyChaosPlates();
            reference.GetComponent<PlayersPieces>().DestroyWallPlates();
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetCoordsOtherPlayer(int x, int y)
    {
        matrixX2 = x;
        matrixY2 = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetRefernce()
    {
        return reference;
    }

    public void SetPlayer2Object(GameObject obj)
    {
        player2Piece = obj;
    }
}
