using UnityEngine;

namespace WH40K
{
    public class GameManager : MonoBehaviour
    {
        public Unit[] player1;
        public Unit[] player2;
        //public Text turnText;
        //public Text[] infoPanel;
        public PlayerSO _player1;
        public PlayerSO _player2;
        public GameTableSO _table;

        public Unit[] player;
        public GameObject userControl;
        //public string phase;
        public GameTable gameTable;
        //private UnitManager unitManager;

        int turn = 1;
        // Start is called before the first frame update
        void Start()
        {
            //unitManager = GetComponent<UnitManager>();
            //unitManager.Load();

            _table.gameTable = gameTable;
            _player1._playerUnits.Clear();
            _player2._playerUnits.Clear();

            foreach (Unit unit in player1)
            {
                _player1._playerUnits.Add(unit);
            }

            foreach (Unit unit in player2)
            {
                _player2._playerUnits.Add(unit);
            }
            player = player1;
            //phase = "Movement Phase";
            //UpdateTurnText();
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}

        //public void EndTurn()
        //{
        //    switch (phase)
        //    {
        //        case "Movement Phase":
        //            phase = "Shooting Phase";

        //            foreach (Unit unit in player)
        //            {
        //                if (unit != null)
        //                {
        //                    unit.GetComponent<Unit>().Freeze();
        //                    unit.GetComponent<Unit>().phase = phase;
        //                }
        //            }

        //            break;
        //        case "Shooting Phase":
        //            phase = "Movement Phase";

        //            if (player == player1)
        //            {
        //                player = player2;
        //            }
        //            else
        //            {
        //                player = player1;
        //                turn++;
        //            }

        //            foreach (Unit unit in player)
        //            {
        //                if (unit != null)
        //                {
        //                    unit.GetComponent<Unit>().ResetData();
        //                    unit.GetComponent<Unit>().phase = phase;
        //                }
        //            }

        //            break;
        //    }

        //    UpdateTurnText();

        //}

        //public void UpdateTurnText()
        //{
        //    foreach (Unit unit in player)
        //    {
        //        if (unit != null)
        //        {
        //            turnText.text = $"{unit.tag} \n{phase}\nTurn: {turn}";
        //            break;
        //        }
        //    }


        //}

        //public void UpdateInfoPanel()
        //{

        //}
    }
}
