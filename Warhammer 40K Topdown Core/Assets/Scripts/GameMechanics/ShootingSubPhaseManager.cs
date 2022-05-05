using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public class ShootingSubPhaseManager : MonoBehaviour, IResult
    {
        [SerializeField] private GameStatsSO _gameStats;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private RollTheDiceSO diceRollingResult;
        [SerializeField] private ShootingPhaseManager _shootingPhase;

        [SerializeField] private RollTheDiceSO _diceAction;
        [SerializeField] private RollTheDiceSO _diceSubResult;
        [SerializeField] private RollTheDiceSO _diceResult;

        public RollTheDiceSO DiceAction => _diceAction;
        public RollTheDiceSO DiceSubResult => _diceSubResult;
        public RollTheDiceSO DiceResult => _diceResult;

        [SerializeField] private List<int> _parameter = new List<int>();
        public GameStatsSO GameStats { get => _gameStats; set => _gameStats = value; }
        public InputReader InputReader { get => _inputReader; /*set => _inputReader = value;*/ }

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
            _inputReader.ActivateEvent += SetShootingSubPhase;
            _inputReader.ExecuteEvent += Wait;
        }
        private void OnDisable()
        {
            if (diceRollingResult != null) diceRollingResult.OnEventRaised -= ProcessResult;
            _inputReader.ActivateEvent -= SetShootingSubPhase;
            _inputReader.ExecuteEvent -= Wait;
        }
        private void SetShootingSubPhase()
        {
            Debug.Log(shootingSubPhase.Peek());
            //ICalculation calculation = ShootingSubPhaseProcessor.SetCalculation(shootingSubPhase.Peek()); ;
            ShootingSubPhaseProcessor.HandleShooting(shootingSubPhase.Peek(), _parameter);
        }
        private void ProcessResult(ShootingSubEvents diceEvent, List<int> result)
        {
            _parameter = result;
            shootingSubPhase.Enqueue(shootingSubPhase.Dequeue());

            if (_parameter != null) Debug.Log(shootingSubPhase.Peek());

            CheckNullValues(result);
        }
        private void CheckNullValues(List<int> values)
        {
            if (values == null || values.Count == 0)
            {
                Debug.Log("Empty");
                _shootingPhase.NextPhase();
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
            SetShootingSubPhase();
        }
    }
}
