using GameMechanics.Combat;
using System.Collections.Generic;
using WH40K.GameMechanics.Combat;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>
namespace WH40K.GameMechanics
{
    public abstract class ShootingSubPhases
    {
        protected IResult _result;

        public ShootingSubPhases(IResult result)
        {
            _result = result;
        }
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        public abstract void HandleShooting(List<int> parameter); // handles the subphases
        public void Next() { CombatProcessor.Next(SubEvents); }
    }

    public class SelectEnemy : ShootingSubPhases
    {
        ICalculation calculation;
        public SelectEnemy(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
        private void SetCalculation()
        {
            calculation ??= new SelectEnemies(_result);
        }
        public override void HandleShooting(List<int> parameter)
        {
            //SetCalculation();
            //calculation.Action();
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Hit : ShootingSubPhases
    {
        public Hit(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
        private void SetCalculation()
        {
            calculation ??= new CalculateHits(_result);
        }
        public override void HandleShooting(List<int> parameter)
        {
            //SetCalculation();
            //calculation.Action();
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        private void SetCalculation()
        {
            calculation ??= new CalculateWounds(_result);
        }
        public override void HandleShooting(List<int> parameter)
        {
            //SetCalculation();
            //calculation.Action(parameter);
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        private void SetCalculation()
        {
            calculation ??= new CalculateSaveroles(_result);
        }

        public override void HandleShooting(List<int> parameter)
        {
            //SetCalculation();
            //calculation.Action(parameter);
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
        private void SetCalculation()
        {
            calculation ??= new DealDamage(_result);
        }
        public override void HandleShooting(List<int> parameter)
        {
            //SetCalculation();
            //calculation.Action(parameter);
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
}