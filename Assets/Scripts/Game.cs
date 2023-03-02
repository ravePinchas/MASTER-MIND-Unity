using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject gamePiece;
    public Button btnBlue;
    public Button btnYellow;

    public GameObject winnerText;
    public GameObject restartText;

    //private GameObject[,] positionsBlueDeads = new GameObject[4, 2];
    //private GameObject[,] positionsYellowDeads = new GameObject[4, 2];
    
    private GameObject[,] positions = new GameObject[8, 8];
    public List<GameObject> playerBlue;
    public List<GameObject> playerYellow;
    public List<GameObject> playerYellowDeads;
    public List<GameObject> playerBlueDeads;
    public List<GameObject> wallsBlue;
    public List<GameObject> wallsYellow;

    

    private string currentPlayer = "blue";

    private bool gameOver = false;

    public int movesCount = 0;

    public int xWall = -9;
    public int yWall = -9;

    // Start is called before the first frame update
    void Start()
    {
        playerBlue = new List<GameObject> {
            Create("wallBreaker_Blue", 0, 0), Create("changeMaker_Blue", 1, 0), Create("overload_Blue", 2, 0),
            Create("hatOfTheDeads_Blue", 3, 0), Create("chaos_Blue", 4, 0), Create("overload_Blue", 5, 0),
            Create("changeMaker_Blue", 6, 0), Create("wallBreaker_Blue", 7, 0),
            Create("replacer_Blue", 0, 1), Create("replacer_Blue", 1, 1), Create("bombShield_Blue", 2, 1),
            Create("cannon_Blue", 3, 1), Create("cannon_Blue", 4, 1), Create("bombShield_Blue", 5, 1),
            Create("replacer_Blue", 6, 1), Create("replacer_Blue", 7, 1),
        };

        playerYellow = new List<GameObject> {
            Create("wallBreaker_Yellow", 0, 7), Create("changeMaker_Yellow", 1, 7), Create("overload_Yellow", 2, 7),
            Create("hatOfTheDeads_Yellow", 3, 7), Create("chaos_Yellow", 4, 7), Create("overload_Yellow", 5, 7),
            Create("changeMaker_Yellow", 6, 7), Create("wallBreaker_Yellow", 7, 7),
            Create("replacer_Yellow", 0, 6), Create("replacer_Yellow", 1, 6), Create("bombShield_Yellow", 2, 6),
            Create("cannon_Yellow", 3, 6), Create("cannon_Yellow", 4, 6), Create("bombShield_Yellow", 5, 6),
            Create("replacer_Yellow", 6, 6), Create("replacer_Yellow", 7, 6)
        };

        playerBlueDeads = new List<GameObject>();
        playerYellowDeads = new List<GameObject>();

        wallsBlue = new List<GameObject>() { Create("wall_Blue", 6, - 1), Create("wall_Blue", 7, - 1) };
        wallsYellow = new List<GameObject>() { Create("wall_Yellow", 0, 8), Create("wall_Yellow", 1, 8) };

        for (int i = 0; i < playerBlue.Count; i++)
        {
            SetPosition(playerBlue[i]);
            SetPosition(playerYellow[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(gamePiece, new Vector3(0, 0, -1), Quaternion.identity);
        PlayersPieces cm = obj.GetComponent<PlayersPieces>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        xWall = x;
        yWall = y;
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        PlayersPieces cm = obj.GetComponent<PlayersPieces>();
        if (obj.name != "wall_Blue" && obj.name != "wall_Yellow")
        {
            positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
        }
    }

    public void SwitchPosition(GameObject reference, GameObject player2Piece)
    {
        PlayersPieces currentPlayer = reference.GetComponent<PlayersPieces>();
        PlayersPieces player2 = player2Piece.GetComponent<PlayersPieces>();


        positions[currentPlayer.GetXBoard(), currentPlayer.GetYBoard()] = player2Piece;
        positions[player2.GetXBoard(), player2.GetYBoard()] = reference;
    }


    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool positionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
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

    public void DestroyWallPlates()
    {
        GameObject[] wallPlates = GameObject.FindGameObjectsWithTag("WallPlate");

        foreach (GameObject wallPlate in wallPlates)
        {
            Destroy(wallPlate);
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
        {
            GameObject[] replacerPlates = GameObject.FindGameObjectsWithTag("ReplacerPlate");

            foreach (GameObject replacerPlate in replacerPlates)
            {
                Destroy(replacerPlate);
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
    public void FindPieceAndDeleteIt(GameObject obj)
    {
        string playerList;
        if (obj.name.Contains("Blue"))
        {
            playerList = "blue";
        }
        else
        {
            playerList = "yellow";
        }
        
        if (playerList == "blue")
        {
            if (playerBlue.Contains(obj) && obj.name != "hatOfTheDeads_Blue")
            {
                playerBlue.Remove(obj);
            }
        }
        else
        {
            if (playerYellow.Contains(obj) && obj.name != "hatOfTheDeads_Yellow")
            {
                playerYellow.Remove(obj);
            }
        }  
    }

    public void SetDeadPieaces(GameObject obj, string playerList)
    { 
        if(playerList == "blue")
        {
            if (!playerBlueDeads.Contains(obj))
            {
                playerBlueDeads.Add(obj);
            }
        }
        else
        {
            if (!playerYellowDeads.Contains(obj))
            {
                playerYellowDeads.Add(obj);
            }
        }
    }

    public void RemoveDeadPieacesAndSetThemAlive(GameObject obj, string playerList)
    {
        if (playerList == "blue")
        {
            if (playerBlueDeads.Contains(obj))
            {
                playerBlueDeads.Remove(obj);
                if(obj.name == "bomb_Blue")
                {
                    obj.name = "bombShield_Blue";
                }
                playerBlue.Add(obj);
            }
        }
        else
        {
            if (playerYellowDeads.Contains(obj))
            {
                playerYellowDeads.Remove(obj);
                if (obj.name == "bomb_Yellow")
                {
                    obj.name = "bombShield_Yellow";
                }
                playerYellow.Add(obj);
            }
        }
    }

    public void Update()
    {
        ChangeButtonsStates();
        if (gameOver == true && Input.GetMouseButton(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                DestroyMovePlates();
                DestroyAttackPlates();
                DestroyReplacerPlates();
                DestroyHatPlates();
                DestroyChaosPlates();
                DestroyWallPlates();
            }
        }
    }

    public void Winner(string playerWinner)
    {
        winnerText.gameObject.SetActive(true);
        winnerText.GetComponent<TMP_Text>().text = playerWinner + " is the winner!";
        
        restartText.gameObject.SetActive(true);

        gameOver = true;
    }
    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void ChangeButtonsStates()
    {
        if (currentPlayer == "blue" && movesCount > 0)
        {
            btnBlue.interactable = true;
            if(movesCount == 1)
            {
                btnBlue.GetComponentInChildren<TMP_Text>().text = "Moves: 1";
            }
            else
            {
                btnBlue.GetComponentInChildren<TMP_Text>().text = "Next Turn";
            }
        }
        if (currentPlayer == "yellow" && movesCount > 0)
        {
            btnYellow.interactable = true;
            if (movesCount == 1)
            {
                btnYellow.GetComponentInChildren<TMP_Text>().text = "Moves: 1";
            }
            else
            {
                btnYellow.GetComponentInChildren<TMP_Text>().text = "Next Turn";
            }
        }
    }
        

    public void NextTurn()
    {
        resetMoveAndAttack();
        if(currentPlayer == "blue")
        {
            currentPlayer = "yellow";
            movesCount = 0;
            btnBlue.gameObject.SetActive(false);
            btnYellow.gameObject.SetActive(true);
            btnYellow.interactable = false;
            btnYellow.GetComponentInChildren<TMP_Text>().text = "Moves: 0";
            btnYellow.GetComponentInChildren<TMP_Text>().color = Color.black;
        }
        else
        {
            currentPlayer = "blue";
            movesCount = 0;
            btnBlue.gameObject.SetActive(true);
            btnYellow.gameObject.SetActive(false);
            btnBlue.interactable = false;
            btnBlue.GetComponentInChildren<TMP_Text>().text = "Moves: 0";
            btnBlue.GetComponentInChildren<TMP_Text>().color = Color.black;
        }
    }

    public void resetMoveAndAttack()
    {
        if (currentPlayer == "blue")
        {
            foreach(GameObject obj in playerBlue)
            {
                obj.GetComponent<PlayersPieces>().isMove = false;
                obj.GetComponent<PlayersPieces>().isAttack = false;
                obj.GetComponent<PlayersPieces>().isAbility = false;
            }
        }
        else
        {
            foreach (GameObject obj in playerYellow)
            {
                obj.GetComponent<PlayersPieces>().isMove = false;
                obj.GetComponent<PlayersPieces>().isAttack = false;
                obj.GetComponent<PlayersPieces>().isAbility = false;
            }
        }
    }
}
