using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnNextTurn : MonoBehaviour
{
    public Button btnBlue;
    public Button btnYellow;
    public void nextTurn()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        if (controller.GetComponent<Game>().IsGameOver())
        {
            return;
        }
        if (controller.GetComponent<Game>().movesCount == 0)
        {
            return;
        }   
        controller.GetComponent<Game>().NextTurn();
    }
}
