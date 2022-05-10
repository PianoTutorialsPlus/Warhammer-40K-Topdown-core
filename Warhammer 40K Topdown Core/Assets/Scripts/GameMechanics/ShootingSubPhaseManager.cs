using GameMechanics.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.GameMechanics
{
    public class ShootingSubPhaseManager : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private RollTheDiceSO diceRollingResult;
        [SerializeField] private ShootingPhaseManager _shootingPhase;

        [SerializeField] private List<int> _parameter = new List<int>();
        public InputReader InputReader { get => _inputReader; }

        private Queue<ShootingSubEvents> shootingSubPhase = new Queue<ShootingSubEvents>();

        private void Awake()
        {
            enabled = false;

            EnqueueShootingSubPhase();
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
            if (diceRollingResult != null) diceRollingResult.OnEventRaised += ProcessResult;
            _inputReader.ExecuteEvent += Wait;
        }
        private void OnDisable()
        {
            if (diceRollingResult != null) diceRollingResult.OnEventRaised -= ProcessResult;
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
