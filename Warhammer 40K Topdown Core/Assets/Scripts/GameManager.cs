using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] player1;
    public GameObject[] player2;
    public Text turnText;
    public Text[] infoPanel;

    public GameObject[] player;
    public GameObject userControl;
    public string phase;

    int turn = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = player1;
        phase = "Movement Phase";
        UpdateTurnText();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void EndTurn()
    {
        switch (phase)
        {
            case "Movement Phase":
                phase = "Shooting Phase";

                foreach (GameObject unit in player)
                {
                    if (unit != null)
                    {
                        unit.GetComponent<Unit>().Freeze();
                        unit.GetComponent<Unit>().phase = phase;
                    }
                }

                break;
            case "Shooting Phase":
                phase = "Movement Phase";

                if (player == player1)
                {
                    player = player2;
                }
                else
                {
                    player = player1;
                    turn++;
                }

                foreach (GameObject unit in player)
                {
                    if (unit != null)
                    {
                        unit.GetComponent<Unit>().ResetData();
                        unit.GetComponent<Unit>().phase = phase;
                    }
                }

                break;
        }

        UpdateTurnText();

    }

    public void UpdateTurnText()
    {
        foreach (GameObject unit in player)
        {
            if (unit != null)
            {
                turnText.text = $"{unit.tag} \n{phase}\nTurn: {turn}";
                break;
            }
        }


    }

    public void UpdateInfoPanel()
    {

    }
}
