using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K.DiceEvents;
using WH40K.InputEvents;
using WH40K.Stats;
using Zenject;

namespace WH40K.Gameplay.GamePhaseEvents
{
    public class ShootingSubPhaseManager : MonoBehaviour
    {
        private InputReader _inputReader;
        private RollTheDiceEventChannelSO _diceResult;
        private ShootingPhaseManager _shootingPhase;

        private List<int> _parameter = new List<int>();
        private Queue<ShootingSubEvents> shootingSubPhase = new Queue<ShootingSubEvents>();

        private void Awake()
        {
            enabled = false;

            EnqueueShootingSubPhase();
        }
        [Inject]
        public void Construct(
            RollTheDiceEventChannelSO diceResult,
            ShootingPhaseManager shootingPhaseManager,
            InputReader inputReader)
        {
            _diceResult = diceResult;
            _shootingPhase = shootingPhaseManager;
            _inputReader = inputReader;
        }
        private void EnqueueShootingSubPhase()
        {
            foreach (ShootingSubEvents phase in ShootingSubPhaseProcessor.GetAbilityByName())
            {
                shootingSubPhase.Enqueue(phase);
            }
        }
        private void OnEnable()
        {
            Debug.Log("Enable ShootingSubPhaseManager");
            if (_diceResult != null) _diceResult.OnEventRaised += ProcessResult;
            _inputReader.ExecuteEvent += Wait;
        }
        private void OnDisable()
        {
            if (_diceResult != null) _diceResult.OnEventRaised -= ProcessResult;
            _inputReader.ExecuteEvent -= Wait;
        }
        private void SetShootingSubPhase()
        {
            Debug.Log(shootingSubPhase.Peek());
            ShootingSubPhaseProcessor.HandleShooting(shootingSubPhase.Peek(), _parameter);
        }
        private void ProcessResult(List<int> result)
        {
            _parameter = result;
            ShootingSubPhaseProcessor.Next(shootingSubPhase.Peek());
            shootingSubPhase.Enqueue(shootingSubPhase.Dequeue());

            if (_parameter != null) Debug.Log(shootingSubPhase.Peek());

            CheckNullValues(result);
        }
        private void CheckNullValues(List<int> values)
        {
            if (values == null || values.Count == 0)
            {
                //Debug.Log("Empty");
                _shootingPhase.NextPhase();
            }
        }
        private void Wait()
        {
            //Debug.Log("Wait");
            StartCoroutine(WaitForButtonCoroutine());
        }
        public IEnumerator WaitForButtonCoroutine()
        {
            yield return null;
            //Debug.Log("Waiting");
            SetShootingSubPhase();
        }
    }
}
