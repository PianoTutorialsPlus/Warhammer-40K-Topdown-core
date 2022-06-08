using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace WH40K.Gameplay.PlayerEvents
{
    public class UnitShootingPhase : UnitPhasesBase, IUnitActionPhase
    {
        private Settings _settings;

        private void Start()
        {
            //_unit = GetComponent<IUnit>();
            enabled = _settings.Enabled;
        }
        private void OnEnable()
        {
            //_unit.ResetData();
            Debug.Log("enable");
        }
        private void OnDisable()
        {
            Debug.Log("disable");
        }

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
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
            else if (pointerEvent.button == PointerEventData.InputButton.Right/* && _gameStats.ActiveUnit != null*/)
            {
                UnitSelector.SelectEnemyUnit();
            }
        }
        [Serializable]
        public class Settings
        {
            public bool Enabled;
        }
    }
}