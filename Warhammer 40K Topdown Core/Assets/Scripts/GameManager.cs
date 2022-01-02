using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] player1;
    public GameObject[] player2;
    public Text turnText;

    public GameObject[] player;
    public GameObject userControl;
   
    int turn = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = player1;
        UpdateTurnText();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void EndTurn()
    {
        if(player == player1)
        {
            player = player2;
        }
        else
        {
            player = player1;
            turn++;
        }
        UpdateTurnText();

        foreach (GameObject unit in player)
        {
            unit.GetComponent<Unit>().ResetData(); 
         }

    }

    public void UpdateTurnText()
    {
        turnText.text = $"{player[0].tag} \nTurn: {turn}";
        
    }
}
