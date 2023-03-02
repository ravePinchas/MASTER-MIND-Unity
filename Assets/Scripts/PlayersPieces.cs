using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersPieces : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;
    public GameObject attackPlate;
    public GameObject replacerPlate;
    public GameObject deadPlate;
    public GameObject chaosPlate;
    public GameObject wallPlate;

    private int xBoard = -1;
    private int yBoard = -1;
    private float xWall = -1f;
    private float yWall = -1f;

    public string player;
    public bool isMove = false;
    public bool isAttack = false;
    public bool isAbility = false;
    public bool isShield = false;
    public bool isReplacer = false;
    public bool isWall = false;

    public Sprite bombShield_Blue, bomb_Blue, replacer_Blue, overload_Blue, hatOfTheDeads_Blue, wallBreaker_Blue, cannon_Blue, changeMaker_Blue, chaos_Blue, hat_Blue, wall_Blue, 
        bombShield_Yellow, bomb_Yellow, replacer_Yellow, overload_Yellow, hatOfTheDeads_Yellow, wallBreaker_Yellow, cannon_Yellow, changeMaker_Yellow, hat_Yellow, chaos_Yellow, wall_Yellow;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "bombShield_Blue": this.GetComponent<SpriteRenderer>().sprite = bombShield_Blue; player = "blue"; isShield = true; break;
            case "bomb_Blue": this.GetComponent<SpriteRenderer>().sprite = bomb_Blue; player = "blue"; break;
            case "replacer_Blue": this.GetComponent<SpriteRenderer>().sprite = replacer_Blue; player = "blue"; isReplacer = true; break;
            case "overload_Blue": this.GetComponent<SpriteRenderer>().sprite = overload_Blue; player = "blue"; break;
            case "hatOfTheDeads_Blue": this.GetComponent<SpriteRenderer>().sprite = hatOfTheDeads_Blue; player = "blue"; break;
            case "wallBreaker_Blue": this.GetComponent<SpriteRenderer>().sprite = wallBreaker_Blue; player = "blue"; break;
            case "cannon_Blue": this.GetComponent<SpriteRenderer>().sprite = cannon_Blue; player = "blue"; break;
            case "changeMaker_Blue": this.GetComponent<SpriteRenderer>().sprite = changeMaker_Blue; player = "blue"; break;
            case "chaos_Blue": this.GetComponent<SpriteRenderer>().sprite = chaos_Blue; player = "blue"; break;
                
            case "wall_Blue": this.GetComponent<SpriteRenderer>().sprite = wall_Blue; isWall = true; player = "blue"; break;

                
            case "bombShield_Yellow": this.GetComponent<SpriteRenderer>().sprite = bombShield_Yellow; player = "yellow"; isShield = true; break;
            case "bomb_Yellow": this.GetComponent<SpriteRenderer>().sprite = bomb_Yellow; player = "yellow"; break;
            case "replacer_Yellow": this.GetComponent<SpriteRenderer>().sprite = replacer_Yellow; player = "yellow"; isReplacer = true; break;
            case "overload_Yellow": this.GetComponent<SpriteRenderer>().sprite = overload_Yellow; player = "yellow"; break;
            case "hatOfTheDeads_Yellow": this.GetComponent<SpriteRenderer>().sprite = hatOfTheDeads_Yellow; player = "yellow"; break;
            case "wallBreaker_Yellow": this.GetComponent<SpriteRenderer>().sprite = wallBreaker_Yellow; player = "yellow"; break;
            case "cannon_Yellow": this.GetComponent<SpriteRenderer>().sprite = cannon_Yellow; player = "yellow"; break;
            case "changeMaker_Yellow": this.GetComponent<SpriteRenderer>().sprite = changeMaker_Yellow; player = "yellow"; break;
            case "chaos_Yellow": this.GetComponent<SpriteRenderer>().sprite = chaos_Yellow; player = "yellow"; break;
                
            case "wall_Yellow": this.GetComponent<SpriteRenderer>().sprite = wall_Yellow; isWall = true; player = "yellow"; break;
        }
    }

    public void SetCoords()
    {
        float x;
        float y;
            
        if (this.isWall)
        {
            x = xBoard;
            y = yBoard - 0.5f;
        }
        else
        {
             x = xBoard;
             y = yBoard;
        }


        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        //-1 on z its infornt of the board
        this.transform.position = new Vector3(x, y, -1);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player 
            && controller.GetComponent<Game>().movesCount < 2)
        {
            DestroyMovePlates();
            DestroyAttackPlates();
            DestroyReplacerPlates();
            DestroyHatPlates();
            DestroyChaosPlates();
            DestroyWallPlates();

            InitiateMovePlates();
            InitiateReplacerPlates();
            InitiateHatPlates();
            InitiateChaosPlates();
            InitiateWallPlates();

            if (this.name.Contains("cannon") && !isAttack)
            {
                CannonAttackPlate2();
            }

            if (isMove && !isAttack)
            {
                InitiateAttackPlates();
            }
        }
        else if(isReplacer && !controller.GetComponent<Game>().IsGameOver() 
            && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            InitiateReplacerPlates();
        }
        else if(this.name.Contains("hat_") && !controller.GetComponent<Game>().IsGameOver()
            && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            InitiateHatPlates();
        }
        else if(this.name.Contains("chaos") && !controller.GetComponent<Game>().IsGameOver()
            && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            InitiateChaosPlates();
        }
    }
    public void DestroyMovePlates()
    {
        {
            GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

            foreach (GameObject movePlate in movePlates)
            {
                Destroy(movePlate);
            }
        }
    }

    public void DestroyChaosPlates()
    {
        {
            GameObject[] chaosPlates = GameObject.FindGameObjectsWithTag("ChaosPlate");

            foreach (GameObject chaosPlate in chaosPlates)
            {
                Destroy(chaosPlate);
            }
        }
    }
    public void DestroyHatPlates()
    {
        {
            GameObject[] hatPlates = GameObject.FindGameObjectsWithTag("DeadPlate");

            foreach (GameObject hatPlate in hatPlates)
            {
                Destroy(hatPlate);
            }
        }
    }
    public void DestroyAttackPlates()
    {
        {
            GameObject[] attackPlates = GameObject.FindGameObjectsWithTag("AttackPlate");

            foreach (GameObject attackPlate in attackPlates)
            {
                Destroy(attackPlate);
            }
        }
    }
    public void DestroyReplacerPlates()
    {
        GameObject[] replacerPlates = GameObject.FindGameObjectsWithTag("ReplacerPlate");
        
        foreach (GameObject replacerPlate in replacerPlates)
        {
            Destroy(replacerPlate);
        }
    }

    public void DestroyWallPlates()
    {
        GameObject[] wallPlates = GameObject.FindGameObjectsWithTag("WallPlate");

        foreach (GameObject wallPlate in wallPlates)
        {
            Destroy(wallPlate);
        }
    }
    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "bombShield_Blue":
            case "bomb_Blue":
            case "wallBreaker_Blue":
            case "changeMaker_Blue":
            case "hatOfTheDeads_Blue":
            case "bombShield_Yellow":
            case "bomb_Yellow":
            case "wallBreaker_Yellow":
            case "changeMaker_Yellow":
            case "hatOfTheDeads_Yellow":
                SurrundedMovePlate();
                break;

            case "cannon_Blue":
            case "chaos_Blue":
            case "cannon_Yellow":
            case "chaos_Yellow":
                SideMovePlate();
                break;
                
            case "replacer_Blue":
            case "replacer_Yellow":
                ReplacerMovePlate();
                break;
                
            case "overload_Blue":
            case "overload_Yellow":
                OverloadMovePlate();
                break;
        }
    }
    public void InitiateAttackPlates()
    {
        switch (this.name)
        {
            case "bombShield_Blue":
            case "bomb_Blue":
            case "wallBreaker_Blue":
            case "changeMaker_Blue":
            case "hatOfTheDeads_Blue":
            case "bombShield_Yellow":
            case "bomb_Yellow":
            case "wallBreaker_Yellow":
            case "changeMaker_Yellow":
            case "hatOfTheDeads_Yellow":
            case "overload_Blue":
            case "overload_Yellow":
                SurrundedAttackPlate();
                break;

            case "chaos_Yellow":
            case "chaos_Blue":
                SideAttackPlate();
                break;
            case "cannon_Blue":
            case "cannon_Yellow":
                CannonAttackPlate();
                break;
        }
    }
    public void ReplacerMovePlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);

        if (!IsObject(xBoard, yBoard + 1) || !CheckIfThereIs_A_Wall(xBoard, yBoard + 1))
            PointMovePlate(xBoard - 0, yBoard + 2, false);
        if (!IsObject(xBoard + 1, yBoard) || !CheckIfThereIs_A_Wall(xBoard + 1, yBoard))
            PointMovePlate(xBoard + 2, yBoard - 0, false);
        if (!IsObject(xBoard - 1, yBoard) || !CheckIfThereIs_A_Wall(xBoard - 1, yBoard))
            PointMovePlate(xBoard - 2, yBoard - 0, false);
        if (!IsObject(xBoard, yBoard - 1) || !CheckIfThereIs_A_Wall(xBoard, yBoard - 1))
            PointMovePlate(xBoard - 0, yBoard - 2, false);
    }
    public void InitiateReplacerPlates()
    {
        if (isReplacer && !isAbility)
        {
            //set the ability plates
            ReplacerPlateAbility(xBoard - 0, yBoard + 1);
            ReplacerPlateAbility(xBoard + 1, yBoard - 0);
            ReplacerPlateAbility(xBoard - 1, yBoard - 0);
            ReplacerPlateAbility(xBoard - 0, yBoard - 1);

            ReplacerPlateAbility(xBoard - 0, yBoard + 2);
            ReplacerPlateAbility(xBoard + 2, yBoard - 0);
            ReplacerPlateAbility(xBoard - 2, yBoard - 0);
            ReplacerPlateAbility(xBoard - 0, yBoard - 2);
        }
    }

    public void InitiateWallPlates()
    {
        if (yBoard >= 0 && yBoard <= 7)
        {
            return;
        }
        controller = GameObject.FindGameObjectWithTag("GameController");
        PlayersPieces cm = controller.GetComponent<Game>().wallsBlue[0].GetComponent<PlayersPieces>();
       
        int xBlue1 = cm.GetComponent<PlayersPieces>().GetXBoard();
        int yBlue1 = cm.GetComponent<PlayersPieces>().GetYBoard();
        int xYellow1 = cm.GetComponent<PlayersPieces>().GetXBoard();
        int yYellow1 = cm.GetComponent<PlayersPieces>().GetYBoard();

        PlayersPieces cm2 = controller.GetComponent<Game>().wallsBlue[1].GetComponent<PlayersPieces>();

        int xBlue2 = cm2.GetComponent<PlayersPieces>().GetXBoard();
        int yBlue2 = cm2.GetComponent<PlayersPieces>().GetYBoard();
        int xYellow2 = cm2.GetComponent<PlayersPieces>().GetXBoard();                                               
        int yYellow2 = cm2.GetComponent<PlayersPieces>().GetYBoard();

        if (isWall)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (!(i == xBlue1 && j == yBlue1) && !(i == xBlue2 && j == yBlue2) &&
                        !(i == xYellow1 && j == yYellow1) && !(i == xYellow2 && j == yYellow2))
                    {
                        //set the ability plates
                        WallPlateSpawn(i, j);
                    }
                }
            }

        }
    }

    public bool CheckIfThereIs_A_Wall(int x, int y)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        PlayersPieces cm = controller.GetComponent<Game>().wallsBlue[0].GetComponent<PlayersPieces>();

        int xBlue1 = cm.GetComponent<PlayersPieces>().GetXBoard();
        int yBlue1 = cm.GetComponent<PlayersPieces>().GetYBoard();
        int xYellow1 = cm.GetComponent<PlayersPieces>().GetXBoard();
        int yYellow1 = cm.GetComponent<PlayersPieces>().GetYBoard();

        PlayersPieces cm2 = controller.GetComponent<Game>().wallsBlue[1].GetComponent<PlayersPieces>();

        int xBlue2 = cm2.GetComponent<PlayersPieces>().GetXBoard();
        int yBlue2 = cm2.GetComponent<PlayersPieces>().GetYBoard();
        int xYellow2 = cm2.GetComponent<PlayersPieces>().GetXBoard();
        int yYellow2 = cm2.GetComponent<PlayersPieces>().GetYBoard();

        if (x == xBlue1 && y == yBlue1 || x == xBlue2 && y == yBlue2 || x == xYellow1 && y == yYellow1 || x == xYellow2 && y == yYellow2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WallPlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY - 0.5f;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(wallPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        WallPlate mpScript = mp.GetComponent<WallPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void InitiateChaosPlates()
    {
        if(yBoard < 0 || xBoard < 0 || yBoard > 7 || xBoard > 7)
        {
            return;
        }
        Game sc = controller.GetComponent<Game>();
        if (this.name == "chaos_Blue")
        {
            foreach (GameObject obj in sc.playerBlue)
            {
                if (obj.name != "hat_Blue" && obj.name != "chaos_Blue")
                {
                    int x = obj.GetComponent<PlayersPieces>().GetXBoard();
                    int y = obj.GetComponent<PlayersPieces>().GetYBoard();
                    ChaosPlateAbility(x, y);
                }
            }
        }
        else if (this.name == "chaos_Yellow")
        {
            foreach (GameObject obj in sc.playerYellow)
            {
                if (obj.name != "hat_Yellow" && obj.name != "chaos_Yellow")
                {
                    int x = obj.GetComponent<PlayersPieces>().GetXBoard();
                    int y = obj.GetComponent<PlayersPieces>().GetYBoard();
                    ChaosPlateAbility(x, y);
                }
            }
        }
    }

    public void ChaosPlateAbility(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (!isAbility && cp != null && cp.GetComponent<PlayersPieces>().player == player)
            {
                MoveChaosSpawn(x, y, cp);
            }
        }
    }
    
    public void InitiateHatPlates()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name == "hat_Blue")
        {
            foreach (GameObject obj in sc.playerBlueDeads)
            {
                int x = obj.GetComponent<PlayersPieces>().GetXBoard();
                int y = obj.GetComponent<PlayersPieces>().GetYBoard();
                HatPlateAbility(x, y, obj);
            }
        }
        else if (this.name == "hat_Yellow")
        {
            foreach (GameObject obj in sc.playerYellowDeads)
            {
                int x = obj.GetComponent<PlayersPieces>().GetXBoard();
                int y = obj.GetComponent<PlayersPieces>().GetYBoard();
                HatPlateAbility(x, y, obj);
            }
        }
    }
    public void HatPlateAbility(int matrixX, int matrixY, GameObject obj)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(deadPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        HatOfTheDeadPlate mpScript = mp.GetComponent<HatOfTheDeadPlate>();
        mpScript.SetHat(gameObject);
        mpScript.SetDeadPiece(obj);
        mpScript.SetCoords(matrixX, matrixY);
        mpScript.SetCoordsForDeadPiece(gameObject.GetComponent<PlayersPieces>().GetXBoard(), gameObject.GetComponent<PlayersPieces>().GetYBoard());
    }
    public void ReplacerPlateAbility(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (isReplacer && !isAbility && cp != null && cp.GetComponent<PlayersPieces>().player != player)
            {
                MoveReplacerSpawn(x, y, cp);
            }
        }
    }
    public void OverloadMovePlate()
    {
        //one move
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);

        controller = GameObject.FindGameObjectWithTag("GameController");
        if (controller.GetComponent<Game>().movesCount > 0)
        {
            return;
        }
        //two moves
        PointMovePlate(xBoard - 1, yBoard - 1, true);
        PointMovePlate(xBoard - 1, yBoard + 1, true);
        PointMovePlate(xBoard + 1, yBoard - 1, true);
        PointMovePlate(xBoard + 1, yBoard + 1, true);

        //check if there is an object between
        if (!IsObject(xBoard, yBoard + 1) || !CheckIfThereIs_A_Wall(xBoard, yBoard + 1))
        {
            PointMovePlate(xBoard - 0, yBoard + 2, true);
        }
        if (!IsObject(xBoard, yBoard + 1) || !CheckIfThereIs_A_Wall(xBoard, yBoard + 1))
        {
            PointMovePlate(xBoard - 0, yBoard - 2, true);
        }
        if (!IsObject(xBoard - 1, yBoard) || !CheckIfThereIs_A_Wall(xBoard - 1, yBoard))
        {
            PointMovePlate(xBoard - 2, yBoard - 0, true);
        }
        if (!IsObject(xBoard + 1, yBoard) || !CheckIfThereIs_A_Wall(xBoard + 1, yBoard))
        {
            PointMovePlate(xBoard + 2, yBoard - 0, true);
        }
        if (!IsObject(xBoard - 1, yBoard - 1) || !CheckIfThereIs_A_Wall(xBoard - 1, yBoard - 1))
        {
            PointMovePlate(xBoard - 2, yBoard - 2, true);
        }
        if (!IsObject(xBoard - 1, yBoard + 1) || !CheckIfThereIs_A_Wall(xBoard - 1, yBoard + 1))
        {
            PointMovePlate(xBoard - 2, yBoard + 2, true);
        }
        if (!IsObject(xBoard + 1, yBoard - 1) || !CheckIfThereIs_A_Wall(xBoard + 1, yBoard - 1))
        {
            PointMovePlate(xBoard + 2, yBoard - 2, true);
        }
        if (!IsObject(xBoard + 1, yBoard + 1) || !CheckIfThereIs_A_Wall(xBoard + 1, yBoard + 1))
        {
            PointMovePlate(xBoard + 2, yBoard + 2, true);
        }
    }
    public bool IsObject(int x, int y)
    {

        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                return false;
            }
            return true;
        }
        return true;
    }
    public void SurrundedMovePlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard - 1, yBoard + 1, false);
        PointMovePlate(xBoard + 1, yBoard - 1, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard + 1, false);
    }
    public void SurrundedAttackPlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard - 1, yBoard + 1, false);
        PointMovePlate(xBoard + 1, yBoard - 1, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard + 1, false);
    }
    public void SideMovePlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
    }
    public void CannonAttackPlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
        
        PointMovePlate(xBoard - 0, yBoard + 2, false);
        PointMovePlate(xBoard - 0, yBoard - 2, false);
        PointMovePlate(xBoard - 2, yBoard - 0, false);
        PointMovePlate(xBoard + 2, yBoard - 0, false);
    }
    public void CannonAttackPlate2()
    {
        PointCannonAttackPlate(xBoard - 0, yBoard + 1);
        PointCannonAttackPlate(xBoard - 0, yBoard - 1);
        PointCannonAttackPlate(xBoard - 1, yBoard - 0);
        PointCannonAttackPlate(xBoard + 1, yBoard - 0);

        PointCannonAttackPlate(xBoard - 0, yBoard + 2);
        PointCannonAttackPlate(xBoard - 0, yBoard - 2);
        PointCannonAttackPlate(xBoard - 2, yBoard - 0);
        PointCannonAttackPlate(xBoard + 2, yBoard - 0);
    }
    public void PointCannonAttackPlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp != null && cp.GetComponent<PlayersPieces>().player != player && !isAttack && !isReplacer && !cp.name.Contains("hat_"))
            {
                MoveAttackSpawn(x, y);
            }
        }
    }
    public void SideAttackPlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1, false);
        PointMovePlate(xBoard - 0, yBoard - 1, false);
        PointMovePlate(xBoard - 1, yBoard - 0, false);
        PointMovePlate(xBoard + 1, yBoard - 0, false);
    }
    public void PointMovePlate(int x, int y, bool isTwoMovesOverload)
    {
        Game sc = controller.GetComponent<Game>();
        if(sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            //Debug.Log(cp.name);
            if(cp == null || cp.GetComponent<PlayersPieces>().player != player && cp.name.Contains("hat_"))
            {
                if (CheckIfThereIs_A_Wall(x, y) && !this.name.Contains("wallBreaker"))
                {
                    return;
                }
                if(!isMove)
                {
                    if(cp != null && cp.name.Contains("hat_"))
                    {
                        MovePlateSpawn(x, y, true, cp, isTwoMovesOverload);
                    }
                    else
                    {
                        MovePlateSpawn(x, y, false, cp, isTwoMovesOverload);
                    }
                }
              
            }
            else if (cp != null && cp.GetComponent<PlayersPieces>().player != player && !isAttack && !isReplacer &&
                !cp.name.Contains("hat_"))
            {
                MoveAttackSpawn(x, y);
            }
        }
    }
    public void MoveReplacerSpawn(int matrixX, int matrixY, GameObject cp)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(replacerPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        ReplacerPlate mpScript = mp.GetComponent<ReplacerPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetPlayer2Object(cp);
        mpScript.SetCoords(matrixX, matrixY);
        mpScript.SetCoordsOtherPlayer(gameObject.GetComponent<PlayersPieces>().GetXBoard(), gameObject.GetComponent<PlayersPieces>().GetYBoard());
    }
    public void MoveChaosSpawn(int matrixX, int matrixY, GameObject cp)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(chaosPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        ChaosPlate mpScript = mp.GetComponent<ChaosPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetOtherPiece(cp);
        mpScript.SetCoords(matrixX, matrixY);
        mpScript.SetCoordsOtherPiece(gameObject.GetComponent<PlayersPieces>().GetXBoard(), gameObject.GetComponent<PlayersPieces>().GetYBoard());
    }

    public void MoveAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(attackPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        AttackPlate mpScript = mp.GetComponent<AttackPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateSpawn(int matrixX, int matrixY, bool hat, GameObject cp, bool isTwoMoveOverload)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
        if (hat)
        {
            mpScript.SetReferenceHat(cp);
            mpScript.SetCoordsHat(cp.GetComponent<PlayersPieces>().GetXBoard(), cp.GetComponent<PlayersPieces>().GetYBoard());
        }
        
    }
}
