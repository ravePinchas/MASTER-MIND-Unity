using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    bool referenceIsBomb = false;

    // Board position not world position
    int matrixX;
    int matrixY;

    public void OnMouseUp()
    {

        SetAttack(false);
        
    }

    public void SetAttack(bool isExplotion)
    {
        if (!isExplotion)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");
            if (controller.GetComponent<Game>().IsGameOver())
            {
                return;
            }
            if (!reference.GetComponent<PlayersPieces>().isMove)
            {
                reference.GetComponent<PlayersPieces>().isMove = true;
                controller.GetComponent<Game>().movesCount++;
            }

            reference.GetComponent<PlayersPieces>().isAttack = true;
            reference.GetComponent<PlayersPieces>().DestroyMovePlates();
            reference.GetComponent<PlayersPieces>().DestroyAttackPlates();
            reference.GetComponent<PlayersPieces>().DestroyReplacerPlates();
            reference.GetComponent<PlayersPieces>().DestroyHatPlates();
            reference.GetComponent<PlayersPieces>().DestroyChaosPlates();
            reference.GetComponent<PlayersPieces>().DestroyWallPlates();
        }

        //The object we attacked
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
        if (!cp.name.Contains("Shield"))
        {
            controller.GetComponent<Game>().FindPieceAndDeleteIt(cp);
        }

        int refX = cp.GetComponent<PlayersPieces>().GetXBoard();
        int refY = cp.GetComponent<PlayersPieces>().GetYBoard();
        //we set the place of the object we moved to empty in the arrays object
        if (cp.name != "hatOfTheDeads_Blue" && cp.name != "hatOfTheDeads_Yellow" && !cp.name.Contains("Shield"))
        {
            controller.GetComponent<Game>().SetPositionEmpty(refX, refY);
        }

        SetPieceDeadPosition(cp, controller);
        
        if(reference.name == "bomb_Blue" || reference.name == "bomb_Yellow" && !referenceIsBomb)
        {
            referenceIsBomb = true;
            int x = reference.GetComponent<PlayersPieces>().GetXBoard();
            int y = reference.GetComponent<PlayersPieces>().GetYBoard();

            SetExplotion(x, y);
        }
        else if (reference.name == "bombShield_Blue" && !referenceIsBomb)
        {
            referenceIsBomb = true;
            reference.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<PlayersPieces>().bomb_Blue;
            reference.name = "bomb_Blue";
        }
        else if (reference.name == "bombShield_Yellow" && !referenceIsBomb)
        {
            referenceIsBomb = true;
            reference.GetComponent<SpriteRenderer>().sprite = reference.GetComponent<PlayersPieces>().bomb_Yellow;
            reference.name = "bomb_Yellow";
        }
    }


    public void SetPieceDeadPosition(GameObject obj, GameObject controller)
    {
        if (controller.GetComponent<Game>().playerBlueDeads.Contains(obj) || 
            controller.GetComponent<Game>().playerYellowDeads.Contains(obj)) {
            Destroy(obj);
            return;
        }

        switch (obj.name)
        {

            case "bombShield_Blue":
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().bomb_Blue;
                obj.name = "bomb_Blue";
                break;
            //case "hat_Blue":
            //    controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
            //    controller.GetComponent<Game>().FindPieceAndDeleteIt(obj);
            //    Destroy(obj);
            //    break;
            case "bomb_Blue":
                int x;
                int y;
                x = obj.GetComponent<PlayersPieces>().GetXBoard();
                y = obj.GetComponent<PlayersPieces>().GetYBoard();
                obj.GetComponent<PlayersPieces>().SetXBoard(1);
                obj.GetComponent<PlayersPieces>().SetYBoard(-2);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().bombShield_Blue;
                //obj.name = "bombShield_Blue";
                SetExplotion(x, y);
                break;

            case "wallBreaker_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(0);
                obj.GetComponent<PlayersPieces>().SetYBoard(-3);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;
            case "changeMaker_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(1);
                obj.GetComponent<PlayersPieces>().SetYBoard(-3);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;
            case "chaos_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(3);
                obj.GetComponent<PlayersPieces>().SetYBoard(-3);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;
            case "cannon_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(2);
                obj.GetComponent<PlayersPieces>().SetYBoard(-2);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;
            case "overload_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(2);
                obj.GetComponent<PlayersPieces>().SetYBoard(-3);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;
            case "replacer_Blue":
                obj.GetComponent<PlayersPieces>().SetXBoard(0);
                obj.GetComponent<PlayersPieces>().SetYBoard(-2);
                obj.GetComponent<PlayersPieces>().SetCoords();
                
                controller.GetComponent<Game>().SetDeadPieaces(obj, "blue");
                break;

            case "hatOfTheDeads_Blue":
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().hat_Blue;
                obj.name = "hat_Blue";
                    break;


            case "bombShield_Yellow":
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().bomb_Yellow;
                obj.name = "bomb_Yellow";
                break;
            case "bomb_Yellow":


                x = obj.GetComponent<PlayersPieces>().GetXBoard();
                y = obj.GetComponent<PlayersPieces>().GetYBoard();
                Debug.Log("x: " + x + "y: " + y);
                obj.GetComponent<PlayersPieces>().SetXBoard(6);
                obj.GetComponent<PlayersPieces>().SetYBoard(9);
                obj.GetComponent<PlayersPieces>().SetCoords();
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().bombShield_Yellow;
                //obj.name = "bombShield_Yellow";
                SetExplotion(x, y);

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "wallBreaker_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(7);
                obj.GetComponent<PlayersPieces>().SetYBoard(10);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "changeMaker_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(6);
                obj.GetComponent<PlayersPieces>().SetYBoard(10);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "chaos_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(4);
                obj.GetComponent<PlayersPieces>().SetYBoard(10);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "cannon_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(5);
                obj.GetComponent<PlayersPieces>().SetYBoard(9);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "overload_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(5);
                obj.GetComponent<PlayersPieces>().SetYBoard(10);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "replacer_Yellow":
                obj.GetComponent<PlayersPieces>().SetXBoard(7);
                obj.GetComponent<PlayersPieces>().SetYBoard(9);
                obj.GetComponent<PlayersPieces>().SetCoords();

                controller.GetComponent<Game>().SetDeadPieaces(obj, "yellow");
                break;
            case "hatOfTheDeads_Yellow":
                obj.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<PlayersPieces>().hat_Yellow;
                obj.name = "hat_Yellow";
                break;
        }
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

    public void SetExplotion(int x, int y)
    {
        BombAttackExpolotion(x , y + 1);
        BombAttackExpolotion(x + 1 , y + 1);
        BombAttackExpolotion(x + 1 , y);
        BombAttackExpolotion(x + 1 , y - 1);
        BombAttackExpolotion(x , y - 1);
        BombAttackExpolotion(x - 1 , y - 1);
        BombAttackExpolotion(x - 1 , y);
        BombAttackExpolotion(x - 1 , y + 1);
    }

    public void BombAttackExpolotion(int x, int y)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            //Debug.Log(cp.name);
            if (cp != null)
            {
                matrixX = x;
                matrixY = y;
                SetAttack(true);
            }
        }
    }
}
