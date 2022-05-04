using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public class ShootingSubPhaseManager : MonoBehaviour, IResult
    {
        [SerializeField] private GameStatsSO _gameStats;

        [SerializeField] private RollTheDiceSO diceRollingResult;

        [SerializeField] private RollTheDiceSO _diceAction;
        [SerializeField] private RollTheDiceSO _diceSubResult;
        [SerializeField] private RollTheDiceSO _diceResult;

        public RollTheDiceSO DiceAction => _diceAction;
        public RollTheDiceSO DiceSubResult => _diceSubResult;
        public RollTheDiceSO DiceResult => _diceResult;

        [SerializeField] private List<int> _parameter = new List<int>();
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }
        public List<int> Parameter { get => _parameter; set => _parameter = value; }

        private Queue<ShootingSubEvents> shootingSubPhase = new Queue<ShootingSubEvents>();

        private void Awake()
        {
            enabled = false;
            EnqueueShootingSubPhase();
            new ShootingSubPhaseProcessor(this);
        }
        private void EnqueueShootingSubPhase()
        {
            shootingSubPhase.Enqueue(ShootingSubEvents.SelectEnemy);
            shootingSubPhase.Enqueue(ShootingSubEvents.Hit);
            shootingSubPhase.Enqueue(ShootingSubEvents.Wound);
            shootingSubPhase.Enqueue(ShootingSubEvents.Save);
            shootingSubPhase.Enqueue(ShootingSubEvents.Damage);
        }
        private void OnEnable()
        {
            if (diceRollingResult != null) diceRollingResult.OnEventRaised += ProcessResult;
        }
        private void HandleShooting()
        {
            Debug.Log(shootingSubPhase.Peek());
            //CalculationBaseSO calculation = ShootingSubPhaseProcessor.SetCalculation(calculations, shootingSubPhase);
            //ICalculation calculation = ShootingSubPhaseProcessor.SetCalculation(shootingSubPhase.Peek()); ;
            ShootingSubPhaseProcessor.SetCalculation(shootingSubPhase.Peek());

            //if (calculation == nul89l) return;
            ShootingSubPhaseProcessor.HandleShooting(shootingSubPhase.Peek());
        }
        private void ProcessResult(ShootingSubEvents diceEvent, List<int> result)
        {
            /*if (shootingSubPhase.Peek() == diceEvent) */shootingSubPhase.Enqueue(shootingSubPhase.Dequeue());

            if (Parameter != null) Debug.Log(shootingSubPhase.Peek());

            CheckNullValues(result);
        }
        private void CheckNullValues(List<int> values)
        {
            if (values == null || values.Count == 0)
            {
                //NextPhase();
            }
        }
        private void Wait()
        {
            StartCoroutine(WaitForButtonCoroutine());
        }
        public IEnumerator WaitForButtonCoroutine()
        {
            //Debug.Log("Waiting");
            yield return null;
            HandleShooting();
        }
    }
}
