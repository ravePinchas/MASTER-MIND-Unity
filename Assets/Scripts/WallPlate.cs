using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Board position not world position
    int matrixX;
    int matrixY;

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
        controller.GetComponent<Game>().movesCount++;


        reference.GetComponent<PlayersPieces>().DestroyMovePlates();
        reference.GetComponent<PlayersPieces>().DestroyAttackPlates();
        reference.GetComponent<PlayersPieces>().DestroyReplacerPlates();
        reference.GetComponent<PlayersPieces>().DestroyHatPlates();
        reference.GetComponent<PlayersPieces>().DestroyChaosPlates();
        reference.GetComponent<PlayersPieces>().DestroyWallPlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetRefernce()
    {
        return reference;
    }
}
