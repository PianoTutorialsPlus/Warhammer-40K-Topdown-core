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
        protected List<int> _parameter => _result.Parameter;

        public ShootingSubPhases(IResult result)
        {
            _result = result;
        }
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        public abstract void SetCalculation(IResult result); // refers to the active calculation process
        public abstract void HandleShooting(); // handles the subphases
        public virtual List<int> ProcessResult(List<int> result) { return result; } // placeholder for results, may be removed
    }

    public class SelectEnemy : ShootingSubPhases
    {
        ICalculation calculation;
        public SelectEnemy(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
        public override void SetCalculation(IResult result)
        {
            calculation ??= new SelectEnemies(result);
        }

        public override void HandleShooting()
        {
            calculation.Action();
        }

    }

    public class Hit : ShootingSubPhases
    {
        public Hit(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
        public override void SetCalculation(IResult calculations)
        {
            calculation ??= new CalculateHits(calculations);
        }
        public override void HandleShooting()
        {
            calculation.Action();
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        public override void SetCalculation(IResult calculations)
        {
            calculation ??= new CalculateWounds(calculations);
        }

        public override void HandleShooting()
        {
            calculation.Action(_parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        public override void SetCalculation(IResult calculations)// { return (ICalculation)calculations[3]; }
        {
            calculation ??= new CalculateSaveroles(calculations);
        }

        public override void HandleShooting()
        {
            calculation.Action(_parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage(IResult result) : base(result) { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
        public override void SetCalculation(IResult calculations)
        {
            calculation ??= new DealDamage(calculations);
        }

        public override void HandleShooting()
        {
            calculation.Action(_parameter);
        }
    }
}