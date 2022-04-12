using System.Collections.Generic;
using WH40K.Essentials;
using WH40K.GameMechanics.Combat;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>
namespace WH40K.GameMechanics
{
    public abstract class ShootingSubPhases
    {
        public ShootingSubPhases() { }
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        public abstract ShootingSubEvents SetSubPhase(); // sets the next subphase
        public abstract ICalculation SetCalculation(IResult calculations); // refers to the active calculation process
        public abstract void HandleShooting(List<int> parameter, ICalculation calculateHits, GameStatsSO gameStats); // handles the subphases
        public virtual List<int> ProcessResult(List<int> result) { return result; } // placeholder for results, may be removed
    }

    public class SelectEnemy : ShootingSubPhases
    {
        ICalculation calculation;
        public SelectEnemy() { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
        public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Hit; }
        public override ICalculation SetCalculation(IResult calculations)// { return (ICalculation)calculations[0]; }
        {
            return calculation ??= new SelectEnemies(calculations);
        }

        public override void HandleShooting(List<int> parameter, ICalculation selectEnemy, GameStatsSO gameStats)
        {
            //selectEnemy.Action(gameStats);
            selectEnemy.Action();
        }

    }

    public class Hit : ShootingSubPhases
    {
        public Hit() { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
        public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Wound; }
        public override ICalculation SetCalculation(IResult calculations) //{ return (ICalculation)calculations[1]; }
        {
            return calculation ??= new CalculateHits(calculations);
        }

        public override void HandleShooting(List<int> parameter, ICalculation calculateHits, GameStatsSO gameStats)
        {
            calculateHits.Action();
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound() { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Save; }

        public override ICalculation SetCalculation(IResult calculations) //{ return (ICalculation)calculations[2]; }
        {
            return calculation ??= new CalculateWounds(calculations);
        }

        public override void HandleShooting(List<int> parameter, ICalculation calculateWounds, GameStatsSO gameStats)
        {
            calculateWounds.Action(parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save() { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.Damage; }
        public override ICalculation SetCalculation(IResult calculations)// { return (ICalculation)calculations[3]; }
        {
            return calculation ??= new CalculateSaveroles(calculations);
        }

        public override void HandleShooting(List<int> parameter, ICalculation calculateSaves, GameStatsSO gameStats)
        {
            calculateSaves.Action(parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage() { }
        ICalculation calculation;

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
        public override ShootingSubEvents SetSubPhase() { return ShootingSubEvents.SelectEnemy; }
        public override ICalculation SetCalculation(IResult calculations)// { return (ICalculation)calculations[4]; }
        {
            return calculation ??= new DealDamage(calculations);
        }

        public override void HandleShooting(List<int> parameter, ICalculation dealDamage, GameStatsSO gameStats)
        {
            dealDamage.Action(parameter);
        }
    }
}