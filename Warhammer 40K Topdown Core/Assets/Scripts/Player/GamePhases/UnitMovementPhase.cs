using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WH40K.NavMesh;
using Zenject;

namespace WH40K.PlayerEvents

{
    public class UnitMovementPhase : UnitPhasesBase, IUnitActionPhase
    {
        private Settings _settings;
        private IPathCalculator _pathCalculator;

        private void Start()
        {
            //_unit = GetComponent<IUnit>();
            enabled = _settings.Enabled;
        }

        private void OnEnable()
        {
            //  Debug.Log("enable");
            if (_unit != null)
            {
                _unit.ResetData();
                _pathCalculator.ResetAgent();
            }
        }

        private void OnDisable()
        {
            //_unit.ResetData();
            //_unit.PathCalculator.FreezeAgent();
            //  Debug.Log("disable");
        }

        [Inject]
        public void Construct(
            Settings settings,
            IPathCalculator pathCalculator)
        {
            _settings = settings;
            _pathCalculator = pathCalculator;
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            if (onPointerEnter != null) onPointerEnter();
            if (onPointerEnterInfo != null) onPointerEnterInfo(Unit);
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            if (onPointerExit != null)
            {
                onPointerExit(Unit);
            }
        }
        public void OnPointerClick(PointerEventData pointerEvent)
        {
            if (onTapDownAction == null) return;
            if (pointerEvent.button == PointerEventData.InputButton.Left)
            {
                UnitSelector.SelectUnit();
                onTapDownAction(Unit);
            }
        }
        [Serializable]
        public class Settings
        {
            public bool Enabled;
        }
    }
}