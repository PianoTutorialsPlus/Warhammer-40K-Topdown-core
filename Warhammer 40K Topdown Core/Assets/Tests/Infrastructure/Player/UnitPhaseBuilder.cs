using UnityEngine;
using WH40K.Gameplay.PlayerEvents;
using WH40K.NavMesh;

namespace Editor.Infrastructure
{
    public class UnitPhaseBuilder<T> : TestDataBuilder<T> where T: Component
    {
        private IUnit _unit;
        private UnitSelector _unitSelector;
        private UnitPointer _unitPointer;
        private UnitMovementPhase.Settings _moveSettings;
        private UnitShootingPhase.Settings _shootingSettings;

        public IPathCalculator _pathCalculator { get; private set; }

        public UnitPhaseBuilder()
        {
        }
        public UnitPhaseBuilder<T> WithUnitSelector(UnitSelector unitSelector)
        {
            _unitSelector = unitSelector;
            return this;
        }
        public UnitPhaseBuilder<T> WithUnit(IUnit unit)
        {
            _unit = unit;
            return this;
        }
        public UnitPhaseBuilder<T> WithUnitPointer(UnitPointer unitPointer)
        {
            _unitPointer = unitPointer;
            return this;
        }
        public UnitPhaseBuilder<T> WithPathCalculator(IPathCalculator pathCalculator)
        {
            _pathCalculator = pathCalculator;
            return this;
        }
        public UnitPhaseBuilder<T> WithMovementSettings(UnitMovementPhase.Settings settings)
        {
            _moveSettings = settings;
            return this;
        }
        public UnitPhaseBuilder<T> WithShootingSettings(UnitShootingPhase.Settings settings)
        {
            _shootingSettings = settings;
            return this;
        }

        public override T Build()
        {
            Container.BindInstance(_unitSelector ??= A.UnitSelector);
            Container.BindInstance(_unit ??= A.Unit.Build()).AsSingle();
            Container.BindInstance(_unitPointer ??= A.UnitPointer);
            Container.BindInstance(_pathCalculator ??= A.PathCalculator.Build());

            Container.BindInstance(_moveSettings);
            Container.BindInstance(_shootingSettings);

            return Container.InstantiateComponentOnNewGameObject<T>();
        }
    }
}