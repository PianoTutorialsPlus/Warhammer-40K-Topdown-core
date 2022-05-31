using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using WH40K.InputEvents;
using Zenject;

namespace WH40K.PlayerEvents
{
    public enum Fraction { None = 0, SpaceMarines, Necrons }

    [RequireComponent(typeof(NavMeshAgent))]

    public class UnitFacade : MonoBehaviour, IUnit// INHARITANCE
    {
        public int weaponRange;
        internal float restDistance;
        public bool IsSelected { get; set; }
        public InputReader _inputReader;
        //----------------------------------------------------------
        private UnitModel _model;
        private UnitStats _unitStats;
        private UnitPointer _unitPointer;
        
        public Fraction Fraction => _unitStats.Fraction;
        public bool IsDone => _unitStats.IsDone;
        public bool IsActivated => _unitStats.IsActivated;
        public Transform Transform => _model.Transform;
        public Vector3 CurrentPosition => _model.Position;

        public UnitMovementPhase UnitMovementPhase;
        public UnitShootingPhase UnitShootingPhase;
        public UnitMover UnitMover { get; protected set; }

        public UnitFacade unit => this;
        public UnityAction<IUnit> OnTapDownAction { get => _unitPointer.OnTapDownAction; set => _unitPointer.OnTapDownAction = value; }
        public UnityAction OnPointerEnter { get => _unitPointer.OnPointerEnter; set => _unitPointer.OnPointerEnter = value; }
        public UnityAction<IUnit> OnPointerEnterInfo { get => _unitPointer.OnPointerEnterInfo; set => _unitPointer.OnPointerEnterInfo = value; }
        public UnityAction<IUnit> OnPointerExit { get => _unitPointer.OnPointerExit; set => _unitPointer.OnPointerExit = value; }

        public int WeaponRange => _unitStats.WeaponRange;
        public int WeaponShots => _unitStats.WeaponShots;
        public int WeaponStrength => _unitStats.WeaponStrength;
        public int WeaponArmourPen => _unitStats.WeaponArmourPen;
        public int WeaponDamage => _unitStats.WeaponDamage;
        public string WeaponName => _unitStats.WeaponName;
        public int Movement => _unitStats.Movement;
        public int BallisticSkill => _unitStats.BallisticSkill;
        public int Toughness => _unitStats.Toughness;
        public int ArmourSave => _unitStats.ArmourSave;
        public int Wounds { get; set; }

        protected void Awake()
        {
            Wounds = 2;//_unitSO.Wounds;
        }

        [Inject]
        public void Construct(
            UnitStats unitStats,
            UnitMover unitMover,
            UnitPointer unitPointer,
            UnitMovementPhase unitMovementPhase,
            UnitShootingPhase unitShootingPhase,
            UnitModel model)
        {
            _model = model;
            _unitStats = unitStats;
            _unitPointer = unitPointer;
            UnitMover = unitMover;
            UnitMovementPhase = unitMovementPhase;
            UnitShootingPhase = unitShootingPhase;
        }

        public void ResetData()
        {
            _unitStats.UnFreeze();
            UnitMover.MovementRange.ResetRange();
            //Debug.Log("Reset:" + name);
            //////canMove = true;
            //canShoot = true;
            //movedDistance = 0;
            //activated = false;
            //IsSelected = false;
            //PathCalculator.ResetAgent();
            //done = false;
        }
        public void Activate()
        {
            _unitStats.Activate();
        }
        public void PrepareMovementPhase()
        {
            //Debug.Log("ResetMovement");
            //m_Agent.isStopped = false;
            //restDistance = MoveDistance;
        }
        public void SetDestination(Vector3 position)
        {
            UnitMover.SetDestination(position);
        }
        public void PrepareShootingPhase()
        {
            //Debug.Log("ResetShooting");
            //m_Agent.isStopped = true;
            //restDistance = weaponRange;
        }
        public virtual void Freeze()
        {
            _unitStats.Freeze();
            ////canMove = false;
            //PathCalculator.FreezeAgent();
            //activated = false;
            //IsSelected = false;
            //done = true;
            //_gameStats.movementSubPhase = MovementPhase.Selection;
            //SetMovementPhaseEvent.RaiseEvent(_gameStats);
        }
        public void TakeDamage(int damage)
        {
            Wounds -= damage;
            if (Wounds <= 0) Destroy();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}