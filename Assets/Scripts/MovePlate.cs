using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    GameObject hat = null;

    // Board position not world position
    int matrixX;
    int matrixY;
    int matrixYHat;
    int matrixXHat;

    bool isTwoMovesOverload = false;

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (controller.GetComponent<Game>().IsGameOver())
        {
            return;
        }
        
        int refX = reference.GetComponent<PlayersPieces>().GetXBoard();
        int refY = reference.GetComponent<PlayersPieces>().GetYBoard();
        //we set the place of the object we moved to empty in the arrays object
        controller.GetComponent<Game>().SetPositionEmpty(refX, refY);

        if (hat)
        {
            int refXHat = hat.GetComponent<PlayersPieces>().GetXBoard();
            int refYHat = hat.GetComponent<PlayersPieces>().GetYBoard();
            controller.GetComponent<Game>().SetPositionEmpty(refXHat, refYHat);
            controller.GetComponent<Game>().FindPieceAndDeleteIt(hat);
            Destroy(hat);
        }

        //The object is moved to the new position (onMouseUp mean that we pressd on it)
        reference.GetComponent<PlayersPieces>().SetXBoard(matrixX);
        reference.GetComponent<PlayersPieces>().SetYBoard(matrixY);

        reference.GetComponent<PlayersPieces>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

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
            reference.GetComponent<PlayersPieces>().isMove = true;
            if (isTwoMovesOverload)
            {
                controller.GetComponent<Game>().movesCount++;
            }
            controller.GetComponent<Game>().movesCount++;
            reference.GetComponent<PlayersPieces>().DestroyMovePlates();
            reference.GetComponent<PlayersPieces>().DestroyAttackPlates();
            reference.GetComponent<PlayersPieces>().DestroyReplacerPlates();
            reference.GetComponent<PlayersPieces>().DestroyHatPlates();
            reference.GetComponent<PlayersPieces>().DestroyChaosPlates();
            reference.GetComponent<PlayersPieces>().DestroyWallPlates();

            reference.GetComponent<PlayersPieces>().InitiateAttackPlates();
            reference.GetComponent<PlayersPieces>().InitiateReplacerPlates();
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetCoordsHat(int x, int y)
    {
        matrixXHat = x;
        matrixYHat = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetRefernce()
    {
        return reference;
    }

    public void SetReferenceHat(GameObject obj)
    {
        hat = obj;
    }
    public GameObject GetHat()
    {
        return hat;
    }

    public void SetBoolOverloadMoves(bool isTwoMoves)
    {
        isTwoMovesOverload = isTwoMoves;
    }
}
